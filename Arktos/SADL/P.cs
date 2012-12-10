// $ANTLR 2.7.5 (20050201): "_sadl.g" -> "P.cs"$

using System.Collections;
using Constructs;

	// Generate the header common to all output files.
	using System;
	
	using TokenBuffer              = antlr.TokenBuffer;
	using TokenStreamException     = antlr.TokenStreamException;
	using TokenStreamIOException   = antlr.TokenStreamIOException;
	using ANTLRException           = antlr.ANTLRException;
	using LLkParser = antlr.LLkParser;
	using Token                    = antlr.Token;
	using IToken                   = antlr.IToken;
	using TokenStream              = antlr.TokenStream;
	using RecognitionException     = antlr.RecognitionException;
	using NoViableAltException     = antlr.NoViableAltException;
	using MismatchedTokenException = antlr.MismatchedTokenException;
	using SemanticException        = antlr.SemanticException;
	using ParserSharedInputState   = antlr.ParserSharedInputState;
	using BitSet                   = antlr.collections.impl.BitSet;
	
/***********************************************
	            Parser 
 ***********************************************/
	public 	class P : antlr.LLkParser
	{
		public const int EOF = 1;
		public const int NULL_TREE_LOOKAHEAD = 3;
		public const int WS = 4;
		public const int T_SEMI = 5;
		public const int T_CREATE = 6;
		public const int T_SCHEMA = 7;
		public const int T_NAME = 8;
		public const int T_WITH = 9;
		public const int T_ALTER = 10;
		public const int T_ACTIVITY = 11;
		public const int T_INPUT = 12;
		public const int T_DOT = 13;
		public const int T_FOR = 14;
		public const int T_OUTPUT = 15;
		public const int T_SCENARIO = 16;
		public const int T_CONNECTIONS = 17;
		public const int T_NAMELIST = 18;
		public const int T_ACTIVITIES = 19;
		public const int T_TYPE = 20;
		public const int T_UNIQUENESS = 21;
		public const int T_VIOLATION = 22;
		public const int T_NULL = 23;
		public const int T_EXISTENSE = 24;
		public const int T_DOMAIN = 25;
		public const int T_MISMATCH = 26;
		public const int T_PRIMARY = 27;
		public const int T_REFERENCE = 28;
		public const int T_PUSH = 29;
		public const int T_FORMAT = 30;
		public const int T_POLICY = 31;
		public const int T_IGNORE = 32;
		public const int T_DELETE = 33;
		public const int T_REPORT = 34;
		public const int T_TO = 35;
		public const int T_FILE = 36;
		public const int T_TABLE = 37;
		public const int T_QUOTE = 38;
		public const int T_SEMANTICS = 39;
		public const int T_CONNECTION = 40;
		public const int T_DATABASE = 41;
		public const int T_ALIAS = 42;
		public const int T_USER = 43;
		public const int T_PASSWORD = 44;
		public const int T_DRIVER = 45;
		public const int T_DATA = 46;
		public const int T_FROM = 47;
		public const int T_KEY = 48;
		public const int T_OPEN_BR = 49;
		public const int T_CLOSE_BR = 50;
		public const int COMMENT = 51;
		
		
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
		
		protected void initialize()
		{
			tokenNames = tokenNames_;
		}
		
		
		protected P(TokenBuffer tokenBuf, int k) : base(tokenBuf, k)
		{
			initialize();
		}
		
		public P(TokenBuffer tokenBuf) : this(tokenBuf,1)
		{
		}
		
		protected P(TokenStream lexer, int k) : base(lexer,k)
		{
			initialize();
		}
		
		public P(TokenStream lexer) : this(lexer,1)
		{
		}
		
		public P(ParserSharedInputState state) : base(state,1)
		{
			initialize();
		}
		
	public void program() //throws RecognitionException, TokenStreamException
{
		
		
		try {      // for error handling
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==WS))
					{
						match(WS);
					}
					else
					{
						goto _loop3_breakloop;
					}
					
				}
