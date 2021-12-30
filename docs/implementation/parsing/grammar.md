###Info
This file describes general grammar rules applied to parsing the command line input string. Here are some definitions:

`|` -- alternative; example: `A | B` -- A or B

`*` -- any character

`!X` -- not a specified character; f.e. `!+` -- character should not be a + sign 

`X` -- terminal symbol (e.g. single constant character)

`"X"` -- escaped terminal symbol (e.g. single constant character that might be mistaken as grammar symbol, f.e. `"|"`)

`X...Z` -- continuous sequence of symbols from X to Z, f.e. `1..3` -- 1,2,3

`SYMBOL` -- reference to a defined symbol; required

`{SYMBOL}` -- reference to a defined symbol(s), a character; 0-n occurence

`[SYMBOL]` -- reference to a defined symbol(s), a character; 0-1 occurence

`(SYMBOL)` -- reference to a defined symbol(s), a character; 1-n occurence

`<SYMBOL>` -- reference to a defined symbol(s), a character; exactly one occurence

`X,Y` -- several rules in a sequence that apply to the each symbol; can be enclosed with three previous brackets

###1 Top-level symbols

```ini
QUERY                <= EXPRESSION {WHITE_SPACE} [;]
EXPRESSION           <= SIMPLE_EXPRESSION | ENCLOSED_EXPRESSION
SIMPLE_EXPRESSION    <= CONSTANT | VARIABLE_EXPRESSION | COMMAND_EXPRESSION | PIPELINE_EXPRESSION | LIST_EXPRESSION
ENCLOSED_EXPRESSION  <= "(" CONSTANT | VARIABLE_EXPRESSION | COMMAND_EXPRESSION ")"
PIPELINE_EXPRESSION  <= EXPRESSION {WHITE_SPACE} "|" {WHITE_SPACE} EXPRESSION
LIST_EXPRESSION      <= "[" {WHITE_SPACE} "]" | "[" {WHITE_SPACE} <EXPRESSION> [{WHITE_SPACE} ; {WHITE_SPACE} {EXPRESSION}] {WHITE_SPACE} "]" 
VARIABLE_EXPRESSION  <= VARIABLE_ACCESS | VARIABLE_ASSIGNMENT
VARIABLE_ACCESS      <= $ IDENTIFIER
VARIABLE_ASSIGNMENT  <= $ IDENTIFIER {WHITE_SPACE} = {WHITE_SPACE} EXPRESSION
COMMAND_EXPRESSION   <= IDENTIFIER [(WHITE_SPACE) (PARAMETER_EXPRESSION)]
PARAMETER_EXPRESSION <= PARAMETER_NAME [(WHITE_SPACE) PARAMETER_ARGUMENT]
PARAMETER_NAME       <= PARAMETER_SIGN UNSIGNED_IDENTIFIER
PARAMETER_ARGUMENT   <= CONSTANT | VARIABLE_EXPRESSION | ENCLOSED_EXPRESSION
```

###2 Low-level symbols

```ini
PARAMETER_SIGN       <= - | --
CONSTANT             <= NULL_EXPRESSION | BOOLEAN | STRING | NUMBER
NULL_EXPRESSION      <= n u l l
BOOLEAN              <= t r u e | f a l s e
STRING               <= SINGLE_QUOTED_STRING | DOUBLE_QUOTED_STRING
SINGLE_QUOTED_STRING <= ' {*,!'} '
DOUBLE_QUOTED_STRING <= " {*,!"} "
NUMBER               <= SIGNED_NUMBER | UNSIGNED_NUMBER
SIGNED_NUMBER        <= <+|-> UNSIGNED_NUMBER
UNSIGNED_NUMBER      <= INTEGER | FLOATING_POINT
INTEGER              <= (DIGIT)
FLOATING_POINT       <= {DIGIT} . (DIGIT)
IDENTIFIER           <= UNSIGNED_IDENTIFIER | SIGNED_IDENTIFIER
SIGNED_IDENTIFIER    <= _ IDENTIFIER_LITERAL
UNSIGNED_IDENTIFIER  <= IDENTIFIER_LITERAL
IDENTIFIER_LITERAL   <= LETTER {WORD_CHARACTER} <!->
WORD_CHARACTER       <= LETTER | DIGIT | _ | -
LETTER               <= A...Z | a..z
DIGIT                <= 0..9
```

###3 Alternate

```ini
STRING_EXPRESSION <= UNQUOTED_STRING | QUOTED_STRING
QUOTED_STRING     <= SINGLE_QUOTED_STRING | DOUBLE_QUOTED_STRING
```