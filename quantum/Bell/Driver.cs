using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;

using static System.Console;

namespace Bell
{
    class Driver
    {
        static void Main(string[] args)
        {
            Test_Simple();
            Test_Flipped();
            Test_Superposition();
            Test_Entangled();
        }

        private static void Test_Simple()
        {
            WriteLine($"\r\n{nameof(Test_Simple)}");
            var header = $"{"Initial", -10}|{Result.Zero, -10}|{Result.One, -10}|";
            WriteLine(new string('-', header.Length));
            WriteLine(header);
            WriteLine(new string('-', header.Length));

            using (var simulator = new QuantumSimulator())
            {
                var nRuns = 10_000;
                var initials = new [] { Result.Zero, Result.One };
                foreach (var init in initials)
                {
                    var (nZero, nOne) =
                        Quantum.BellTestSimple.Run(simulator, nRuns, init).Result;
                    WriteLine($"{init, -10}|{nZero, -10}|{nOne, -10}");                
                }

            }
        }
        private static void Test_Flipped()
        {
            WriteLine($"\r\n{nameof(Test_Flipped)}");
            var header = $"{"Initial", -10}|{Result.Zero, -10}|{Result.One, -10}|";
            WriteLine(new string('-', header.Length));
            WriteLine(header);
            WriteLine(new string('-', header.Length));

            using (var simulator = new QuantumSimulator())
            {
                var nRuns = 10_000;
                var initials = new [] { Result.Zero, Result.One };
                foreach (var init in initials)
                {
                    var (nZero, nOne) =
                        Quantum.BellTestFlipped.Run(simulator, nRuns, init).Result;
                    WriteLine($"{init, -10}|{nZero, -10}|{nOne, -10}");                
                }

            }
        }

        private static void Test_Superposition()
        {
            WriteLine($"\r\n{nameof(Test_Superposition)}");
            var header = $"{"Initial", -10}|{Result.Zero, -10}|{Result.One, -10}|";
            WriteLine(new string('-', header.Length));
            WriteLine(header);
            WriteLine(new string('-', header.Length));

            using (var simulator = new QuantumSimulator())
            {
                var nRuns = 10_000;
                var initials = new [] { Result.Zero, Result.One };
                foreach (var init in initials)
                {
                    var (nZero, nOne) =
                        Quantum.BellTestSuperposition.Run(simulator, nRuns, init).Result;
                    WriteLine($"{init, -10}|{nZero, -10}|{nOne, -10}");                
                }

            }
        }

        private static void Test_Entangled()
        {
            WriteLine($"\r\n{nameof(Test_Entangled)}");
            var header = $"{"Initial", -10}|{Result.Zero, -10}|{Result.One, -10}|" + 
            $"{"Entangled",-10}|";
            WriteLine(new string('-', header.Length));
            WriteLine(header);
            WriteLine(new string('-', header.Length));

            using (var simulator = new QuantumSimulator())
            {
                var nRuns = 10_000;
                var initials = new [] { Result.Zero, Result.One };
                foreach (var init in initials)
                {
                    var (nZero, nOne, nAgreed) =
                        Quantum.BellTestEntangled.Run(simulator, nRuns, init).Result;
                    WriteLine($"{init, -10}|{nZero, -10}|{nOne, -10}|{nAgreed,-10}|");                
                }

            }
        }
    }
}