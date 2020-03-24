%{
int invalid=0;
int part=0;
%}
NUMBER [0-9]+
VARIABLE [A-Za-z]([A-Za-z0-9]*)
OPERATOR "+"|"-"|"*"|"%"|"/" 

%%
({VARIABLE})"="({NUMBER}|{VARIABLE})({OPERATOR}({VARIABLE}|{NUMBER}))+";" {
printf("Acknowledged part of input:%s\n",yytext,"\n"); 
part+=1;

}
\n {if(invalid==0 && part>0){printf("Valid expression\n");}else if(part>0) {printf("\nNot valid but a part of expression is valid\n");}else{printf("\nNot valid at all\n");};exit(0);}
. {
invalid+=1;
}
%%
main( argc, argv )
int argc;
char **argv;
/*yyin is a variable of the type FILE* and points to the input file. yyin is defined by LEX automatically. If the programmer assigns an input file to yyin in the auxiliary functions section, then yyin is set to point to that file. Otherwise LEX assigns yyin to stdin(console input)*/
    {
    ++argv, --argc;  /* skip over program name */
    printf("Enter the arithmetic expression:\n");
    if ( argc > 0 )
            yyin = fopen( argv[0], "r" );
    else
/*Console input*/
            yyin = stdin;
    yylex();
    }
