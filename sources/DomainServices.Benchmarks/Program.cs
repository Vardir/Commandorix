using BenchmarkDotNet.Running;

using DomainServices.Benchmarks.Parsing;

var lexerSummary = BenchmarkRunner.Run<LineLexerBenchmark>();