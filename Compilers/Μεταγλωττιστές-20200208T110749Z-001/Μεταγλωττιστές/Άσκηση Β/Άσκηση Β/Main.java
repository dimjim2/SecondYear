public class Main {
    public static void main(String...args){
        boolean z=true;
        //Counts terminal characters
        int count;
        //Steps counts the number of replacements based on grammar production rules in  m.s
        int steps=-1;
        //Make an instance m of Class Syntax
        Syntax m=new Syntax();
        //Print the initial contents of field s in m
        System.out.println(m.s);
        while(z){
            count=0;
            steps+=1;
            for(int i=0;i<m.s.length()&& steps<=70;i++){
                //Covert to char ch
                char ch=m.s.charAt(i);
                //if char is terminal based on grammar rules increment count
                if(ch=='v'||ch=='-'||ch=='+'||ch=='('||ch==')'){
                    count+=1;
                }
                //if char is 'E' and followed by 'x' then we have substring "Ex"
                // and we must do replacement based on rule Expression
                else if(ch=='E' && m.s.charAt(i+1)=='x'){
                    m.Expression();
                    break;
                }
                //if char is 'S' and followed by 'E' then we have substring "SE"
                // and we must do replacement based on rule SubExpression
                else if(ch=='S' && m.s.charAt(i+1)=='E'){
                    m.SubExpression();
                    break;
                }
                //if char is 'E' and followed by '1' then we have substring "E1"
                // and we must do replacement based on rule Element1
                else if(ch=='E' && m.s.charAt(i+1)=='1'){
                    m.Element1();
                    break;
                }
                //if char is 'E' and followed by '2' then we have substring "E2"
                // and we must do replacement based on rule Element2
                else if(ch=='E' && m.s.charAt(i+1)=='2') {
                    m.Element2();
                    break;
                }
                //Error
                else {
                    System.out.println("Something went wrong");
                    break;
                }
            }
            //All the char at field s of m are terminal so loop must end
            if(count==m.s.length()){
                z=false;
            }
            //If the string generator has not completed its operation the string s of m is composed
            // of non terminal characters then terminate in order to confront the recursion problem
            if(steps>70){
                System.out.println("Terminated because of big recursion!!!");
                z=false;
            }
        }
        System.out.println("Steps"+" "+steps);
        }
    }
