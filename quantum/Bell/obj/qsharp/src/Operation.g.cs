#pragma warning disable 1591
using System;
using Microsoft.Quantum.Primitive;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.MetaData.Attributes;

[assembly: OperationDeclaration("Bell.Quantum", "Set (desired : Result, q1 : Qubit) : ()", new string[] { }, "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs", 162L, 7L, 5L)]
[assembly: OperationDeclaration("Bell.Quantum", "BellTestSimple (count : Int, initial : Result) : (Int, Int)", new string[] { }, "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs", 410L, 19L, 5L)]
[assembly: OperationDeclaration("Bell.Quantum", "BellTestFlipped (count : Int, initial : Result) : (Int, Int)", new string[] { }, "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs", 1077L, 45L, 5L)]
[assembly: OperationDeclaration("Bell.Quantum", "BellTestSuperposition (count : Int, initial : Result) : (Int, Int)", new string[] { }, "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs", 1787L, 73L, 5L)]
[assembly: OperationDeclaration("Bell.Quantum", "BellTestEntangled (count : Int, initial : Result) : (Int, Int, Int)", new string[] { }, "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs", 2498L, 101L, 5L)]
#line hidden
namespace Bell.Quantum
{
    public class Set : Operation<(Result,Qubit), QVoid>
    {
        public Set(IOperationFactory m) : base(m)
        {
            this.Dependencies = new Type[] { typeof(Microsoft.Quantum.Primitive.M), typeof(Microsoft.Quantum.Primitive.X) };
        }

        public override Type[] Dependencies
        {
            get;
        }

        protected ICallable<Qubit, Result> M
        {
            get
            {
                return this.Factory.Get<ICallable<Qubit, Result>, Microsoft.Quantum.Primitive.M>();
            }
        }

        protected IUnitary<Qubit> MicrosoftQuantumPrimitiveX
        {
            get
            {
                return this.Factory.Get<IUnitary<Qubit>, Microsoft.Quantum.Primitive.X>();
            }
        }

        public override Func<(Result,Qubit), QVoid> Body
        {
            get => (_args) =>
            {
#line hidden
                this.Factory.StartOperation("Bell.Quantum.Set", OperationFunctor.Body, _args);
                var __result__ = default(QVoid);
                try
                {
                    var (desired,q1) = _args;
#line 10 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    var current = M.Apply<Result>(q1);
#line 11 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    if ((desired != current))
                    {
#line 13 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        MicrosoftQuantumPrimitiveX.Apply(q1);
                    }

#line hidden
                    return __result__;
                }
                finally
                {
#line hidden
                    this.Factory.EndOperation("Bell.Quantum.Set", OperationFunctor.Body, __result__);
                }
            }

            ;
        }

        public static System.Threading.Tasks.Task<QVoid> Run(IOperationFactory __m__, Result desired, Qubit q1)
        {
            return __m__.Run<Set, (Result,Qubit), QVoid>((desired, q1));
        }
    }

    public class BellTestSimple : Operation<(Int64,Result), (Int64,Int64)>
    {
        public BellTestSimple(IOperationFactory m) : base(m)
        {
            this.Dependencies = new Type[] { typeof(Microsoft.Quantum.Primitive.Allocate), typeof(Microsoft.Quantum.Primitive.M), typeof(Microsoft.Quantum.Primitive.Release), typeof(Bell.Quantum.Set) };
        }

        public override Type[] Dependencies
        {
            get;
        }

        protected Allocate Allocate
        {
            get
            {
                return this.Factory.Get<Allocate, Microsoft.Quantum.Primitive.Allocate>();
            }
        }

        protected ICallable<Qubit, Result> M
        {
            get
            {
                return this.Factory.Get<ICallable<Qubit, Result>, Microsoft.Quantum.Primitive.M>();
            }
        }

        protected Release Release
        {
            get
            {
                return this.Factory.Get<Release, Microsoft.Quantum.Primitive.Release>();
            }
        }

