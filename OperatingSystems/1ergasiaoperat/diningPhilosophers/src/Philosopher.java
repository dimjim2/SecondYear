
import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Random;
public class Philosopher extends Thread{
    //Formatter for printing and parsing date-time objects using patterns.
    //The instance f will follow the pattern of hour,minutes and seconds.
    DateTimeFormatter f = DateTimeFormatter.ofPattern("HH:mm:ss");
    Random rand = new Random();
    int Id;
    int priority;
    public int runtime;
    int previousRuntime;
    int secondsToEat;
    int  totalSeconds;
    ArrayList<Long> waitingTime=new ArrayList<>();
    public Philosopher(int ID) {
        this.Id = ID;
        this.previousRuntime=0;
        this.priority = rand.nextInt(3) + 1;
        this.runtime=priority*Main.Quantum;
        this.secondsToEat=0;
        this.totalSeconds=0;
    }

    void changePriorities(){
        previousRuntime+=runtime;
        priority = rand.nextInt(3) + 1;
        // method returns a reference to the currently executing thread object and we change again the priority of object-thread
        Thread.currentThread().setPriority(priority);
        runtime=priority*Main.Quantum;
        System.out.println("Changed priority: " + priority +" of Philosopher "+Id);
    }
    public void run() {
          while (totalSeconds <20)
              try {
                  //changePriorities();
                  long startTime = System.currentTimeMillis();
                  Main.mon.take_forks(Id);
                  long endTime = System.currentTimeMillis();
                  waitingTime.add(endTime - startTime);
                  Eat();
                  Main.mon.put_forks(Id);
              }
              //Thrown when a thread is waiting, sleeping, or otherwise occupied, and the thread is interrupted,
              // either before or during the activity
                catch (InterruptedException e) {
                  e.printStackTrace();
              }
      }

    public void Eat() {
            secondsToEat=rand.nextInt(20)+1;
            if(secondsToEat+totalSeconds>20){
                secondsToEat-=secondsToEat+totalSeconds-20;
            }
            totalSeconds+=secondsToEat;
            try {
                System.out.println("Philosopher " + Id + " is eats for "+secondsToEat+".\n");
                System.out.println("Philosopher " + Id + " is eating at time "+f.format(LocalTime.now())+".\n");
                Thread.sleep(1000*secondsToEat);
             } catch (InterruptedException e) {
                e.printStackTrace();
        }
    }
}
