using BenchmarkDotNet.Attributes;

using Vardirsoft.Commandorix.DomainServices.Parsing;

namespace DomainServices.Benchmarks.Parsing;

[MemoryDiagnoser]
public class LineLexerBenchmark
{
  private const string InputLine = "Lorem [ipsum] (dolor) \t {sit} amet \"Lorem ipsum dolor sit amet\" -# 'Lorem ipsum dolor sit amet' $lorem ,;= _ipsum +dolor --sit amet + * : 'consectetur' 'adipiscing elit' \"ed do eiusmod tempor incididunt ut labore et dolore magna aliqua\" $somestring anotherstring";
  private const string InputLine2X = $"{InputLine} {InputLine}";
  private const string InputLine4X = $"{InputLine2X} {InputLine2X}";
  private const string InputLine8X = $"{InputLine4X} {InputLine4X}";
  private const string InputLine16X = $"{InputLine8X} {InputLine8X}";

  private readonly ILexer _lexer;

  public LineLexerBenchmark()
  {
    _lexer = new LineLexer(new StringLexer());
  }

  [Benchmark]
  public TokenizationResult TokenizeString()
  {
    return _lexer.Tokenize(InputLine, 1, 0);
  }

  [Benchmark]
  public TokenizationResult TokenizeString2X()
  {
    return _lexer.Tokenize(InputLine2X, 1, 0);
  }

  [Benchmark]
  public TokenizationResult TokenizeString4X()
  {
    return _lexer.Tokenize(InputLine4X, 1, 0);
  }

  [Benchmark]
  public TokenizationResult TokenizeString8X()
  {
    return _lexer.Tokenize(InputLine8X, 1, 0);
  }

  [Benchmark]
  public TokenizationResult TokenizeString16X()
  {
    return _lexer.Tokenize(InputLine16X, 1, 0);
  }
}