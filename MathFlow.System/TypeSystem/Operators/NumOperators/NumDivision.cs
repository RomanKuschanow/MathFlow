﻿using MathFlow.SemanticAnalyzer.Scope;
using MathFlow.TypeSystem.Functions;
using MathFlow.TypeSystem.Instances;
using MathFlow.TypeSystem.Types;
using System.Collections.Immutable;
using System.Threading.Tasks.Dataflow;

namespace MathFlow.TypeSystem.Operators.NumOperators;
public class NumDivision : Operator
{
    private static NumDivision _instance = new();

    public static NumDivision Instance => _instance;

    private NumDivision() : base(NumDivFunc.Instance.Arguments, NumDivFunc.Instance.Returns, OperatorType.Division, NumDivFunc.Instance)
    {
    }

    private class NumDivFunc : IFunction
    {
        public Guid MemberId { get; init; } = Guid.NewGuid();

        private static NumDivFunc _instance = new();

        public static NumDivFunc Instance => _instance;

        public ImmutableList<Variable> Arguments => new List<Variable>()
        {
            new("a", Num.Instance),
            new("b", Num.Instance)
        }.ToImmutableList();

        public Type Returns => Num.Instance;

        public IInstance Execute(IScope scope = null!, params IInstance[] instances)
        {
            if (!Arguments.Select(a => a.Type).SequenceEqual(instances.Select(i => i.Type)))
            {
                throw new ArgumentException($"Actual instances does not match expected instances", nameof(instances));
            }

            return (NumInstance)instances[0] / (NumInstance)instances[1];
        }
    }
}
