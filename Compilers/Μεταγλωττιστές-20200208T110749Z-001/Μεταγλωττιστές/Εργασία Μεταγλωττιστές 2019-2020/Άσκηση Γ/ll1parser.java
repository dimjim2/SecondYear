import java.io.*; 
import java.util.*; 

//Input = ((α-β)*(β+α))

public class ll1parser {
	
	public static void main (String[] args) {
		System.out.println("LL(1) parser:");
		Scanner scanner = new Scanner(System.in);
		System.out.println("Enter string to be parsed:");
		String input = scanner.nextLine() + "$";
		System.out.println("String to be parsed is:" + input);
		int inputptr = 0;
		Stack <String> stack = new Stack<String>();
		String[][] ptable = {
			{"(X)", null, null, null, null, null, null},
			{"YZ", null, "YZ", "YZ", null, null, null},
			{"S", null, "α", "β", null, null, null},
			{null, "", null, null, "*X", "-X", "+X"}
		};
		String[] terminals = {"(", ")", "α", "β", "*", "-", "+", "$"};
		String[] nonterms = {"S", "X", "Y", "Z"};
		List<String> terlist = Arrays.asList(terminals);
		List<String> nonterlist = Arrays.asList(nonterms);
		System.out.print("Displaying terminal characters: ");
		for(int i = 0; i < 8; i++){
			System.out.print(terminals[i] + " ");
		}
		System.out.println();
		System.out.print("Displaying non-terminal characters: ");
		for(int i = 0; i < 4; i++){
			System.out.print(nonterms[i] + " ");
		}
		System.out.println();
		System.out.println("Displaying parsing table: ");
		System.out.println("---------------------------------------------------");
		for(int i = 0; i < 4; i++){
			System.out.print("| ");
			for(int j = 0; j < 7; j++){
				System.out.print(ptable[i][j]);
				System.out.print(" | ");
			}
			System.out.println(" ");
			System.out.println("---------------------------------------------------");
		}
		int counter = 0;
		stack.push("$");
		stack.push("S");
		System.out.println("Stack pointer: " + stack.peek());
		System.out.println("Input pointer: " + Character.toString(input.charAt(inputptr)));
		System.out.println("--------------------------------------------");
		do{
			try{
			counter++;
			System.out.println("Repetition number: " + counter);
			if(terlist.contains(stack.peek()) || stack.peek() == "$"){
				if(stack.peek().equals(Character.toString(input.charAt(inputptr)))){
					System.out.println(stack.pop() + " is popped out of the stack");
					inputptr++;
				}else{
					System.out.println("Error!!!");
					break;
				}
			}else{
				int row = nonterlist.indexOf(stack.peek());
				if (row == -1){
					System.out.println("No non terminal character");
					break;
				}
				int col = terlist.indexOf(Character.toString(input.charAt(inputptr)));
				if (col == -1){
					System.out.println("No terminal character");
					break;
				}
				if (row != -1 && col != -1){
					String rule = ptable[row][col];
					if (rule == null){
						System.out.println("Input not recognised by grammar!!!");
						break;
					}
					System.out.println("Rule is: " + stack.peek() + "->" + rule);
					stack.pop();
					String revprod = "";
					int length = rule.length() - 1;
					int pos = 0;
					for(int i = length; i >= 0; i--){
						char c = rule.charAt(i);
						revprod = addChar(revprod, c, pos);
						pos++;
						stack.push(Character.toString(c));
						System.out.println("Revprod is: " + revprod);
					}
					System.out.println("top on stack: " + stack.peek());
					System.out.println("Input pointer: " + Character.toString(input.charAt(inputptr)));
				}else{
				System.out.println("Error!!!");
				break;
				}
			}
			System.out.println("--------------------------------------------");
			}catch(Exception e){
				System.out.println(e.getMessage());
				break;
			}
		}	
		while (stack.peek() != "$");
		System.out.println("top on stack: " + stack.peek());
		System.out.println("Input pointer: " + Character.toString(input.charAt(inputptr)));
		System.out.println("LL1 parser shuting down ... ");
	}
	public static String addChar(String str, char ch, int position) { //used to build the reverse production of the rule in order to get pushed to the stack form right to left
	StringBuilder sb = new StringBuilder(str);
	sb.insert(position, ch);
	return sb.toString();
	}
}