        protected ICallable<(Result,Qubit), QVoid> Set
        {
            get
            {
                return this.Factory.Get<ICallable<(Result,Qubit), QVoid>, Bell.Quantum.Set>();
            }
        }

        public override Func<(Int64,Result), (Int64,Int64)> Body
        {
            get => (_args) =>
            {
#line hidden
                this.Factory.StartOperation("Bell.Quantum.BellTestSimple", OperationFunctor.Body, _args);
                var __result__ = default((Int64,Int64));
                try
                {
                    var (count,initial) = _args;
#line 22 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    var numOfOnes = 0L;
#line 23 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    var qubits = Allocate.Apply(1L);
#line 25 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    foreach (var test in new Range(1L, count))
                    {
#line 27 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        Set.Apply((initial, qubits[0L]));
#line 29 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        var result = M.Apply<Result>(qubits[0L]);
#line 31 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        if ((result == Result.One))
                        {
#line 33 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                            numOfOnes = (numOfOnes + 1L);
                        }
                    }

#line 37 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    Set.Apply((Result.Zero, qubits[0L]));
#line hidden
                    Release.Apply(qubits);
#line hidden
                    __result__ = ((count - numOfOnes), numOfOnes);
#line 40 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    return __result__;
                }
                finally
                {
#line hidden
                    this.Factory.EndOperation("Bell.Quantum.BellTestSimple", OperationFunctor.Body, __result__);
                }
            }

            ;
        }

        public static System.Threading.Tasks.Task<(Int64,Int64)> Run(IOperationFactory __m__, Int64 count, Result initial)
        {
            return __m__.Run<BellTestSimple, (Int64,Result), (Int64,Int64)>((count, initial));
        }
    }

    public class BellTestFlipped : Operation<(Int64,Result), (Int64,Int64)>
    {
        public BellTestFlipped(IOperationFactory m) : base(m)
        {
            this.Dependencies = new Type[] { typeof(Microsoft.Quantum.Primitive.Allocate), typeof(Microsoft.Quantum.Primitive.M), typeof(Microsoft.Quantum.Primitive.Release), typeof(Bell.Quantum.Set), typeof(Microsoft.Quantum.Primitive.X) };
        }

        public override Type[] Dependencies
        {
            get;
        }

        protected Allocate Allocate
        {
            get
            {
                return this.Factory.Get<Allocate, Microsoft.Quantum.Primitive.Allocate>();
            }
        }

        protected ICallable<Qubit, Result> M
        {
            get
            {
                return this.Factory.Get<ICallable<Qubit, Result>, Microsoft.Quantum.Primitive.M>();
            }
        }

        protected Release Release
        {
            get
            {
                return this.Factory.Get<Release, Microsoft.Quantum.Primitive.Release>();
            }
        }

        protected ICallable<(Result,Qubit), QVoid> Set
        {
            get
            {
                return this.Factory.Get<ICallable<(Result,Qubit), QVoid>, Bell.Quantum.Set>();
            }
        }

        protected IUnitary<Qubit> MicrosoftQuantumPrimitiveX
        {
            get
            {
                return this.Factory.Get<IUnitary<Qubit>, Microsoft.Quantum.Primitive.X>();
            }
        }

        public override Func<(Int64,Result), (Int64,Int64)> Body
        {
            get => (_args) =>
            {
#line hidden
                this.Factory.StartOperation("Bell.Quantum.BellTestFlipped", OperationFunctor.Body, _args);
                var __result__ = default((Int64,Int64));
                try
                {
                    var (count,initial) = _args;
#line 48 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    var numOfOnes = 0L;
#line 49 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    var qubits = Allocate.Apply(1L);
#line 51 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    foreach (var test in new Range(1L, count))
                    {
#line 53 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        Set.Apply((initial, qubits[0L]));
#line 55 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        MicrosoftQuantumPrimitiveX.Apply(qubits[0L]);
#line 57 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        var result = M.Apply<Result>(qubits[0L]);
#line 59 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        if ((result == Result.One))
                        {
#line 61 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                            numOfOnes = (numOfOnes + 1L);
                        }
                    }

#line 65 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    Set.Apply((Result.Zero, qubits[0L]));
#line hidden
                    Release.Apply(qubits);
#line hidden
                    __result__ = ((count - numOfOnes), numOfOnes);
#line 68 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    return __result__;
                }
                finally
                {
#line hidden
                    this.Factory.EndOperation("Bell.Quantum.BellTestFlipped", OperationFunctor.Body, __result__);
                }
            }

            ;
        }

