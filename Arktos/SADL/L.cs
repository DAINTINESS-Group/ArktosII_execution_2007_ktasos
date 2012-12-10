// $ANTLR 2.7.5 (20050201): "_sadl.g" -> "L.cs"$

using System.Collections;
using Constructs;

	// Generate header specific to lexer CSharp file
	using System;
	using Stream                          = System.IO.Stream;
	using TextReader                      = System.IO.TextReader;
	using Hashtable                       = System.Collections.Hashtable;
	using Comparer                        = System.Collections.Comparer;
	using CaseInsensitiveHashCodeProvider = System.Collections.CaseInsensitiveHashCodeProvider;
	using CaseInsensitiveComparer         = System.Collections.CaseInsensitiveComparer;
	
	using TokenStreamException            = antlr.TokenStreamException;
	using TokenStreamIOException          = antlr.TokenStreamIOException;
	using TokenStreamRecognitionException = antlr.TokenStreamRecognitionException;
	using CharStreamException             = antlr.CharStreamException;
	using CharStreamIOException           = antlr.CharStreamIOException;
	using ANTLRException                  = antlr.ANTLRException;
	using CharScanner                     = antlr.CharScanner;
	using InputBuffer                     = antlr.InputBuffer;
	using ByteBuffer                      = antlr.ByteBuffer;
	using CharBuffer                      = antlr.CharBuffer;
	using Token                           = antlr.Token;
	using IToken                          = antlr.IToken;
	using CommonToken                     = antlr.CommonToken;
	using SemanticException               = antlr.SemanticException;
	using RecognitionException            = antlr.RecognitionException;
	using NoViableAltForCharException     = antlr.NoViableAltForCharException;
	using MismatchedCharException         = antlr.MismatchedCharException;
	using TokenStream                     = antlr.TokenStream;
	using LexerSharedInputState           = antlr.LexerSharedInputState;
	using BitSet                          = antlr.collections.impl.BitSet;
	
