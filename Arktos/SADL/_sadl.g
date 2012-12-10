header {
using System.Collections;
using Constructs;
}

options {
	language  =  "CSharp";
}
/***********************************************
	            Parser 
 ***********************************************/

class P extends Parser;
options {
	exportVocab=SADL;
	k=1;
}
{
// **************** Package Constructs ****************
// Collections for the actual objects. During the execution
// of the parser will NOT be initializad completely.

public SortedList AllScenarios = new SortedList();
public SortedList AllActivities = new SortedList();
public SortedList AllSchemas = new SortedList();
public SortedList AllRecordSets = new SortedList();

// ****************** Collections.cs ******************
// These collections will keep objects with information that cannot be loaded now
// The VB SADLloader will complete the initialization of the objects.

public SortedList AllSADLScenarios = new SortedList();
public SortedList AllSADLActivities = new SortedList();
public SortedList AllSADLRecordSets = new SortedList();
public SortedList AllSADLEdges = new SortedList();

// The 'Current*' pointers will hold the 'Constuct' objects

private Scenario CurrentScenario;
private Activity CurrentActivity;
private RecordSet CurrentRecordSet;
private Schema CurrentSchema;

// The 'Load*' pointers will hold the 'Collections.cs' objects

private SADLScenario LoadScenario;
private SADLActivity LoadActivity;
private SADLRecordSet LoadRecordSet;
private SADLEdge LoadEdge;

private string ScenarioName = "";
private string ActivityName = "";
private string SchemaName = "";
private string RecordSetName = "";
private string EdgeActivityName = "";

private string Buf1 = "";
private string Buf2 = "";
private string Buf3 = "";
public Boolean FoundError = false;

        public override void reportError(string s)
        {
            FoundError = true;
            base.reportError(s);
        }

        public override void reportError(RecognitionException ex)
        {
            FoundError = true;
            base.reportError(ex);
        }
}

// SADL Main structure
program
	:	(WS)*
	(	(create | alter)*
		T_SEMI (WS)*
	)*
	EOF
	;

// CREATE clause
create
	:
	T_CREATE WS 
           	(scenario | connection | activity | table | schema) (WS)*
      ;

//SCHEMA clause
schema
	:	T_SCHEMA	WS {SchemaName = LT(1).getText();
						CurrentSchema = new Schema(SchemaName);}
		T_NAME		WS
		T_WITH		WS
		(	{Buf1 = LT(1).getText();}	T_NAME WS
			{Buf2 = LT(1).getText(); CurrentSchema.AddAttribute(Buf1, Buf2);}	T_NAME WS)*
			{Console.WriteLine("Schema: " + SchemaName);
			AllSchemas.Add(AllSchemas.Count, CurrentSchema);}
;

// ALTER clause
alter
	:	T_ALTER		WS
		T_ACTIVITY	WS	{EdgeActivityName = LT(1).getText();}
		T_NAME		WS
		T_WITH		WS
		T_INPUT 	WS	
		({Buf1 = ""; Buf2 = ""; Buf3 = ""; Buf1 = LT(1).getText();}
		T_NAME	(T_DOT	{Buf2 = LT(1).getText();}	T_NAME)?	WS
		T_FOR	WS		{Buf3 = LT(1).getText();}	T_NAME	
		{LoadEdge = new SADLEdge(EdgeActivityName);
		LoadEdge.SetNodes(Buf1, EdgeActivityName); LoadEdge.SetEdges(Buf2, Buf3);
		AllSADLEdges.Add(AllSADLEdges.Count + 1, LoadEdge);}	WS)*

		T_OUTPUT	WS	
		({Buf1 = ""; Buf2 = ""; Buf3 = ""; Buf1 = LT(1).getText();}
		T_NAME	(T_DOT	{Buf2 = LT(1).getText();}	T_NAME)?	WS
		T_FOR	WS		{Buf3 = LT(1).getText();}	T_NAME
		{LoadEdge = new SADLEdge(EdgeActivityName);
		LoadEdge.SetNodes(EdgeActivityName, Buf1); LoadEdge.SetEdges(Buf3, Buf2);
		AllSADLEdges.Add(AllSADLEdges.Count + 1, LoadEdge);}	WS)*
      ;

