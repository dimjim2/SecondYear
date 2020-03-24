%{
	#include<stdio.h>
	#include <stdbool.h>
	int count[5];
	int a1=0,a2=0,a3=0,a4=0,a5=0;
	int verb=0;
	int invalid=0;
	int count1=0;
	char *str;
	char nam[20];
	bool shmeia=true;
%}
%%
"σημείο" {
 printf("%s: είναι γεωμετρική οντότητα\n",yytext);
 a1+=1;
}
"ευθεία" {
 printf("%s: είναι γεωμετρική οντότητα\n",yytext);
 a2+=1;
}
"τρίγωνο" {
 printf("%s: είναι γεωμετρική οντότητα\n",yytext);
 a3+=1;
}
"τετράγωνο" {
 printf("%s: είναι γεωμετρική οντότητα\n",yytext);
 a4+=1;
}
"πεντάγωνο" {
 printf("%s: είναι γεωμετρική οντότητα\n",yytext);
 a5+=1;
}
Δίνεται	printf("%s: είναι ρήμα\n",yytext); verb+=1;
("Α"|"Β"|"Γ"|"Δ"|"Ε"){5}	{				
								count1+=1;
								printf("%s: είναι όνομα γεωμετρικής οντότητας\n",yytext);
								count[0]=1;
								str = (char *)calloc(strlen(yytext)+1, sizeof(char));
								strcpy(str,yytext);
								}
("Α"|"Β"|"Γ"|"Δ"|"Ε"){4}	{
								count1+=1;
								printf("%s: είναι όνομα γεωμετρικής οντότητας\n",yytext);
								count[1]=1;
								str = (char *)calloc(strlen(yytext)+1, sizeof(char));
								strcpy(str,yytext);
								}
("Α"|"Β"|"Γ"|"Δ"|"Ε"){3}	{
								count1+=1;
								printf("%s: είναι όνομα γεωμετρικής οντότητας\n",yytext);
								count[2]=1;
								str = (char *)calloc(strlen(yytext)+1, sizeof(char));
								strcpy(str,yytext);
								}
("Α"|"Β"|"Γ"|"Δ"|"Ε"|"Ζ"|"Η"|"Θ"){2}	{
								count1+=1;
								printf("%s: είναι όνομα γεωμετρικής οντότητας\n",yytext);
								count[3]=1;
								str = (char *)calloc(strlen(yytext)+1, sizeof(char));
								strcpy(str,yytext);
								}
("Α"|"Β"|"Γ"|"Δ"|"Ε"|"Ζ"|"Η"|"Θ"){1}	{
								count1+=1;
								printf("%s: είναι όνομα γεωμετρικής οντότητας\n",yytext);
								count[4]=1;
								str = (char *)calloc(strlen(yytext)+1, sizeof(char));
								strcpy(str,yytext);
								}
\n {
if(count1==1&&verb==1 &&invalid==0){
		if((count[0]==1 &&a5==1)||(count[1]==1&& a4==1)||(count[2]==1&& a3==1)||(count[3]==1&& a2==1)||(count[4]==1&& a1==1))
		{
			printf("\n");
			
			for(int i = 0; i < sizeof(str); i++){
				nam[i] = *(str + i);
				//printf("%c", *(str + i));
			}
			/*printf("\n");
			for(int i = 0; i < sizeof(str); i++){
				printf("%d", nam[i]);
			}
			printf("\n");
			*/
			for(int i=1;i<sizeof(str);i=i+2 )
			{
				if(shmeia==false)
				{
					break;
				}
			
				for(int j=i+2;j<sizeof(str);j=j+2)
				{
					if(nam[i]==0)
					{
						break;
					}
					if(nam[i]==nam[j])
					{	
						//printf("%d,%d", nam[i],nam[j]);
						printf("Επανάληψη γράμματος.\nΜη έγκυρη πρόταση!!!\n");
						shmeia=false;
						break;
					}
				}
			}
			
			
		}
		else
		{
			printf("Μάλλον έχεις μπερδέψει τα γεωμετρικά σύμβολα και το πλήθος των σημείων τους.\nΜη έγκυρη πρόταση!!!\n");
			shmeia=false;
		}
		if (shmeia==true)
		{
			printf("Όλα καλα.Έγκυρος συλλογισμός!!!\n");
		}
	}
else{
	printf("Λείπει το ρήμα ή δεν έχεις πληκρολόγησει αποδεκτό χαρακτήρα.\nΜη έγκυρη πρόταση!!!");
}
exit(0);
}
[ \t]+ 	
. {
	invalid+=1;
	printf("Μη αναγνωρίσιμος χαρακτήρας\n");
}
%%
 void main() {
 yylex();
 //printf("%d\n",count1);
} 
