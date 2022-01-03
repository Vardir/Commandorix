### 1 Base results
```text
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1415 (21H1/May2021Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.101
[Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
```
|            Method |       Mean |    Error |   StdDev |  Gen 0 |  Gen 1 | Allocated |
|------------------ |-----------:|---------:|---------:|-------:|-------:|----------:|
|    TokenizeString |   371.0 ns |  7.31 ns |  8.98 ns | 0.0730 | 0.0010 |      1 KB |
|  TokenizeString2X |   584.0 ns | 10.62 ns |  9.94 ns | 0.0849 | 0.0019 |      1 KB |
|  TokenizeString4X |   865.7 ns | 14.90 ns | 13.94 ns | 0.1554 | 0.0067 |      3 KB |
|  TokenizeString8X | 1,447.0 ns | 22.28 ns | 20.84 ns | 0.2975 | 0.0210 |      5 KB |
| TokenizeString16X | 1,765.3 ns | 33.41 ns | 29.62 ns | 0.3128 | 0.0248 |      5 KB |

### 2 With token list pre-allocation in *LineLexer.Tokenize*
```text
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1415 (21H1/May2021Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.101
[Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
```
|            Method |       Mean |    Error |   StdDev |  Gen 0 |  Gen 1 | Allocated |
|------------------ |-----------:|---------:|---------:|-------:|-------:|----------:|
|    TokenizeString |   327.1 ns |  6.33 ns |  7.29 ns | 0.0367 | 0.0005 |     616 B |
|  TokenizeString2X |   580.1 ns | 11.56 ns | 16.94 ns | 0.0648 | 0.0019 |   1,088 B |
|  TokenizeString4X |   839.1 ns | 16.78 ns | 19.32 ns | 0.0982 | 0.0038 |   1,656 B |
|  TokenizeString8X | 1,330.5 ns | 17.97 ns | 15.93 ns | 0.1488 | 0.0095 |   2,496 B |
| TokenizeString16X | 1,629.9 ns | 11.28 ns | 10.56 ns | 0.1888 | 0.0153 |   3,176 B |

### 3 With *TokenizationResult.Tokens* as *ReadOnlySpan&lt;Token&gt;* and *CollectionsMarshal.AsSpan* in *LineLexer.Tokenize*
```text
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1415 (21H1/May2021Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.101
[Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
```
|            Method |       Mean |    Error |   StdDev |  Gen 0 |  Gen 1 | Allocated |
|------------------ |-----------:|---------:|---------:|-------:|-------:|----------:|
|    TokenizeString |   306.3 ns |  3.78 ns |  3.35 ns | 0.0200 |      - |     336 B |
|  TokenizeString2X |   547.0 ns | 10.17 ns |  9.99 ns | 0.0362 | 0.0010 |     608 B |
|  TokenizeString4X |   798.0 ns | 12.35 ns | 10.95 ns | 0.0572 | 0.0029 |     968 B |
|  TokenizeString8X | 1,277.4 ns | 14.70 ns | 13.75 ns | 0.0801 | 0.0057 |   1,368 B |
| TokenizeString16X | 1,552.4 ns | 22.57 ns | 21.11 ns | 0.1068 | 0.0095 |   1,808 B |
