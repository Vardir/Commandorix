using BenchmarkDotNet.Attributes;

using Vardirsoft.Commandorix.DomainServices.Lexing;
using Vardirsoft.Commandorix.Entities.Tokens;

namespace DomainServices.Benchmarks.Parsing;

[MemoryDiagnoser]
public class LineLexerBenchmark
{
  private const string InputLine = "docker run --device=/dev/sda:/dev/xvdc --rm -it ubuntu fdisk  /dev/xvdc";
  private const string InputLine2X = $"{InputLine} -d --isolation process microsoft/nanoserver powershell echo process";
  private const string InputLine4X = $"{InputLine2X} --storage-opt size=120G fedora /bin/bash -tmpfs /run:rw,noexec,nosuid,size=65536k my_image";
  private const string InputLine8X = $"{InputLine4X} -v 'pwd':'pwd' -w 'pwd' -i -t  ubuntu pwd --read-only -v /icanwrite busybox touch /icanwrite/here";
  private const string InputLine16X = $"{InputLine8X} -t -i -v /var/run/docker.sock:/var/run/docker.sock -v /path/to/static-docker-binary:/usr/bin/docker busybox sh";

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