_loop3_breakloop:				;
			}    // ( ... )*
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==T_SEMI||LA(1)==T_CREATE||LA(1)==T_ALTER))
					{
						{    // ( ... )*
							for (;;)
							{
								switch ( LA(1) )
								{
								case T_CREATE:
								{
									create();
									break;
								}
								case T_ALTER:
								{
									alter();
									break;
								}
								default:
								{
									goto _loop6_breakloop;
								}
								 }
							}
_loop6_breakloop:							;
						}    // ( ... )*
						match(T_SEMI);
						{    // ( ... )*
							for (;;)
							{
								if ((LA(1)==WS))
								{
									match(WS);
								}
								else
								{
									goto _loop8_breakloop;
								}
								
							}
_loop8_breakloop:							;
						}    // ( ... )*
					}
					else
					{
						goto _loop9_breakloop;
					}
					
				}
_loop9_breakloop:				;
			}    // ( ... )*
			match(Token.EOF_TYPE);
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_0_);
		}
	}
	
	public void create() //throws RecognitionException, TokenStreamException
{
		
		
		try {      // for error handling
			match(T_CREATE);
			match(WS);
			{
				switch ( LA(1) )
				{
				case T_SCENARIO:
				{
					scenario();
					break;
				}
				case T_CONNECTION:
				{
					connection();
					break;
				}
				case T_ACTIVITY:
				{
					activity();
					break;
				}
				case T_TABLE:
				{
					table();
					break;
				}
				case T_SCHEMA:
				{
					schema();
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==WS))
					{
						match(WS);
					}
					else
					{
						goto _loop13_breakloop;
					}
					
				}
_loop13_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_1_);
		}
	}
	
	public void alter() //throws RecognitionException, TokenStreamException
{
		
		
		try {      // for error handling
			match(T_ALTER);
			match(WS);
			match(T_ACTIVITY);
			match(WS);
			EdgeActivityName = LT(1).getText();
			match(T_NAME);
			match(WS);
			match(T_WITH);
			match(WS);
			match(T_INPUT);
			match(WS);
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==T_NAME))
					{
						Buf1 = ""; Buf2 = ""; Buf3 = ""; Buf1 = LT(1).getText();
						match(T_NAME);
						{
							switch ( LA(1) )
							{
							case T_DOT:
							{
								match(T_DOT);
								Buf2 = LT(1).getText();
								match(T_NAME);
								break;
							}
							case WS:
							{
								break;
							}
							default:
							{
								throw new NoViableAltException(LT(1), getFilename());
							}
							 }
						}
						match(WS);
						match(T_FOR);
						match(WS);
						Buf3 = LT(1).getText();
						match(T_NAME);
						LoadEdge = new SADLEdge(EdgeActivityName);
								LoadEdge.SetNodes(Buf1, EdgeActivityName); LoadEdge.SetEdges(Buf2, Buf3);
								AllSADLEdges.Add(AllSADLEdges.Count + 1, LoadEdge);
						match(WS);
					}
					else
					{
						goto _loop20_breakloop;
					}
					
				}
_loop20_breakloop:				;
			}    // ( ... )*
			match(T_OUTPUT);
			match(WS);
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==T_NAME))
					{
						Buf1 = ""; Buf2 = ""; Buf3 = ""; Buf1 = LT(1).getText();
						match(T_NAME);
						{
							switch ( LA(1) )
							{
							case T_DOT:
							{
								match(T_DOT);
								Buf2 = LT(1).getText();
								match(T_NAME);
								break;
							}
							case WS:
							{
								break;
							}
							default:
							{
								throw new NoViableAltException(LT(1), getFilename());
							}
							 }
						}
						match(WS);
						match(T_FOR);
						match(WS);
						Buf3 = LT(1).getText();
						match(T_NAME);
						LoadEdge = new SADLEdge(EdgeActivityName);
								LoadEdge.SetNodes(EdgeActivityName, Buf1); LoadEdge.SetEdges(Buf3, Buf2);
								AllSADLEdges.Add(AllSADLEdges.Count + 1, LoadEdge);
						match(WS);
					}
					else
					{
						goto _loop23_breakloop;
					}
					
				}
