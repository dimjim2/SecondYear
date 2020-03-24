#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

int main()
{
	//εισαγωγή συμβολοσειράς
	char str[101];
	char c;
	int count=0;
	int i=0;
	bool input=true;
	bool flag=false;
	printf("Enter your value:");
	while((c=getchar())!='\n')
	{
		count++;
		if((c!='x')&&(c!='y'))
		{
			input=false;
			printf("wrong input");
			break;
		}
		
		
		str[i]=c;
		++i;
		if (count>100)
		{
			input=false;
			printf("input can't be more than 100 characters");
			break;
		}
	}
	if(count==0)
	{
		printf("there's no input given");
		input=false;
	}
	if(input==true)
	{
	
		//υλοποίηση στοίβας
		//true=1
		//false=0
		//αρχικοποίηση στοίβας
		struct stack
		{
			char array[count];
			int top;
		};
		typedef struct stack STACK; 
		void ST_init (STACK *s){s->top=-1;}
		//κενή στοίβα
		int ST_empty(STACK s)
		{
			if(s.top==-1)
				return 1;
			else if(s.top!=-1)
				return 0;
		}
	
		// γεμάτη στοιβα
		int ST_full (STACK s){return s.top==count-1;}
	
		//push
		int ST_push (STACK *s,char x)
		{
			if(ST_full(*s))
				return 0;
			else
			{
				s->top++;
				s->array[s->top]=x;
				return 1;
			}
		}
	
		//pop
		int ST_pop(STACK *s,char *x)
		{
			if(ST_empty(*s))
				return 0;
			else
			{
				*x=s->array[s->top];
				s->top--;
				return 1;
			}
		}	
	

		str[i]='e';
		STACK stoiba;
		ST_init(&stoiba);
		printf("Stack    Status    Input         Function number\n");
		ST_push(&stoiba,'$');
		for (int j=0;j<=stoiba.top; j++)
		{
			printf("%c        ",stoiba.array[j]);
		}
		int K=1;
		printf("k%i        ",K);
		int number=0;
		
	
		if (i>15)
		{
			printf("%c%c%c...%c%c%c(%i)\n",str[0],str[1],str[2],str[i-3],str[i-2],str[i-1],count);
		}
		else
		{
			for(int j=0;j<i;j++)
			{
				printf("%c",str[j]);
			}
			printf("\n");
		}
	
		
	

		for(int j=0;j<=i;j++)
		{

			if(str[j]=='x')
			{
				//1)	p(k1,$,x)=(k1,$x)
				if(stoiba.array[stoiba.top]=='x'){number=2;}
				//2)	p(k1,x,x)=(k1,xx) 
				else if(stoiba.array[stoiba.top]=='$'){number=1;}
				if(ST_push(&stoiba,str[j]))
				{
					//εκτύπωση
					if(stoiba.top>10)
					{
						printf("%c%c...%c(%i)",stoiba.array[0],stoiba.array[1],stoiba.array[stoiba.top],stoiba.top);
					}
					else
					{
						for (int k=0;k<=stoiba.top; k++)
						{printf("%c",stoiba.array[k]);}
					}
					printf("	");
					printf("k%i",K);
					printf("	");
					if ((i-j)>15)
					{
						printf("%c%c%c...%c%c%c(%i)",str[j],str[j+1],str[j+2],str[i-3],str[i-2],str[i-1],(i-j));
					}else
					{
						for (int k=j+1;k<i;k++)
						{printf("%c",str[k]);}
					}
					printf("		%i",number);
					printf("\n");
				
				}
			
			}
			//3)	p(k1,x,y)=(k1,ε)
			if((str[j]=='y')&&(stoiba.array[stoiba.top]=='x'))
			{
				//εκτύπωση
				char x='x';
				if(ST_pop(&stoiba,&x))
				{
					if(stoiba.top>10)
					{
						printf("%c%c...%c(%i)",stoiba.array[0],stoiba.array[1],stoiba.array[stoiba.top],stoiba.top);
					}
					else
					{
						for (int k=0;k<=stoiba.top; k++)
						{printf("%c",stoiba.array[k]);}
					}
					printf("	");
					printf("k%i",K);
					printf("	");
					if ((i-j)>15)
					{
						printf("%c%c%c...%c%c%c(%i)",str[j],str[j+1],str[j+2],str[i-3],str[i-2],str[i-1],(i-j));
					}else
					{
						for (int k=j+1;k<i;k++)
						{printf("%c",str[k]);}
					}
					number=3;
					printf("		%i",number);
					printf("\n");
				
				}
			}else if ((str[j]=='y')&&(stoiba.array[stoiba.top]=='$'))
			{
				printf("\n Illegal Entry");
				flag=false;
				break;
			}
			//4)	p(k1,$,ε)=(k2,ε)
			if((str[j]=='e')&&(j==i)&&(stoiba.array[stoiba.top]=='$'))
			{
				//εκτύπωση
				if(stoiba.top>10)
				{
					printf("%c%c...%c(%i)",stoiba.array[0],stoiba.array[1],stoiba.array[stoiba.top],stoiba.top);
				}
				else
				{
					for (int k=0;k<=stoiba.top; k++)
					{printf("%c",stoiba.array[k]);}
				}
				printf("	");
				K=2;
				printf("k%i",K);
				printf("	");
				if ((i-j)>15)
				{
					printf("%c%c%c...%c%c%c(%i)",str[j],str[j+1],str[j+2],str[i-3],str[i-2],str[i-1],(i-j));
				}else
				{
					for (int k=j+1;k<i;k++)
					{printf("%c",str[k]);}
				}
				for (int k=j+1;k<=i;k++)
				{printf("%c",str[k]);}
				number=4;
				printf("		%i",number);
				printf("\n");
				flag=true;
			
			}
			
			
			
		}
	
	
	}
	printf("\n");
	if(flag==true){printf("YES!!");}else{printf("NO");}
	
 
}