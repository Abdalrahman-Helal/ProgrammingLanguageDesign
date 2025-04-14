
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
        SYMBOL_EOF         =  0, // (EOF)
        SYMBOL_ERROR       =  1, // (Error)
        SYMBOL_WHITESPACE  =  2, // Whitespace
        SYMBOL_MINUS       =  3, // '-'
        SYMBOL_MINUSMINUS  =  4, // '--'
        SYMBOL_EXCLAMEQ    =  5, // '!='
        SYMBOL_PERCENT     =  6, // '%'
        SYMBOL_AMPAMP      =  7, // '&&'
        SYMBOL_LPAREN      =  8, // '('
        SYMBOL_RPAREN      =  9, // ')'
        SYMBOL_TIMES       = 10, // '*'
        SYMBOL_TIMESTIMES  = 11, // '**'
        SYMBOL_COMMA       = 12, // ','
        SYMBOL_DIV         = 13, // '/'
        SYMBOL_SEMI        = 14, // ';'
        SYMBOL_PIPEPIPE    = 15, // '||'
        SYMBOL_PLUS        = 16, // '+'
        SYMBOL_PLUSPLUS    = 17, // '++'
        SYMBOL_LT          = 18, // '<'
        SYMBOL_LTEQ        = 19, // '<='
        SYMBOL_EQ          = 20, // '='
        SYMBOL_GT          = 21, // '>'
        SYMBOL_GTEQ        = 22, // '>='
        SYMBOL_CALL        = 23, // Call
        SYMBOL_DIGIT       = 24, // Digit
        SYMBOL_ELSE        = 25, // else
        SYMBOL_END         = 26, // End
        SYMBOL_FUNC        = 27, // Func
        SYMBOL_ID          = 28, // ID
        SYMBOL_IF          = 29, // if
        SYMBOL_LOOP        = 30, // Loop
        SYMBOL_RET         = 31, // Ret
        SYMBOL_START       = 32, // Start
        SYMBOL_TYPE        = 33, // Type
        SYMBOL_VOID        = 34, // void
        SYMBOL_ARGS        = 35, // <args>
        SYMBOL_ASSIGN      = 36, // <assign>
        SYMBOL_CONCEPT     = 37, // <concept>
        SYMBOL_COND        = 38, // <cond>
        SYMBOL_DECLARATION = 39, // <declaration>
        SYMBOL_DIGIT2      = 40, // <digit>
        SYMBOL_EXP         = 41, // <exp>
        SYMBOL_EXPR        = 42, // <expr>
        SYMBOL_FACTOR      = 43, // <factor>
        SYMBOL_FOR_STMT    = 44, // <for_stmt>
        SYMBOL_FUNC_CALL   = 45, // <func_call>
        SYMBOL_FUNC_DECL   = 46, // <func_decl>
        SYMBOL_ID2         = 47, // <id>
        SYMBOL_IF_STMT     = 48, // <if_stmt>
        SYMBOL_OP          = 49, // <op>
        SYMBOL_PARAM       = 50, // <param>
        SYMBOL_PARAMS      = 51, // <params>
        SYMBOL_PROGRAM     = 52, // <program>
        SYMBOL_STEP        = 53, // <step>
        SYMBOL_STMT_LIST   = 54, // <stmt_list>
        SYMBOL_TERM        = 55, // <term>
        SYMBOL_TYPE2       = 56  // <type>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_START_END                                 =  0, // <program> ::= Start <stmt_list> End
        RULE_STMT_LIST                                         =  1, // <stmt_list> ::= <concept>
        RULE_STMT_LIST2                                        =  2, // <stmt_list> ::= <concept> <stmt_list>
        RULE_CONCEPT                                           =  3, // <concept> ::= <declaration>
        RULE_CONCEPT2                                          =  4, // <concept> ::= <assign>
        RULE_CONCEPT3                                          =  5, // <concept> ::= <if_stmt>
        RULE_CONCEPT4                                          =  6, // <concept> ::= <for_stmt>
        RULE_CONCEPT5                                          =  7, // <concept> ::= <func_decl>
        RULE_CONCEPT6                                          =  8, // <concept> ::= <func_call>
        RULE_DECLARATION_SEMI                                  =  9, // <declaration> ::= <type> <id> ';'
        RULE_DECLARATION_EQ_SEMI                               = 10, // <declaration> ::= <type> <id> '=' <expr> ';'
        RULE_ASSIGN_EQ_SEMI                                    = 11, // <assign> ::= <id> '=' <expr> ';'
        RULE_ID_ID                                             = 12, // <id> ::= ID
        RULE_EXPR_PLUS                                         = 13, // <expr> ::= <expr> '+' <term>
        RULE_EXPR_MINUS                                        = 14, // <expr> ::= <expr> '-' <term>
        RULE_EXPR                                              = 15, // <expr> ::= <term>
        RULE_TERM_TIMES                                        = 16, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                          = 17, // <term> ::= <term> '/' <factor>
        RULE_TERM_PERCENT                                      = 18, // <term> ::= <term> '%' <factor>
        RULE_TERM                                              = 19, // <term> ::= <factor>
        RULE_FACTOR_TIMESTIMES                                 = 20, // <factor> ::= <factor> '**' <exp>
        RULE_FACTOR                                            = 21, // <factor> ::= <exp>
        RULE_EXP_LPAREN_RPAREN                                 = 22, // <exp> ::= '(' <expr> ')'
        RULE_EXP                                               = 23, // <exp> ::= <id>
        RULE_EXP2                                              = 24, // <exp> ::= <digit>
        RULE_DIGIT_DIGIT                                       = 25, // <digit> ::= Digit
        RULE_IF_STMT_IF_LPAREN_RPAREN_START_END                = 26, // <if_stmt> ::= if '(' <cond> ')' Start <stmt_list> End
        RULE_IF_STMT_IF_LPAREN_RPAREN_START_END_ELSE_START_END = 27, // <if_stmt> ::= if '(' <cond> ')' Start <stmt_list> End else Start <stmt_list> End
        RULE_COND                                              = 28, // <cond> ::= <expr> <op> <expr>
        RULE_OP_LT                                             = 29, // <op> ::= '<'
        RULE_OP_GT                                             = 30, // <op> ::= '>'
        RULE_OP_EQ                                             = 31, // <op> ::= '='
        RULE_OP_EXCLAMEQ                                       = 32, // <op> ::= '!='
        RULE_OP_LTEQ                                           = 33, // <op> ::= '<='
        RULE_OP_GTEQ                                           = 34, // <op> ::= '>='
        RULE_OP_AMPAMP                                         = 35, // <op> ::= '&&'
        RULE_OP_PIPEPIPE                                       = 36, // <op> ::= '||'
        RULE_FOR_STMT_LOOP_LPAREN_SEMI_SEMI_RPAREN_START_END   = 37, // <for_stmt> ::= Loop '(' <type> <assign> ';' <cond> ';' <step> ')' Start <stmt_list> End
        RULE_TYPE_TYPE                                         = 38, // <type> ::= Type
        RULE_STEP_MINUSMINUS                                   = 39, // <step> ::= '--' <id>
        RULE_STEP_MINUSMINUS2                                  = 40, // <step> ::= <id> '--'
        RULE_STEP_PLUSPLUS                                     = 41, // <step> ::= '++' <id>
        RULE_STEP_PLUSPLUS2                                    = 42, // <step> ::= <id> '++'
        RULE_STEP                                              = 43, // <step> ::= <assign>
        RULE_FUNC_DECL_FUNC_LPAREN_RPAREN_START_RET_END        = 44, // <func_decl> ::= Func <type> <id> '(' <params> ')' Start <stmt_list> Ret <expr> End
        RULE_FUNC_DECL_FUNC_VOID_LPAREN_RPAREN_START_END       = 45, // <func_decl> ::= Func void <id> '(' <params> ')' Start <stmt_list> End
        RULE_PARAMS                                            = 46, // <params> ::= <param>
        RULE_PARAMS_COMMA                                      = 47, // <params> ::= <param> ',' <params>
        RULE_PARAMS2                                           = 48, // <params> ::= 
        RULE_PARAM                                             = 49, // <param> ::= <type> <id>
        RULE_FUNC_CALL_CALL_LPAREN_RPAREN_SEMI                 = 50, // <func_call> ::= Call <id> '(' <args> ')' ';'
        RULE_ARGS                                              = 51, // <args> ::= <expr>
        RULE_ARGS_COMMA                                        = 52, // <args> ::= <expr> ',' <args>
        RULE_ARGS2                                             = 53  // <args> ::= 
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lst;

        public MyParser(string filename, ListBox lst)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            this.lst = lst;
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

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_AMPAMP :
                //'&&'
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

                case (int)SymbolConstants.SYMBOL_TIMESTIMES :
                //'**'
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

                case (int)SymbolConstants.SYMBOL_PIPEPIPE :
                //'||'
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

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CALL :
                //Call
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //End
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNC :
                //Func
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
                //Loop
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RET :
                //Ret
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //Start
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TYPE :
                //Type
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VOID :
                //void
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARGS :
                //<args>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONCEPT :
                //<concept>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECLARATION :
                //<declaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
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

                case (int)SymbolConstants.SYMBOL_FOR_STMT :
                //<for_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNC_CALL :
                //<func_call>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNC_DECL :
                //<func_decl>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STMT :
                //<if_stmt>
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

                case (int)SymbolConstants.SYMBOL_PARAMS :
                //<params>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEP :
                //<step>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT_LIST :
                //<stmt_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TYPE2 :
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
                case (int)RuleConstants.RULE_PROGRAM_START_END :
                //<program> ::= Start <stmt_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST :
                //<stmt_list> ::= <concept>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST2 :
                //<stmt_list> ::= <concept> <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT :
                //<concept> ::= <declaration>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT2 :
                //<concept> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT3 :
                //<concept> ::= <if_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT4 :
                //<concept> ::= <for_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT5 :
                //<concept> ::= <func_decl>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT6 :
                //<concept> ::= <func_call>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLARATION_SEMI :
                //<declaration> ::= <type> <id> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLARATION_EQ_SEMI :
                //<declaration> ::= <type> <id> '=' <expr> ';'
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

                case (int)RuleConstants.RULE_TERM_PERCENT :
                //<term> ::= <term> '%' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_TIMESTIMES :
                //<factor> ::= <factor> '**' <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_LPAREN_RPAREN :
                //<exp> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP :
                //<exp> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP2 :
                //<exp> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_START_END :
                //<if_stmt> ::= if '(' <cond> ')' Start <stmt_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_START_END_ELSE_START_END :
                //<if_stmt> ::= if '(' <cond> ')' Start <stmt_list> End else Start <stmt_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <expr> <op> <expr>
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

                case (int)RuleConstants.RULE_OP_EQ :
                //<op> ::= '='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LTEQ :
                //<op> ::= '<='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GTEQ :
                //<op> ::= '>='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_AMPAMP :
                //<op> ::= '&&'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_PIPEPIPE :
                //<op> ::= '||'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STMT_LOOP_LPAREN_SEMI_SEMI_RPAREN_START_END :
                //<for_stmt> ::= Loop '(' <type> <assign> ';' <cond> ';' <step> ')' Start <stmt_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_TYPE :
                //<type> ::= Type
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS :
                //<step> ::= '--' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS2 :
                //<step> ::= <id> '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS :
                //<step> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS2 :
                //<step> ::= <id> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP :
                //<step> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNC_DECL_FUNC_LPAREN_RPAREN_START_RET_END :
                //<func_decl> ::= Func <type> <id> '(' <params> ')' Start <stmt_list> Ret <expr> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNC_DECL_FUNC_VOID_LPAREN_RPAREN_START_END :
                //<func_decl> ::= Func void <id> '(' <params> ')' Start <stmt_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMS :
                //<params> ::= <param>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMS_COMMA :
                //<params> ::= <param> ',' <params>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMS2 :
                //<params> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAM :
                //<param> ::= <type> <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNC_CALL_CALL_LPAREN_RPAREN_SEMI :
                //<func_call> ::= Call <id> '(' <args> ')' ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGS :
                //<args> ::= <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGS_COMMA :
                //<args> ::= <expr> ',' <args>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGS2 :
                //<args> ::= 
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
            string m2="Expected Token:"args.ExpectedTokens.ToString();
            lst.Items.Add(m2);
            //todo: Report message to UI?
        }

    }
}