// SCENARIO clause
scenario	
	: 	T_SCENARIO 		WS	{ScenarioName = LT(1).getText();
							CurrentScenario = new Scenario(ScenarioName);
							LoadScenario = new SADLScenario(ScenarioName);}
		T_NAME 			WS
		T_WITH			WS
		T_CONNECTIONS	WS
		T_NAME			(T_NAMELIST)* WS
		T_ACTIVITIES	WS	{Buf1 = LT(1).getText(); LoadScenario.AddActivity(Buf1);}
		T_NAME			(	{Buf1 = LT(1).getText().Substring(1); LoadScenario.AddActivity(Buf1);}	T_NAMELIST)*
		{AllScenarios.Add(AllScenarios.Count + 1, CurrentScenario);
		AllSADLScenarios.Add(AllSADLScenarios.Count + 1, LoadScenario);}
	;

// ACTIVITY clause
activity
	:	T_ACTIVITY	WS	{ActivityName = LT(1).getText();
						CurrentActivity = new Activity(ActivityName);
						LoadActivity = new SADLActivity(ActivityName);}
		T_NAME 		WS
		T_WITH		WS
		T_INPUT		WS	T_SCHEMA	WS
						({Buf1 = LT(1).getText();}	T_NAME	WS	T_WITH	WS	
						{Buf2 = LT(1).getText(); LoadActivity.SetInputEdge(Buf1, Buf2);}	T_NAME WS)*	
		T_OUTPUT	WS	T_SCHEMA	WS
						({Buf1 = LT(1).getText();}	T_NAME	WS	T_WITH	WS
						{Buf2 = LT(1).getText();  LoadActivity.SetOutputEdge(Buf1, Buf2);}	T_NAME	WS)*
		T_TYPE	WS
		(		T_UNIQUENESS WS T_VIOLATION	{Buf1 = "UniquenessViolation";}
			|	T_NULL WS T_EXISTENSE		{Buf1 = "NullExistence";}
			|	T_DOMAIN WS T_MISMATCH		{Buf1 = "DomainMismatch";}
			|	T_PRIMARY WS T_VIOLATION	{Buf1 = "PrimaryViolation";}
			|	T_REFERENCE WS T_VIOLATION	{Buf1 = "ReferenceViolation";}
			|	T_PUSH
			|	T_FORMAT WS T_MISMATCH
			|	{Buf1 = LT(1).getText();} T_NAME
		) {CurrentActivity.SetActivityType(Buf1);}		WS
		T_POLICY	WS {Buf1 = LT(1).getText(); CurrentActivity.SetActivityPolicy(Buf1);}
		(		T_IGNORE
			|	T_DELETE
			|	T_REPORT  WS T_TO WS {Buf1 = LT(1).getText();} (T_FILE | T_TABLE)
		) 		WS
		(	T_OUTPUT WS 
		 	({Buf2 = LT(1).getText();} T_NAME | 
		 	 {Buf2 = LT(1).getText(); Buf3 = Buf2.Substring(1, Buf2.Length - 2);
		 	 CurrentActivity.SetReportArgs(Buf1, Buf3);} T_QUOTE ) WS )?
		T_SEMANTICS 	WS
		{Buf1 = LT(1).getText(); CurrentActivity.SetActivitySemantics(Buf1);
		Console.WriteLine("Activity: " + ActivityName);
		AllActivities.Add(AllActivities.Count + 1, CurrentActivity);
		AllSADLActivities.Add(AllSADLActivities.Count + 1, LoadActivity);}
		T_QUOTE
	;

// CONNECTION clause
connection	
	: 	T_CONNECTION 	WS
		T_NAME 		WS
		T_WITH 		WS
		T_DATABASE 		WS
		T_QUOTE		WS
		T_ALIAS		WS
		T_NAME		WS

			(T_USER 		WS
			 T_NAME 		WS
			 T_PASSWORD 	WS
			 T_NAME 		WS)?

		T_DRIVER		WS
		T_QUOTE
	;

