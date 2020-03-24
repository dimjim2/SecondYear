import java.util.Random;
//Import random class
public class Syntax {
    Random r=new Random();
    /*Initial content of the String s will be "Ex" that means execution
    which marks the start of the program
     */
    String s="Ex";
    //Mutable string pr initialized to s
    StringBuilder pr=new StringBuilder(s);
    public void Expression(){
        //Returns the index in which we find for the first time the substring "Ex"
        int index=pr.indexOf("Ex");
        //Using stringBuilder pr we replace the characters of the mutable string starting from index
        // and ending in index+2 with characters in the specified string "(SE)"
        pr.replace(index,index+2,"(SE)");
        //We convert the stringBuilder pr to string and set its value to s
        s=pr.toString();
        System.out.println(pr.toString());
    }
    public void SubExpression(){
        //Returns the index in which we find for the first time the substring "SE"
        int index=pr.indexOf("SE");
        //Replace the substring of pr according to  index with "E1E2"
        pr.replace(index, index + 2, "E1E2");
        s=pr.toString();
        System.out.println(pr.toString());
    }
    public void Element2(){
        //We generate a random number between 1-3 and set the int result to choice
        int choice=r.nextInt(3)+1;
        // //Returns the index in which we find for the first time the substring "E2" in pr  and set it to index
        int index=pr.indexOf("E2");
        //Based on choice replace the substring of pr according to index (starting point,ending point)
        // with "-SE","+SE" or "" (nothing)
        if(choice==1){
            pr.replace(index,index+2,"-SE");
        }
        else if(choice==2){
            pr.replace(index,index+2,"+SE");
        }
        else{
            pr.replace(index,index+2,"");
        }
        s=pr.toString();
        System.out.println(pr.toString());
    }
    public void Element1(){
        //We generate a random number between 1-2 and set the int result to choice
        int choice=r.nextInt(2)+1;
        //Returns the index in which we find for the first time the substring "E1" in pr and set it to index
        int index=pr.indexOf("E1");
        //Based on choice replace the substring of pr according to index (starting point,ending point)
        // with "v" or "Ex"
        if(choice==1){
            pr.replace(index,index+2,"v");
        }
        else{
            pr.replace(index,index+2,"Ex");
        }
        s=pr.toString();
        System.out.println(pr.toString());
    }
}
