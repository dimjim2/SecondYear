import java.util.*;

public class Main {
    static int num;
    public static int Quantum = 1;
    //Left Philosopher
    public static final int Left(int i) {
        return (i + num - 1) % num;
    }
    //Right Philosopher
    public static final int Right(int i) {
        return (i + 1) % num;
    }
    //Monitor
    public static ourMonitor mon;//=new ourMonitor(5);
    static int max=-1, position = 0;
    static int sum = 0;
    static int count3 = 0;


    public static void main(String... args) {
        Philosopher P[];
        Scanner myObj = new Scanner(System.in);  // Create a Scanner object
        System.out.println("Enter number of philosophers");
        boolean ok = false;
        while (!ok) {
            if (myObj.hasNextInt()) {
                num = myObj.nextInt();
                if (num >= 3 && num <= 10) {
                    ok = true;
                } else {
                    System.out.println("You typed an integer but not in range of 3-10");
                }
            } else {
                System.out.println("Not an integer give a again!!! ");
                myObj.next();
            }
        }
        System.out.println("Number of philosopher is " + num);
        //Create instances
        P = new Philosopher[num];
        mon = new ourMonitor(num);
        for (int j = 0; j < num; j++) {
            P[j] = new Philosopher(j);
            P[j].setPriority(P[j].priority);
            System.out.println("Priority: " + P[j].priority + " of Philosopher " + j);
            System.out.println("Runtime time: " + P[j].runtime);
        }
        //When a program calls the start() method, a new thread is created and then the run() method is executed
        for (int j = 0; j < num; j++) {
            P[j].start();

        }
        //Priority scheduling
        int counter;
        boolean itsok = false;
        while (!itsok) {
            counter=0;
            for(int i=0;i<num;i++){
                P[i].changePriorities();
            }
            for (int i=0;i<num;i++) {
                if (P[i].totalSeconds<20) {
                    if (max < P[i].priority) {
                        max = P[i].priority;
                        position = P[i].Id;
                    } else if (max == P[i].priority && P[position].previousRuntime > P[i].previousRuntime) {
                        max = P[i].priority;
                        position = P[i].Id;
                    }
                }
            }
            for(int i=0;i<num;i++) {
                if (P[i].totalSeconds < 20) {
                    try {
                        if (P[i].Id != position)
                            System.out.println("The philosopher "+i+" must sleep");
                        /*
                        yield() basically means that the thread is not doing anything particularly important and if any other threads
                         or processes need to be run, they should run. Otherwise, the current thread will continue to run.
                         */
                            P[i].yield();
                            P[i].sleep(P[position].runtime * 1000);

                    } catch (Exception e) {
                        System.out.println(e);
                    }
                }
            }
             for(int i=0;i<num;i++){
                 if(P[i].totalSeconds<20){
                     counter+=1;
                 }
             }
             if(counter==0){
                 System.out.println("Terminated");
                 itsok=true;
             }
        }
        for(int i=0;i<num;i++){
            count3=0;
            for(long j:P[i].waitingTime){
                count3+=j;
            }
            System.out.println("Size "+P[i].waitingTime.size());
            long w=count3/P[i].waitingTime.size();
            float sec = w / 1000F;
            sum+=sec;
            System.out.println("Waiting time for food "+sec+"(sec)");
        }
        long overalWaitTime=sum/num;
        System.out.println("Average waiting time for food for all philosophers "+overalWaitTime+"(sec)");
    }
}
