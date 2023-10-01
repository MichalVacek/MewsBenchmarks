using BenchmarkDotNet.Attributes;
using FuncSharp;
using JetBrains.Annotations;

namespace MewsBenchmarks.Bool;

[MemoryDiagnoser]
public class BoolPatternMatchingBenchmark
{
    [Params(null, true, false)]
    public bool? ConditionNullable { get; [UsedImplicitly] set; }
    
    [Params(true, false)]
    public bool Condition { get; [UsedImplicitly] set; }
    
    [Benchmark]
    public string MatchNullable()
    {
        return ConditionNullable.Match(
            true, _ => "True",
            false, _ => "False",
            _ => "wtf"
        );
    }

    [Benchmark]
    public string Match()
    {
        return Condition.Match(
            true, _ => "True",
            false, _ => "False"
        );
    }

    [Benchmark]
    public string SwitchExpressionNullable()
    {
        return ConditionNullable switch
        {
            true => "True",
            false => "False",
            null => "wtf"
        };
    }

    [Benchmark]
    public string SwitchExpression()
    {
        return Condition switch
        {
            true => "True",
            false => "False"
        };
    }

    [Benchmark]
    public string IfStatementNullable()
    {
        if (ConditionNullable is null)
            return "wtf";

        if (ConditionNullable is true)
            return "True";

        return "False";
    }


    [Benchmark]
    public string IfStatement()
    {
        if (Condition)
            return "True";

        return "False";
    }
}