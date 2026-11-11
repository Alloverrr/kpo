using System;
using System.IO;

namespace лаба2
{
    public class Game
    {
        public int size;
        public Player cat;
        public Player mouse;
        public bool isGameOver;

        // статические поля для путей к файлам
        public static string InputFile = "1.ChaseData.txt";
        public static string OutputFile = "1.PursuitLog.txt";

        private StreamWriter writer;

        // Конструктор игры
        public Game(int size)
        {
            this.size = size;
            cat = new Player("Cat");
            mouse = new Player("Mouse");
            isGameOver = false;
        }
        public void Run()
        {
            // Открываем файл для записи
            writer = new StreamWriter(OutputFile);

            // Пишем шапку таблицы
            writer.WriteLine("Cat and Mouse");
            writer.WriteLine();
            writer.WriteLine("Cat Mouse  Distance");
            writer.WriteLine("-------------------");

            // Читаем все строки из входного файла
            string[] lines = File.ReadAllLines(InputFile);

            // Начинаем с 1, так как 0-я строка это размер поля
            for (int i = 1; i < lines.Length; i++)
            {
                if (isGameOver) break; // Если игра кончилась, останавливаемся

                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                // Разбиваем строку по пробелам
                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                char command = parts[0][0];
                int value = 0;

                // Если есть второе слово, преобразуем его в число
                if (parts.Length > 1)
                {
                    value = int.Parse(parts[1]);
                }

                // Выполняем команду
                switch (command)
                {
                    case 'M':
                        mouse.Move(value, size);
                        CheckCatch(); // Проверяем, не поймали ли мышь
                        break;
                    case 'C':
                        cat.Move(value, size);
                        CheckCatch();
                        break;
                    case 'P':
                        PrintState(); // Выводим состояние
                        break;
                }
            }

            // Если игра закончилась, но никто не поймал (выжила мышь)
            if (!isGameOver)
            {
                PrintFinalResult(false);
            }

            writer.Close(); // Закрываем файл
        }
        private void PrintState()
        {
            // Если игрок не в игре, выводим "??", иначе его координату
            string catPos = (cat.state == State.NotInGame) ? "??" : cat.location.ToString();
            string mousePos = (mouse.state == State.NotInGame) ? "??" : mouse.location.ToString();

            // PadLeft делает ровные столбцы (добавляет пробелы слева)
            writer.WriteLine($"{catPos.PadLeft(3)} {mousePos.PadLeft(5)} {GetDistance().ToString().PadLeft(9)}");
        }

        private int GetDistance()
        {
            // Расстояние всегда положительное
            if (cat.state == State.NotInGame || mouse.state == State.NotInGame)
                return 0;

            return Math.Abs(cat.location - mouse.location);
        }

        private void CheckCatch()
        {
            // Если оба играют и оказались на одной клетке
            if (mouse.state == State.Playing && cat.state == State.Playing)
            {
                if (mouse.location == cat.location)
                {
                    isGameOver = true;
                    mouse.state = State.Loser;
                    cat.state = State.Winner;
                    PrintFinalResult(true); // Выводим результат (поймали)
                }
            }
        }

        private void PrintFinalResult(bool caught)
        {
            writer.WriteLine("-------------------");
            writer.WriteLine();
            writer.WriteLine();
            writer.WriteLine("Distance traveled:   Mouse    Cat");
            writer.WriteLine($"                        {mouse.distanceTraveled}      {cat.distanceTraveled}");
            writer.WriteLine();

            if (caught)
                writer.WriteLine($"Mouse caught at: {mouse.location}");
            else
                writer.WriteLine("Mouse evaded Cat");
        }
    }
}