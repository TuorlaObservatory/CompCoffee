namespace Bell.Quantum
{
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Primitive;

    operation Set (desired : Result, q1 : Qubit) : ()
    {
        body
        {
           let current = M(q1);
           if (desired != current) 
           {
               X(q1);
           }
        }
    }

    operation BellTestSimple (count : Int, initial : Result) : (Int, Int)
    {
        body 
        {
            mutable numOfOnes = 0;
            using (qubits = Qubit[1])
            {
                for (test in 1..count)
                {
                    Set (initial, qubits[0]);

                    let result = M(qubits[0]);
                    
                    if(result == One)
                    {
                        set numOfOnes = numOfOnes + 1;
                    }
                }

                Set(Zero, qubits[0]);
            }

            return (count - numOfOnes, numOfOnes);
        }
    }

    operation BellTestFlipped (count : Int, initial : Result) : (Int, Int)
    {
        body 
        {
            mutable numOfOnes = 0;
            using (qubits = Qubit[1])
            {
                for (test in 1..count)
                {
                    Set (initial, qubits[0]);

                    X(qubits[0]);

                    let result = M(qubits[0]);
                    
                    if(result == One)
                    {
                        set numOfOnes = numOfOnes + 1;
                    }
                }

                Set(Zero, qubits[0]);
            }

            return (count - numOfOnes, numOfOnes);
        }
    }

    operation BellTestSuperposition (count : Int, initial : Result) : (Int, Int)
    {
        body 
        {
            mutable numOfOnes = 0;
            using (qubits = Qubit[1])
            {
                for (test in 1..count)
                {
                    Set (initial, qubits[0]);

                    H(qubits[0]);

                    let result = M(qubits[0]);
                    
                    if(result == One)
                    {
                        set numOfOnes = numOfOnes + 1;
                    }
                }

                Set(Zero, qubits[0]);
            }

            return (count - numOfOnes, numOfOnes);
        }
    }

    operation BellTestEntangled (count : Int, initial : Result) : (Int, Int, Int)
    {
        body 
        {
            mutable numOfOnes = 0;
            mutable agree = 0;
            using (qubits = Qubit[2])
            {
                for (test in 1..count)
                {
                    Set (initial, qubits[0]);
                    Set (Zero, qubits[1]);
                    H(qubits[0]);
                    CNOT(qubits[0], qubits[1]);

                    let result = M(qubits[0]);
                    
                    if(result == One)
                    {
                        set numOfOnes = numOfOnes + 1;
                    }

                    if(result == M(qubits[1]))
                    {
                        set agree = agree + 1;
                    }
                }

                Set(Zero, qubits[0]);
                Set(Zero, qubits[1]);
            }

            return (count - numOfOnes, numOfOnes, agree);
        }
    }
}
 