using System;

namespace лаба2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Настраиваем пути к файлам (для теста 1)
            Game.InputFile = "1.ChaseData.txt";
            Game.OutputFile = "1.PursuitLog.txt";

            // Создаем игру с размером поля 16 и запускаем
            Game game = new Game(16);
            game.Run();

            Console.WriteLine("Готово! Проверь файл " + Game.OutputFile);
            Console.ReadKey();
        }
    }
}
