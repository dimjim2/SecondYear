import java.time.LocalDateTime;
import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.concurrent.Semaphore;
import java.util.concurrent.locks.*;
import java.util.*;
public class ourMonitor implements State {
    DateTimeFormatter f = DateTimeFormatter.ofPattern("HH:mm:ss");
    int state[];
    Condition[] self;
    ReentrantLock entLock;
    public ourMonitor(int num){
        entLock=new ReentrantLock();
        state=new int[num];
        self=new Condition[num];
        for(int i=0; i<num; i++){
            //Returns a Condition instance for use with this Lock instance.
            self[i]=entLock.newCondition();
            state[i]=State.Thinking;
        }
    }
    void take_forks(int i) throws InterruptedException {
        entLock.lock();
        state[i] = Hungry;
        System.out.println("Philosopher " + i + " is hungry at time " + f.format(LocalTime.now())+".\n");
        System.out.println("Lock hold value is "+entLock.getHoldCount()+" by Philosopher "+i);
        test(i);
        if (state[i] != Eating) {
                self[i].await();

            }
        entLock.unlock();
        System.out.println("Lock hold value is "+entLock.getHoldCount() +" ,unlocked");
    }
    void test(int i){
        if(state[i]==Hungry &&state[Main.Left(i)]!=Eating && state[Main.Right(i)]!=Eating){
            state[i]=Eating;
            self[i].signal();
        }
        else{

            if(state[Main.Left(i)]==Eating){
                System.out.println("Philosopher "+i+" failed to take left fork , because Philosopher "+Main.Left(i)+" was eating");
            }
            if(state[Main.Right(i)]==Eating){
                System.out.println("Philosopher "+i+" failed to take right fork , because Philosopher "+Main.Right(i)+" was eating");
            }
            if(state[i]!=Hungry){
                System.out.println("Philosopher "+i+" failed to take  forks , because Philosopher was not hungry");
            }
        }
    }
    void put_forks(int i){
        entLock.lock();
        state[i]=Thinking;
        System.out.println("Philosopher " + i + " is thinking "+f.format(LocalTime.now())+".\n");
        try {
            Thinking();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        test(Main.Left(i));
        test(Main.Right(i));
        entLock.unlock();
    }
    private void Thinking() throws InterruptedException {
            Thread.sleep(((int) (Math.random() * 200)));
    }

}