_loop23_breakloop:				;
			}    // ( ... )*
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_1_);
		}
	}
	
	public void scenario() //throws RecognitionException, TokenStreamException
{
		
		
		try {      // for error handling
			match(T_SCENARIO);
			match(WS);
			ScenarioName = LT(1).getText();
										CurrentScenario = new Scenario(ScenarioName);
										LoadScenario = new SADLScenario(ScenarioName);
			match(T_NAME);
			match(WS);
			match(T_WITH);
			match(WS);
			match(T_CONNECTIONS);
			match(WS);
			match(T_NAME);
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==T_NAMELIST))
					{
						match(T_NAMELIST);
					}
					else
					{
						goto _loop26_breakloop;
					}
					
				}
_loop26_breakloop:				;
			}    // ( ... )*
			match(WS);
			match(T_ACTIVITIES);
			match(WS);
			Buf1 = LT(1).getText(); LoadScenario.AddActivity(Buf1);
			match(T_NAME);
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==T_NAMELIST))
					{
						Buf1 = LT(1).getText().Substring(1); LoadScenario.AddActivity(Buf1);
						match(T_NAMELIST);
					}
					else
					{
						goto _loop28_breakloop;
					}
					
				}
_loop28_breakloop:				;
			}    // ( ... )*
			AllScenarios.Add(AllScenarios.Count + 1, CurrentScenario);
					AllSADLScenarios.Add(AllSADLScenarios.Count + 1, LoadScenario);
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_2_);
		}
	}
	
	public void connection() //throws RecognitionException, TokenStreamException
{
		
		
		try {      // for error handling
			match(T_CONNECTION);
			match(WS);
			match(T_NAME);
			match(WS);
			match(T_WITH);
			match(WS);
			match(T_DATABASE);
			match(WS);
			match(T_QUOTE);
			match(WS);
			match(T_ALIAS);
			match(WS);
			match(T_NAME);
			match(WS);
			{
				switch ( LA(1) )
				{
				case T_USER:
				{
					match(T_USER);
					match(WS);
					match(T_NAME);
					match(WS);
					match(T_PASSWORD);
					match(WS);
					match(T_NAME);
					match(WS);
					break;
				}
				case T_DRIVER:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(T_DRIVER);
			match(WS);
			match(T_QUOTE);
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_2_);
		}
	}
	
	public void activity() //throws RecognitionException, TokenStreamException
{
		
		
		try {      // for error handling
			match(T_ACTIVITY);
			match(WS);
			ActivityName = LT(1).getText();
									CurrentActivity = new Activity(ActivityName);
									LoadActivity = new SADLActivity(ActivityName);
			match(T_NAME);
			match(WS);
			match(T_WITH);
			match(WS);
			match(T_INPUT);
			match(WS);
			match(T_SCHEMA);
			match(WS);
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==T_NAME))
					{
						Buf1 = LT(1).getText();
						match(T_NAME);
						match(WS);
						match(T_WITH);
						match(WS);
						Buf2 = LT(1).getText(); LoadActivity.SetInputEdge(Buf1, Buf2);
						match(T_NAME);
						match(WS);
					}
					else
					{
						goto _loop31_breakloop;
					}
					
				}
_loop31_breakloop:				;
			}    // ( ... )*
			match(T_OUTPUT);
			match(WS);
			match(T_SCHEMA);
			match(WS);
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==T_NAME))
					{
						Buf1 = LT(1).getText();
						match(T_NAME);
						match(WS);
						match(T_WITH);
						match(WS);
						Buf2 = LT(1).getText();  LoadActivity.SetOutputEdge(Buf1, Buf2);
						match(T_NAME);
						match(WS);
					}
					else
					{
						goto _loop33_breakloop;
					}
					
				}