        public static System.Threading.Tasks.Task<(Int64,Int64)> Run(IOperationFactory __m__, Int64 count, Result initial)
        {
            return __m__.Run<BellTestFlipped, (Int64,Result), (Int64,Int64)>((count, initial));
        }
    }

    public class BellTestSuperposition : Operation<(Int64,Result), (Int64,Int64)>
    {
        public BellTestSuperposition(IOperationFactory m) : base(m)
        {
            this.Dependencies = new Type[] { typeof(Microsoft.Quantum.Primitive.Allocate), typeof(Microsoft.Quantum.Primitive.H), typeof(Microsoft.Quantum.Primitive.M), typeof(Microsoft.Quantum.Primitive.Release), typeof(Bell.Quantum.Set) };
        }

        public override Type[] Dependencies
        {
            get;
        }

        protected Allocate Allocate
        {
            get
            {
                return this.Factory.Get<Allocate, Microsoft.Quantum.Primitive.Allocate>();
            }
        }

        protected IUnitary<Qubit> MicrosoftQuantumPrimitiveH
        {
            get
            {
                return this.Factory.Get<IUnitary<Qubit>, Microsoft.Quantum.Primitive.H>();
            }
        }

        protected ICallable<Qubit, Result> M
        {
            get
            {
                return this.Factory.Get<ICallable<Qubit, Result>, Microsoft.Quantum.Primitive.M>();
            }
        }

        protected Release Release
        {
            get
            {
                return this.Factory.Get<Release, Microsoft.Quantum.Primitive.Release>();
            }
        }

        protected ICallable<(Result,Qubit), QVoid> Set
        {
            get
            {
                return this.Factory.Get<ICallable<(Result,Qubit), QVoid>, Bell.Quantum.Set>();
            }
        }

        public override Func<(Int64,Result), (Int64,Int64)> Body
        {
            get => (_args) =>
            {
#line hidden
                this.Factory.StartOperation("Bell.Quantum.BellTestSuperposition", OperationFunctor.Body, _args);
                var __result__ = default((Int64,Int64));
                try
                {
                    var (count,initial) = _args;
#line 76 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    var numOfOnes = 0L;
#line 77 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    var qubits = Allocate.Apply(1L);
#line 79 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    foreach (var test in new Range(1L, count))
                    {
#line 81 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        Set.Apply((initial, qubits[0L]));
#line 83 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        MicrosoftQuantumPrimitiveH.Apply(qubits[0L]);
#line 85 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        var result = M.Apply<Result>(qubits[0L]);
#line 87 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        if ((result == Result.One))
                        {
#line 89 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                            numOfOnes = (numOfOnes + 1L);
                        }
                    }

#line 93 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    Set.Apply((Result.Zero, qubits[0L]));
#line hidden
                    Release.Apply(qubits);
#line hidden
                    __result__ = ((count - numOfOnes), numOfOnes);
#line 96 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    return __result__;
                }
                finally
                {
#line hidden
                    this.Factory.EndOperation("Bell.Quantum.BellTestSuperposition", OperationFunctor.Body, __result__);
                }
            }

            ;
        }

        public static System.Threading.Tasks.Task<(Int64,Int64)> Run(IOperationFactory __m__, Int64 count, Result initial)
        {
            return __m__.Run<BellTestSuperposition, (Int64,Result), (Int64,Int64)>((count, initial));
        }
    }