// TABLE clause, at least one attribute required
table
	:	T_TABLE	WS	{RecordSetName = LT(1).getText();
					CurrentRecordSet = new RecordSet(RecordSetName);
					LoadRecordSet = new SADLRecordSet(RecordSetName);}
		T_NAME	WS
		T_WITH	WS	
		T_DATA	WS	{Buf1 = LT(1).getText(); CurrentRecordSet.SetRecordSetType(Buf1);}
		(T_FILE | T_NAME)		WS	(T_FROM | T_TO)	WS 
		{Buf1 = LT(1).getText(); Buf2 = Buf1.Substring(1, Buf1.Length - 2);
		CurrentRecordSet.SetRecordSetSemantics(Buf1);}
		T_QUOTE WS	T_SCHEMA	WS
		{Buf1 = LT(1).getText(); LoadRecordSet.SetSchema(Buf1);}	T_NAME	WS
		{AllRecordSets.Add(AllRecordSets.Count + 1, CurrentRecordSet);
		Console.WriteLine("RecordSet: " + RecordSetName);
		AllSADLRecordSets.Add(AllSADLRecordSets.Count + 1, LoadRecordSet);}
	;

/***********************************************
	            Lexer 
 ***********************************************/

class L extends Lexer;
options {
	exportVocab=SADL;
	k=1;
	testLiterals = false;   // don't automatically test for literals
	caseSensitive = false;
	caseSensitiveLiterals = false;
}

// The token aka keywords of the language
tokens {
	// Genaral Keywords
	T_CREATE		= "create";
	T_WITH			= "with";

	// Connection Keywords
	T_CONNECTION	= "connection";
	T_DATABASE		= "database";
	T_ALIAS			= "alias";
	T_USER			= "user";
	T_PASSWORD		= "password";
	T_DRIVER		= "driver";

	// Scenario Keywords
	T_SCENARIO		= "scenario";
	T_CONNECTIONS	= "connections";
	T_ACTIVITIES	= "activities";

	// Activity Keywords	
	T_ACTIVITY		= "activity";
	T_TYPE			= "type";
	T_POLICY		= "policy";
	T_OUTPUT		= "output";
	T_SEMANTICS		= "semantics";

	// Activity Types Keywords
	T_UNIQUENESS 	= "uniqueness";
	T_NULL 			= "null";
	T_EXISTENSE		= "existence";
	T_DOMAIN 		= "domain";
	T_MISMATCH 		= "mismatch";
	T_PRIMARY 		= "primary";
	T_KEY 			= "key";
	T_REFERENCE		= "reference";
	T_VIOLATION		= "violation";
	T_PUSH 			= "push";
	T_FORMAT		= "format";

	// Activities Policy Keywords
	T_IGNORE 		= "ignore" ;
	T_DELETE 		= "delete" ;
	T_REPORT		= "report";
	T_TO			= "to";
	T_FILE			= "file";
	T_TABLE			= "table";
	
	// Alter Keywords
	T_ALTER		= "alter";
	T_INPUT		= "input";
	T_FOR			= "for";

	T_SCHEMA		= "schema";
	T_DATA		= "data";
	T_FROM		= "from";
}

// Name, e.g., for scenario names, tables etc
T_NAME	options {testLiterals=true;}
	:	('a'..'z') ('a'..'z'|'0'..'9'|'_'|'@')*
	;

// List of names
T_NAMELIST	
	options {ignore=WS;}
	: 	',' T_NAME
	;

// Quoted string. Everything between "".
T_QUOTE	:	'"'
		(	options {generateAmbigWarnings=false;}
		:	"\r\n"		{newline();}
		|	'\r'			{newline();}
		|	'\n'			{newline();}
		|	':'
		|	'\\'
		|	'.'
		|	'('
		|	')'
		|	'/'
		|	">"
		|	"<"
		|	"="
		|	'+'
		|	'-'
		|	'\''
		|	'['
		|	']'
		|	'$'
		|	'^'
		|	~('"')
		)+ 
		'"'
	;	

// Semicolon
T_SEMI 	: ';';
T_OPEN_BR	: '(';
T_CLOSE_BR	: ')';
T_DOT		: '.';

// White Space
WS	:	( options {generateAmbigWarnings=false;}
		:	' '
		|	'\t'
		|	'\n'		{newline();}
		|	"\r\n"	{newline();}
		|	'\r'		{newline();}
		)+
	;

// And C-like comments
COMMENT	:	
		"/*"
		(	options {generateAmbigWarnings=false;}
		:
			{ LA(2)!='/' }? '*'
		|	"\r\n"		{newline();}
		|	'\r'	 		{newline();}
		|	'\n'			{newline();}
		|	~('*'|'\n'|'\r')
		)*
		"*/" { $setType(Token.SKIP); }
	;