_loop33_breakloop:				;
			}    // ( ... )*
			match(T_TYPE);
			match(WS);
			{
				switch ( LA(1) )
				{
				case T_UNIQUENESS:
				{
					match(T_UNIQUENESS);
					match(WS);
					match(T_VIOLATION);
					Buf1 = "UniquenessViolation";
					break;
				}
				case T_NULL:
				{
					match(T_NULL);
					match(WS);
					match(T_EXISTENSE);
					Buf1 = "NullExistence";
					break;
				}
				case T_DOMAIN:
				{
					match(T_DOMAIN);
					match(WS);
					match(T_MISMATCH);
					Buf1 = "DomainMismatch";
					break;
				}
				case T_PRIMARY:
				{
					match(T_PRIMARY);
					match(WS);
					match(T_VIOLATION);
					Buf1 = "PrimaryViolation";
					break;
				}
				case T_REFERENCE:
				{
					match(T_REFERENCE);
					match(WS);
					match(T_VIOLATION);
					Buf1 = "ReferenceViolation";
					break;
				}
				case T_PUSH:
				{
					match(T_PUSH);
					break;
				}
				case T_FORMAT:
				{
					match(T_FORMAT);
					match(WS);
					match(T_MISMATCH);
					break;
				}
				case T_NAME:
				{
					Buf1 = LT(1).getText();
					match(T_NAME);
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			CurrentActivity.SetActivityType(Buf1);
			match(WS);
			match(T_POLICY);
			match(WS);
			Buf1 = LT(1).getText(); CurrentActivity.SetActivityPolicy(Buf1);
			{
				switch ( LA(1) )
				{
				case T_IGNORE:
				{
					match(T_IGNORE);
					break;
				}
				case T_DELETE:
				{
					match(T_DELETE);
					break;
				}
				case T_REPORT:
				{
					match(T_REPORT);
					match(WS);
					match(T_TO);
					match(WS);
					Buf1 = LT(1).getText();
					{
						switch ( LA(1) )
						{
						case T_FILE:
						{
							match(T_FILE);
							break;
						}
						case T_TABLE:
						{
							match(T_TABLE);
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(WS);
			{
				switch ( LA(1) )
				{
				case T_OUTPUT:
				{
					match(T_OUTPUT);
					match(WS);
					{
						switch ( LA(1) )
						{
						case T_NAME:
						{
							Buf2 = LT(1).getText();
							match(T_NAME);
							break;
						}
						case T_QUOTE:
						{
							Buf2 = LT(1).getText(); Buf3 = Buf2.Substring(1, Buf2.Length - 2);
									 	 CurrentActivity.SetReportArgs(Buf1, Buf3);
							match(T_QUOTE);
							break;
						}
						default:
						{
							throw new NoViableAltException(LT(1), getFilename());
						}
						 }
					}
					match(WS);
					break;
				}
				case T_SEMANTICS:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(T_SEMANTICS);
			match(WS);
			Buf1 = LT(1).getText(); CurrentActivity.SetActivitySemantics(Buf1);
					Console.WriteLine("Activity: " + ActivityName);
					AllActivities.Add(AllActivities.Count + 1, CurrentActivity);
					AllSADLActivities.Add(AllSADLActivities.Count + 1, LoadActivity);
			match(T_QUOTE);
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_2_);
		}
	}
	
	public void table() //throws RecognitionException, TokenStreamException
{
		
		
		try {      // for error handling
			match(T_TABLE);
			match(WS);
			RecordSetName = LT(1).getText();
								CurrentRecordSet = new RecordSet(RecordSetName);
								LoadRecordSet = new SADLRecordSet(RecordSetName);
			match(T_NAME);
			match(WS);
			match(T_WITH);
			match(WS);
			match(T_DATA);
			match(WS);
			Buf1 = LT(1).getText(); CurrentRecordSet.SetRecordSetType(Buf1);
			{
				switch ( LA(1) )
				{
				case T_FILE:
				{
					match(T_FILE);
					break;
				}
				case T_NAME:
				{
					match(T_NAME);
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(WS);
			{
				switch ( LA(1) )
				{
				case T_FROM:
				{
					match(T_FROM);
					break;
				}
				case T_TO:
				{
					match(T_TO);
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				 }
			}
			match(WS);
			Buf1 = LT(1).getText(); Buf2 = Buf1.Substring(1, Buf1.Length - 2);
					CurrentRecordSet.SetRecordSetSemantics(Buf1);
			match(T_QUOTE);
			match(WS);
			match(T_SCHEMA);
			match(WS);
			Buf1 = LT(1).getText(); LoadRecordSet.SetSchema(Buf1);
			match(T_NAME);
			match(WS);
			AllRecordSets.Add(AllRecordSets.Count + 1, CurrentRecordSet);
					Console.WriteLine("RecordSet: " + RecordSetName);
					AllSADLRecordSets.Add(AllSADLRecordSets.Count + 1, LoadRecordSet);
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_2_);
		}
	}
	
	public void schema() //throws RecognitionException, TokenStreamException
{
		
		
		try {      // for error handling
			match(T_SCHEMA);
			match(WS);
			SchemaName = LT(1).getText();
									CurrentSchema = new Schema(SchemaName);
			match(T_NAME);
			match(WS);
			match(T_WITH);
			match(WS);
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==T_NAME))
					{
						Buf1 = LT(1).getText();
						match(T_NAME);
						match(WS);
						Buf2 = LT(1).getText(); CurrentSchema.AddAttribute(Buf1, Buf2);
						match(T_NAME);
						match(WS);
					}
					else
					{
						goto _loop16_breakloop;
					}
					
				}
_loop16_breakloop:				;
			}    // ( ... )*
			Console.WriteLine("Schema: " + SchemaName);
						AllSchemas.Add(AllSchemas.Count, CurrentSchema);
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_2_);
		}
	}
	
	private void initializeFactory()
	{
	}
	
	public static readonly string[] tokenNames_ = new string[] {
		@"""<0>""",
		@"""EOF""",
		@"""<2>""",
		@"""NULL_TREE_LOOKAHEAD""",
		@"""WS""",
		@"""T_SEMI""",
		@"""create""",
		@"""schema""",
		@"""T_NAME""",
		@"""with""",
		@"""alter""",
		@"""activity""",
		@"""input""",
		@"""T_DOT""",
		@"""for""",
		@"""output""",
		@"""scenario""",
		@"""connections""",
		@"""T_NAMELIST""",
		@"""activities""",
		@"""type""",
		@"""uniqueness""",
		@"""violation""",
		@"""null""",
		@"""existence""",
		@"""domain""",
		@"""mismatch""",
		@"""primary""",
		@"""reference""",
		@"""push""",
		@"""format""",
		@"""policy""",
		@"""ignore""",
		@"""delete""",
		@"""report""",
		@"""to""",
		@"""file""",
		@"""table""",
		@"""T_QUOTE""",
		@"""semantics""",
		@"""connection""",
		@"""database""",
		@"""alias""",
		@"""user""",
		@"""password""",
		@"""driver""",
		@"""data""",
		@"""from""",
		@"""key""",
		@"""T_OPEN_BR""",
		@"""T_CLOSE_BR""",
		@"""COMMENT"""
	};
	
	private static long[] mk_tokenSet_0_()
	{
		long[] data = { 2L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_0_ = new BitSet(mk_tokenSet_0_());
	private static long[] mk_tokenSet_1_()
	{
		long[] data = { 1120L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_1_ = new BitSet(mk_tokenSet_1_());
	private static long[] mk_tokenSet_2_()
	{
		long[] data = { 1136L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_2_ = new BitSet(mk_tokenSet_2_());
	
}
