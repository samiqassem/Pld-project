
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF              =  0, // (EOF)
        SYMBOL_ERROR            =  1, // (Error)
        SYMBOL_WHITESPACE       =  2, // Whitespace
        SYMBOL_MINUS            =  3, // '-'
        SYMBOL_MINUSMINUS       =  4, // '--'
        SYMBOL_EXCLAMEQ         =  5, // '!='
        SYMBOL_LPAREN           =  6, // '('
        SYMBOL_RPAREN           =  7, // ')'
        SYMBOL_TIMES            =  8, // '*'
        SYMBOL_COMMA            =  9, // ','
        SYMBOL_DIV              = 10, // '/'
        SYMBOL_SEMI             = 11, // ';'
        SYMBOL_LBRACE           = 12, // '{'
        SYMBOL_RBRACE           = 13, // '}'
        SYMBOL_PLUS             = 14, // '+'
        SYMBOL_PLUSPLUS         = 15, // '++'
        SYMBOL_LT               = 16, // '<'
        SYMBOL_EQ               = 17, // '='
        SYMBOL_EQEQ             = 18, // '=='
        SYMBOL_GT               = 19, // '>'
        SYMBOL_BIG              = 20, // big
        SYMBOL_DEFINE           = 21, // define
        SYMBOL_DIGIT            = 22, // Digit
        SYMBOL_END              = 23, // End
        SYMBOL_GET              = 24, // get
        SYMBOL_GO               = 25, // go
        SYMBOL_HERE             = 26, // here
        SYMBOL_ID               = 27, // ID
        SYMBOL_IF               = 28, // if
        SYMBOL_LOOP             = 29, // loop
        SYMBOL_PLAY             = 30, // play
        SYMBOL_SMALL            = 31, // small
        SYMBOL_VAR              = 32, // var
        SYMBOL_WORDS            = 33, // words
        SYMBOL_ASSIGN           = 34, // <assign>
        SYMBOL_CALLARGMINUSLIST = 35, // <callarg-list>
        SYMBOL_CODE             = 36, // <code>
        SYMBOL_CODEMINUSSTMT    = 37, // <code-stmt>
        SYMBOL_CONDITION        = 38, // <condition>
        SYMBOL_DECLERATION      = 39, // <decleration>
        SYMBOL_DIGIT2           = 40, // <digit>
        SYMBOL_EXPR             = 41, // <expr>
        SYMBOL_FACTOR           = 42, // <factor>
        SYMBOL_ID2              = 43, // <id>
        SYMBOL_IFMINUSSTMT      = 44, // <if-stmt>
        SYMBOL_LOOP2            = 45, // <loop>
        SYMBOL_METHOD           = 46, // <method>
        SYMBOL_METHODMINUSCALL  = 47, // <method-call>
        SYMBOL_OP               = 48, // <op>
        SYMBOL_PARAM            = 49, // <param>
        SYMBOL_PARAMMINUSLIST   = 50, // <param-list>
        SYMBOL_START            = 51, // <Start>
        SYMBOL_STEPS            = 52, // <steps>
        SYMBOL_TERM             = 53, // <term>
        SYMBOL_TYPE             = 54  // <type>
    };

    enum RuleConstants : int
    {
        RULE_START_PLAY_END                           =  0, // <Start> ::= play <code> End
        RULE_CODE                                     =  1, // <code> ::= <code-stmt>
        RULE_CODE2                                    =  2, // <code> ::= <code-stmt> <code>
        RULE_CODESTMT                                 =  3, // <code-stmt> ::= <assign>
        RULE_CODESTMT2                                =  4, // <code-stmt> ::= <decleration>
        RULE_CODESTMT3                                =  5, // <code-stmt> ::= <if-stmt>
        RULE_CODESTMT4                                =  6, // <code-stmt> ::= <loop>
        RULE_CODESTMT5                                =  7, // <code-stmt> ::= <method>
        RULE_CODESTMT6                                =  8, // <code-stmt> ::= <method-call>
        RULE_DECLERATION_VAR                          =  9, // <decleration> ::= var <id>
        RULE_ASSIGN_EQ_SEMI                           = 10, // <assign> ::= <id> '=' <expr> ';'
        RULE_ID_ID                                    = 11, // <id> ::= ID
        RULE_EXPR_PLUS                                = 12, // <expr> ::= <expr> '+' <term>
        RULE_EXPR_MINUS                               = 13, // <expr> ::= <expr> '-' <term>
        RULE_EXPR                                     = 14, // <expr> ::= <term>
        RULE_TERM_TIMES                               = 15, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                 = 16, // <term> ::= <term> '/' <factor>
        RULE_TERM                                     = 17, // <term> ::= <factor>
        RULE_FACTOR_LPAREN_RPAREN                     = 18, // <factor> ::= '(' <expr> ')'
        RULE_FACTOR                                   = 19, // <factor> ::= <id>
        RULE_FACTOR2                                  = 20, // <factor> ::= <digit>
        RULE_DIGIT_DIGIT                              = 21, // <digit> ::= Digit
        RULE_IFSTMT_IF_LBRACE_RBRACE_GO               = 22, // <if-stmt> ::= if '{' <condition> '}' go <code>
        RULE_IFSTMT_IF_GO_GO_HERE                     = 23, // <if-stmt> ::= if <condition> go <code> go here <code>
        RULE_CONDITION                                = 24, // <condition> ::= <expr> <op> <expr>
        RULE_OP_LT                                    = 25, // <op> ::= '<'
        RULE_OP_GT                                    = 26, // <op> ::= '>'
        RULE_OP_EQEQ                                  = 27, // <op> ::= '=='
        RULE_OP_EXCLAMEQ                              = 28, // <op> ::= '!='
        RULE_LOOP_LOOP_LBRACE_COMMA_COMMA_RBRACE_SEMI = 29, // <loop> ::= loop '{' <type> <assign> ',' <condition> ',' <steps> '}' <code> ';'
        RULE_TYPE_SMALL                               = 30, // <type> ::= small
        RULE_TYPE_BIG                                 = 31, // <type> ::= big
        RULE_TYPE_WORDS                               = 32, // <type> ::= words
        RULE_STEPS_MINUSMINUS                         = 33, // <steps> ::= '--' <id>
        RULE_STEPS_PLUSPLUS                           = 34, // <steps> ::= '++' <id>
        RULE_STEPS                                    = 35, // <steps> ::= <assign>
        RULE_METHOD_DEFINE_LBRACE_RBRACE_SEMI         = 36, // <method> ::= define <id> '{' <param-list> '}' <code> ';'
        RULE_PARAMLIST                                = 37, // <param-list> ::= <param>
        RULE_PARAMLIST_COMMA                          = 38, // <param-list> ::= <param> ',' <param-list>
        RULE_PARAM                                    = 39, // <param> ::= <type> <id>
        RULE_METHODCALL_GET_LBRACE_RBRACE             = 40, // <method-call> ::= get <id> '{' <callarg-list> '}'
        RULE_CALLARGLIST                              = 41, // <callarg-list> ::= <expr>
        RULE_CALLARGLIST_COMMA                        = 42  // <callarg-list> ::= <expr> ',' <callarg-list>
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lst;
        ListBox ls;
        public MyParser(string filename , ListBox lst , ListBox ls)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            this. lst = lst;
            this.ls = ls;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BIG :
                //big
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFINE :
                //define
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //End
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GET :
                //get
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GO :
                //go
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_HERE :
                //here
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //ID
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LOOP :
                //loop
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLAY :
                //play
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SMALL :
                //small
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VAR :
                //var
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WORDS :
                //words
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CALLARGMINUSLIST :
                //<callarg-list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CODE :
                //<code>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CODEMINUSSTMT :
                //<code-stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONDITION :
                //<condition>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECLERATION :
                //<decleration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IFMINUSSTMT :
                //<if-stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LOOP2 :
                //<loop>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHOD :
                //<method>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODMINUSCALL :
                //<method-call>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAM :
                //<param>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAMMINUSLIST :
                //<param-list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //<Start>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEPS :
                //<steps>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TYPE :
                //<type>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_START_PLAY_END :
                //<Start> ::= play <code> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CODE :
                //<code> ::= <code-stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CODE2 :
                //<code> ::= <code-stmt> <code>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CODESTMT :
                //<code-stmt> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CODESTMT2 :
                //<code-stmt> ::= <decleration>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CODESTMT3 :
                //<code-stmt> ::= <if-stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CODESTMT4 :
                //<code-stmt> ::= <loop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CODESTMT5 :
                //<code-stmt> ::= <method>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CODESTMT6 :
                //<code-stmt> ::= <method-call>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLERATION_VAR :
                //<decleration> ::= var <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_EQ_SEMI :
                //<assign> ::= <id> '=' <expr> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= ID
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <expr> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <expr> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_LPAREN_RPAREN :
                //<factor> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR2 :
                //<factor> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTMT_IF_LBRACE_RBRACE_GO :
                //<if-stmt> ::= if '{' <condition> '}' go <code>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTMT_IF_GO_GO_HERE :
                //<if-stmt> ::= if <condition> go <code> go here <code>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONDITION :
                //<condition> ::= <expr> <op> <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EQEQ :
                //<op> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOOP_LOOP_LBRACE_COMMA_COMMA_RBRACE_SEMI :
                //<loop> ::= loop '{' <type> <assign> ',' <condition> ',' <steps> '}' <code> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_SMALL :
                //<type> ::= small
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_BIG :
                //<type> ::= big
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_WORDS :
                //<type> ::= words
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEPS_MINUSMINUS :
                //<steps> ::= '--' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEPS_PLUSPLUS :
                //<steps> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEPS :
                //<steps> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHOD_DEFINE_LBRACE_RBRACE_SEMI :
                //<method> ::= define <id> '{' <param-list> '}' <code> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMLIST :
                //<param-list> ::= <param>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMLIST_COMMA :
                //<param-list> ::= <param> ',' <param-list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAM :
                //<param> ::= <type> <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHODCALL_GET_LBRACE_RBRACE :
                //<method-call> ::= get <id> '{' <callarg-list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CALLARGLIST :
                //<callarg-list> ::= <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CALLARGLIST_COMMA :
                //<callarg-list> ::= <expr> ',' <callarg-list>
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+" in line: "+args.UnexpectedToken.Location.LineNr;
            lst.Items.Add(message);
            string m2 ="Expected token " + args.ExpectedTokens.ToString();
            lst.Items.Add(m2 );
            //todo: Report message to UI?
        }
        private void TokenReadEvent(LALRParser pr,TokenReadEventArgs args)
        {
            string info = args.Token.Text + "       " + (SymbolConstants)args.Token.Symbol.Id;
            ls.Items.Add(info);
        }
    }
}
