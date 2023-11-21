using BenchmarkDotNet.Attributes;
using FuncSharp;

namespace MewsBenchmarks.OptionalEntity;

[MemoryDiagnoser]
public class OptionalEnttityAccessBenchmark
{
    public Entity PrimaryEntity = new Entity()
    {
        Id = Guid.NewGuid(),
        OptionalEntityId = Guid.NewGuid(),
        OptionalEntityValue = new Entity()
        {
            Id = Guid.NewGuid(),
            OptionalEntityId = Guid.NewGuid(),
            OptionalEntityValue = new Entity
            {
                Id = Guid.NewGuid()
            }
        }
    };

    [Benchmark]
    public Guid? FuncSharpEntityAccessV1()
    {
        return PrimaryEntity.OptionalEntity.FlatMap(e => e.OptionalEntity.Map(e => e.Id)).ToNullable();
    }

    [Benchmark]
    public Guid? FuncSharpEntityAccessV2()
    {
        return PrimaryEntity.OptionalEntity.Map(e => e.OptionalEntity.Map(e => e.Id)).Flatten().ToNullable();
    }

    [Benchmark]
    public Guid? FuncSharpEntityAccessV3()
    {
        return PrimaryEntity.OptionalEntity.GetOrNull()?.OptionalEntity.GetOrNull()?.Id;
    }

    [Benchmark]
    public Guid? FuncSharpEntityAccessV4()
    {
        return PrimaryEntity.OptionalEntity.ToNullable(e => e.OptionalEntity.ToNullable(e2 => e2.Id));
    }

    [Benchmark]
    public Guid? NullConditionalOperatorsEntityAccess()
    {
        return PrimaryEntity.SafeOptionalEntity?.SafeOptionalEntity?.Id;
    }
}