    public class BellTestEntangled : Operation<(Int64,Result), (Int64,Int64,Int64)>
    {
        public BellTestEntangled(IOperationFactory m) : base(m)
        {
            this.Dependencies = new Type[] { typeof(Microsoft.Quantum.Primitive.Allocate), typeof(Microsoft.Quantum.Primitive.CNOT), typeof(Microsoft.Quantum.Primitive.H), typeof(Microsoft.Quantum.Primitive.M), typeof(Microsoft.Quantum.Primitive.Release), typeof(Bell.Quantum.Set) };
        }

        public override Type[] Dependencies
        {
            get;
        }

        protected Allocate Allocate
        {
            get
            {
                return this.Factory.Get<Allocate, Microsoft.Quantum.Primitive.Allocate>();
            }
        }

        protected IUnitary<(Qubit,Qubit)> MicrosoftQuantumPrimitiveCNOT
        {
            get
            {
                return this.Factory.Get<IUnitary<(Qubit,Qubit)>, Microsoft.Quantum.Primitive.CNOT>();
            }
        }

        protected IUnitary<Qubit> MicrosoftQuantumPrimitiveH
        {
            get
            {
                return this.Factory.Get<IUnitary<Qubit>, Microsoft.Quantum.Primitive.H>();
            }
        }

        protected ICallable<Qubit, Result> M
        {
            get
            {
                return this.Factory.Get<ICallable<Qubit, Result>, Microsoft.Quantum.Primitive.M>();
            }
        }

        protected Release Release
        {
            get
            {
                return this.Factory.Get<Release, Microsoft.Quantum.Primitive.Release>();
            }
        }

        protected ICallable<(Result,Qubit), QVoid> Set
        {
            get
            {
                return this.Factory.Get<ICallable<(Result,Qubit), QVoid>, Bell.Quantum.Set>();
            }
        }

        public override Func<(Int64,Result), (Int64,Int64,Int64)> Body
        {
            get => (_args) =>
            {
#line hidden
                this.Factory.StartOperation("Bell.Quantum.BellTestEntangled", OperationFunctor.Body, _args);
                var __result__ = default((Int64,Int64,Int64));
                try
                {
                    var (count,initial) = _args;
#line 104 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    var numOfOnes = 0L;
#line 105 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    var agree = 0L;
#line 106 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    var qubits = Allocate.Apply(2L);
#line 108 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    foreach (var test in new Range(1L, count))
                    {
#line 110 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        Set.Apply((initial, qubits[0L]));
#line 111 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        Set.Apply((Result.Zero, qubits[1L]));
#line 112 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        MicrosoftQuantumPrimitiveH.Apply(qubits[0L]);
#line 113 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        MicrosoftQuantumPrimitiveCNOT.Apply((qubits[0L], qubits[1L]));
#line 115 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        var result = M.Apply<Result>(qubits[0L]);
#line 117 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        if ((result == Result.One))
                        {
#line 119 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                            numOfOnes = (numOfOnes + 1L);
                        }

#line 122 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                        if ((result == M.Apply<Result>(qubits[1L])))
                        {
#line 124 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                            agree = (agree + 1L);
                        }
                    }

#line 128 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    Set.Apply((Result.Zero, qubits[0L]));
#line 129 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    Set.Apply((Result.Zero, qubits[1L]));
#line hidden
                    Release.Apply(qubits);
#line hidden
                    __result__ = ((count - numOfOnes), numOfOnes, agree);
#line 132 "C:\\Users\\ilikos.UTU\\OneDrive\\GOOGLEDRIVE\\Teaching\\Programming\\Temp\\QS\\Bell\\Operation.qs"
                    return __result__;
                }
                finally
                {
#line hidden
                    this.Factory.EndOperation("Bell.Quantum.BellTestEntangled", OperationFunctor.Body, __result__);
                }
            }

            ;
        }

        public static System.Threading.Tasks.Task<(Int64,Int64,Int64)> Run(IOperationFactory __m__, Int64 count, Result initial)
        {
            return __m__.Run<BellTestEntangled, (Int64,Result), (Int64,Int64,Int64)>((count, initial));
        }
    }
}