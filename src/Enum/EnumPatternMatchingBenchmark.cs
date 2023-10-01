using BenchmarkDotNet.Attributes;
using FuncSharp;
using JetBrains.Annotations;

namespace MewsBenchmarks.Enum;

[MemoryDiagnoser]
public class EnumPatternMatchingBenchmark
{
    [Params(Color.Blue)]
    public Color Color { get; [UsedImplicitly] set; }
    
    [Benchmark]
    public string Match()
    {
        return Color.Match(
            Color.Blue, _ => "Blue",
            Color.Green, _ => "Green",
            Color.Red, _ => "Red",
            Color.Yellow, _ => "Yellow",
            Color.Pink, _ => "Pink",
            _ => throw new ArgumentOutOfRangeException()
        );
    }

    [Benchmark]
    public string SwitchExpression()
    {
        return Color switch
        {
            Color.Red => "Red",
            Color.Green => "Green",
            Color.Blue => "Blue",
            Color.Yellow => "Yellow",
            Color.Pink => "Pink",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}