/***********************************************
	            Lexer 
 ***********************************************/
	public 	class L : antlr.CharScanner	, TokenStream
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
		
		public L(Stream ins) : this(new ByteBuffer(ins))
		{
		}
		
		public L(TextReader r) : this(new CharBuffer(r))
		{
		}
		
		public L(InputBuffer ib)		 : this(new LexerSharedInputState(ib))
		{
		}
		
		public L(LexerSharedInputState state) : base(state)
		{
			initialize();
		}
		private void initialize()
		{
			caseSensitiveLiterals = false;
			setCaseSensitive(false);
			literals = new Hashtable(100, (float) 0.4, CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			literals.Add("to", 35);
			literals.Add("violation", 22);
			literals.Add("for", 14);
			literals.Add("semantics", 39);
			literals.Add("ignore", 32);
			literals.Add("create", 6);
			literals.Add("alter", 10);
			literals.Add("driver", 45);
			literals.Add("input", 12);
			literals.Add("existence", 24);
			literals.Add("uniqueness", 21);
			literals.Add("key", 48);
			literals.Add("activity", 11);
			literals.Add("report", 34);
			literals.Add("user", 43);
			literals.Add("data", 46);
			literals.Add("policy", 31);
			literals.Add("database", 41);
			literals.Add("output", 15);
			literals.Add("schema", 7);
			literals.Add("reference", 28);
			literals.Add("type", 20);
			literals.Add("alias", 42);
			literals.Add("null", 23);
			literals.Add("push", 29);
			literals.Add("password", 44);
			literals.Add("connections", 17);
			literals.Add("connection", 40);
			literals.Add("scenario", 16);
			literals.Add("from", 47);
			literals.Add("table", 37);
			literals.Add("activities", 19);
			literals.Add("delete", 33);
			literals.Add("format", 30);
			literals.Add("primary", 27);
			literals.Add("file", 36);
			literals.Add("with", 9);
			literals.Add("mismatch", 26);
			literals.Add("domain", 25);
		}
		
		override public IToken nextToken()			//throws TokenStreamException
		{
			IToken theRetToken = null;
tryAgain:
			for (;;)
			{
//				IToken _token = null;
				int _ttype = Token.INVALID_TYPE;
				resetText();
				try     // for char stream error handling
				{
					try     // for lexical error handling
					{
						switch ( cached_LA1 )
						{
						case 'a':  case 'b':  case 'c':  case 'd':
						case 'e':  case 'f':  case 'g':  case 'h':
						case 'i':  case 'j':  case 'k':  case 'l':
						case 'm':  case 'n':  case 'o':  case 'p':
						case 'q':  case 'r':  case 's':  case 't':
						case 'u':  case 'v':  case 'w':  case 'x':
						case 'y':  case 'z':
						{
							mT_NAME(true);
							theRetToken = returnToken_;
							break;
						}
						case ',':
						{
							mT_NAMELIST(true);
							theRetToken = returnToken_;
							break;
						}
						case '\t':  case '\n':  case '\r':  case ' ':
						{
							mWS(true);
							theRetToken = returnToken_;
							break;
						}
						case '"':
						{
							mT_QUOTE(true);
							theRetToken = returnToken_;
							break;
						}
						case ';':
						{
							mT_SEMI(true);
							theRetToken = returnToken_;
							break;
						}
						case '(':
						{
							mT_OPEN_BR(true);
							theRetToken = returnToken_;
							break;
						}
						case ')':
						{
							mT_CLOSE_BR(true);
							theRetToken = returnToken_;
							break;
						}
						case '.':
						{
							mT_DOT(true);
							theRetToken = returnToken_;
							break;
						}
						case '/':
						{
							mCOMMENT(true);
							theRetToken = returnToken_;
							break;
						}
						default:
						{
							if (cached_LA1==EOF_CHAR) { uponEOF(); returnToken_ = makeToken(Token.EOF_TYPE); }
				else {throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());}
						}
						break; }
						if ( null==returnToken_ ) goto tryAgain; // found SKIP token
						_ttype = returnToken_.Type;
						returnToken_.Type = _ttype;
						return returnToken_;
					}
					catch (RecognitionException e) {
							throw new TokenStreamRecognitionException(e);
					}
				}
				catch (CharStreamException cse) {
					if ( cse is CharStreamIOException ) {
						throw new TokenStreamIOException(((CharStreamIOException)cse).io);
					}
					else {
						throw new TokenStreamException(cse.Message);
					}
				}
			}
		}
		
	public void mT_NAME(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = T_NAME;
		
		{
			matchRange('a','z');
		}
		{    // ( ... )*
			for (;;)
			{
				switch ( cached_LA1 )
				{
				case 'a':  case 'b':  case 'c':  case 'd':
				case 'e':  case 'f':  case 'g':  case 'h':
				case 'i':  case 'j':  case 'k':  case 'l':
				case 'm':  case 'n':  case 'o':  case 'p':
				case 'q':  case 'r':  case 's':  case 't':
				case 'u':  case 'v':  case 'w':  case 'x':
				case 'y':  case 'z':
				{
					matchRange('a','z');
					break;
				}
				case '0':  case '1':  case '2':  case '3':
				case '4':  case '5':  case '6':  case '7':
				case '8':  case '9':
				{
					matchRange('0','9');
					break;
				}
				case '_':
				{
					match('_');
					break;
				}
				case '@':
				{
					match('@');
					break;
				}
				default:
				{
					goto _loop47_breakloop;
				}
				 }
			}
_loop47_breakloop:			;
		}    // ( ... )*
		_ttype = testLiteralsTable(_ttype);
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mT_NAMELIST(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = T_NAMELIST;
		
		match(',');
		{
			switch ( cached_LA1 )
			{
			case '\t':  case '\n':  case '\r':  case ' ':
			{
				int _saveIndex = 0;
				_saveIndex = text.Length;
				mWS(false);
				text.Length = _saveIndex;
				break;
			}
			case 'a':  case 'b':  case 'c':  case 'd':
			case 'e':  case 'f':  case 'g':  case 'h':
			case 'i':  case 'j':  case 'k':  case 'l':
			case 'm':  case 'n':  case 'o':  case 'p':
			case 'q':  case 'r':  case 's':  case 't':
			case 'u':  case 'v':  case 'w':  case 'x':
			case 'y':  case 'z':
			{
				break;
			}
			default:
			{
				throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());
			}
			 }
		}
		mT_NAME(false);
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mWS(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = WS;
		
		{ // ( ... )+
			int _cnt60=0;
			for (;;)
			{
				switch ( cached_LA1 )
				{
				case ' ':
				{
					match(' ');
					break;
				}
				case '\t':
				{
					match('\t');
					break;
				}
				case '\n':
				{
					match('\n');
					newline();
					break;
				}
				default:
					if ((cached_LA1=='\r'))
					{
						match("\r\n");
						newline();
					}
					else if ((cached_LA1=='\r')) {
						match('\r');
						newline();
					}
				else
				{
					if (_cnt60 >= 1) { goto _loop60_breakloop; } else { throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());; }
				}
				break; }
				_cnt60++;
			}
_loop60_breakloop:			;
		}    // ( ... )+
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mT_QUOTE(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = T_QUOTE;
		
		match('"');
		{ // ( ... )+
			int _cnt53=0;
			for (;;)
			{
				if ((cached_LA1=='\r'))
				{
					match("\r\n");
					newline();
				}
				else if ((cached_LA1=='\r')) {
					match('\r');
					newline();
				}
				else if ((cached_LA1=='\n')) {
					match('\n');
					newline();
				}
				else if ((cached_LA1==':')) {
					match(':');
				}
				else if ((cached_LA1=='\\')) {
					match('\\');
				}
				else if ((cached_LA1=='.')) {
					match('.');
				}
				else if ((cached_LA1=='(')) {
					match('(');
				}
				else if ((cached_LA1==')')) {
					match(')');
				}
				else if ((cached_LA1=='/')) {
					match('/');
				}
				else if ((cached_LA1=='>')) {
					match(">");
				}
				else if ((cached_LA1=='<')) {
					match("<");
				}
				else if ((cached_LA1=='=')) {
					match("=");
				}
				else if ((cached_LA1=='+')) {
					match('+');
				}
				else if ((cached_LA1=='-')) {
					match('-');
				}
				else if ((cached_LA1=='\'')) {
					match('\'');
				}
				else if ((cached_LA1=='[')) {
					match('[');
				}
				else if ((cached_LA1==']')) {
					match(']');
				}
				else if ((cached_LA1=='$')) {
					match('$');
				}
				else if ((cached_LA1=='^')) {
					match('^');
				}
				else if ((tokenSet_0_.member(cached_LA1))) {
					{
						match(tokenSet_0_);
					}
				}
				else
				{
					if (_cnt53 >= 1) { goto _loop53_breakloop; } else { throw new NoViableAltForCharException(cached_LA1, getFilename(), getLine(), getColumn());; }
				}
				
				_cnt53++;
			}
_loop53_breakloop:			;
		}    // ( ... )+
		match('"');
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mT_SEMI(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = T_SEMI;
		
		match(';');
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mT_OPEN_BR(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = T_OPEN_BR;
		
		match('(');
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mT_CLOSE_BR(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = T_CLOSE_BR;
		
		match(')');
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mT_DOT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = T_DOT;
		
		match('.');
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	public void mCOMMENT(bool _createToken) //throws RecognitionException, CharStreamException, TokenStreamException
{
		int _ttype; IToken _token=null; int _begin=text.Length;
		_ttype = COMMENT;
		
		match("/*");
		{    // ( ... )*
			for (;;)
			{
				switch ( cached_LA1 )
				{
				case '\n':
				{
					match('\n');
					newline();
					break;
				}
				case '\u0000':  case '\u0001':  case '\u0002':  case '\u0003':
				case '\u0004':  case '\u0005':  case '\u0006':  case '\u0007':
				case '\u0008':  case '\t':  case '\u000b':  case '\u000c':
				case '\u000e':  case '\u000f':  case '\u0010':  case '\u0011':
				case '\u0012':  case '\u0013':  case '\u0014':  case '\u0015':
				case '\u0016':  case '\u0017':  case '\u0018':  case '\u0019':
				case '\u001a':  case '\u001b':  case '\u001c':  case '\u001d':
				case '\u001e':  case '\u001f':  case ' ':  case '!':
				case '"':  case '#':  case '$':  case '%':
				case '&':  case '\'':  case '(':  case ')':
				case '+':  case ',':  case '-':  case '.':
				case '/':  case '0':  case '1':  case '2':
				case '3':  case '4':  case '5':  case '6':
				case '7':  case '8':  case '9':  case ':':
				case ';':  case '<':  case '=':  case '>':
				case '?':  case '@':  case 'A':  case 'B':
				case 'C':  case 'D':  case 'E':  case 'F':
				case 'G':  case 'H':  case 'I':  case 'J':
				case 'K':  case 'L':  case 'M':  case 'N':
				case 'O':  case 'P':  case 'Q':  case 'R':
				case 'S':  case 'T':  case 'U':  case 'V':
				case 'W':  case 'X':  case 'Y':  case 'Z':
				case '[':  case '\\':  case ']':  case '^':
				case '_':  case '`':  case 'a':  case 'b':
				case 'c':  case 'd':  case 'e':  case 'f':
				case 'g':  case 'h':  case 'i':  case 'j':
				case 'k':  case 'l':  case 'm':  case 'n':
				case 'o':  case 'p':  case 'q':  case 'r':
				case 's':  case 't':  case 'u':  case 'v':
				case 'w':  case 'x':  case 'y':  case 'z':
				case '{':  case '|':  case '}':  case '~':
				case '\u007f':
				{
					{
						match(tokenSet_1_);
					}
					break;
				}
				default:
					if (((cached_LA1=='*'))&&( LA(2)!='/' ))
					{
						match('*');
					}
					else if ((cached_LA1=='\r')) {
						match("\r\n");
						newline();
					}
					else if ((cached_LA1=='\r')) {
						match('\r');
						newline();
					}
				else
				{
					goto _loop64_breakloop;
				}
				break; }
			}
_loop64_breakloop:			;
		}    // ( ... )*
		match("*/");
		_ttype = Token.SKIP;
		if (_createToken && (null == _token) && (_ttype != Token.SKIP))
		{
			_token = makeToken(_ttype);
			_token.setText(text.ToString(_begin, text.Length-_begin));
		}
		returnToken_ = _token;
	}
	
	
	private static long[] mk_tokenSet_0_()
	{
		long[] data = { -17179869185L, -1L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_0_ = new BitSet(mk_tokenSet_0_());
	private static long[] mk_tokenSet_1_()
	{
		long[] data = { -4398046520321L, -1L, 0L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_1_ = new BitSet(mk_tokenSet_1_());
	
}
