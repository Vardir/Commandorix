### 1 Base results
```text
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1415 (21H1/May2021Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.101
[Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
```
|            Method |        Mean |    Error |   StdDev |  Gen 0 |  Gen 1 | Allocated |
|------------------ |------------:|---------:|---------:|-------:|-------:|----------:|
|    TokenizeString |    889.3 ns |  1.74 ns |  1.62 ns | 0.1383 | 0.0057 |      2 KB |
|  TokenizeString2X |  1,757.0 ns |  2.52 ns |  2.35 ns | 0.2708 | 0.0191 |      4 KB |
|  TokenizeString4X |  3,518.4 ns | 67.12 ns | 84.89 ns | 0.5341 | 0.0763 |      9 KB |
|  TokenizeString8X |  6,880.6 ns | 28.48 ns | 26.64 ns | 1.0605 | 0.2594 |     17 KB |
| TokenizeString16X | 13,725.1 ns | 28.96 ns | 27.09 ns | 2.1057 | 0.7019 |     35 KB |


### 2 With token list pre-allocation in *LineLexer.Tokenize*
```text
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1415 (21H1/May2021Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.101
[Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
```
|            Method |        Mean |    Error |   StdDev |   Gen 0 |  Gen 1 | Allocated |
|------------------ |------------:|---------:|---------:|--------:|-------:|----------:|
|    TokenizeString |    850.3 ns |  3.11 ns |  2.91 ns |  0.1068 |  0.0038 |      2 KB |
|  TokenizeString2X |  1,693.0 ns |  5.02 ns |  4.45 ns |  0.2079 |  0.0153 |      3 KB |
|  TokenizeString4X |  3,353.2 ns |  9.53 ns |  8.91 ns |  0.4120 |  0.0572 |      7 KB |
|  TokenizeString8X |  6,656.8 ns |  9.38 ns |  8.77 ns |  0.8163 |  0.1984 |     13 KB |
| TokenizeString16X | 13,305.3 ns | 58.75 ns | 52.08 ns |  1.6327 |  0.5341 |     27 KB |

### 3 With *TokenizationResult.Tokens* as *ReadOnlySpan&lt;Token&gt;* and *CollectionsMarshal.AsSpan* in *LineLexer.Tokenize*
```text
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1415 (21H1/May2021Update)
AMD Ryzen 7 5800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.101
[Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
```
|            Method |        Mean |    Error |   StdDev |  Gen 0 |  Gen 1 | Allocated |
|------------------ |------------:|---------:|---------:|-------:|-------:|----------:|
|    TokenizeString |    824.6 ns |  1.29 ns |  1.21 ns | 0.0658 | 0.0038 |      1 KB |
|  TokenizeString2X |  1,624.4 ns |  1.78 ns |  1.48 ns | 0.1278 | 0.0134 |      2 KB |
|  TokenizeString4X |  3,253.0 ns |  6.03 ns |  5.64 ns | 0.2518 | 0.0496 |      4 KB |
|  TokenizeString8X |  6,437.4 ns | 10.21 ns |  9.55 ns | 0.5035 | 0.1678 |      8 KB |
| TokenizeString16X | 12,926.2 ns | 23.49 ns | 20.82 ns | 0.9918 | 0.4883 |     16 KB |
