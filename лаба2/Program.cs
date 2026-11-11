using System;

namespace лаба2
{
    class Program
    {
        static void Main(string[] args)
        {
            var tests = new (string input, string output, int size)[]
            {
                ("1.ChaseData.txt", "1.PursuitLog.txt", 16),
                ("2.ChaseData.txt", "2.PursuitLog.txt", 20),
                ("3.ChaseData.txt", "3.PursuitLog.txt", 27)
            };

            foreach (var test in tests)
            {
                Game.InputFile = test.input;
                Game.OutputFile = test.output;

                Game game = new Game(test.size);
                game.Run();
                Console.WriteLine($"Готово: {test.output}");
            }

            Console.WriteLine("Все тесты завершены. Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}