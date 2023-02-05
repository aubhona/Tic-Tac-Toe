using TicTacToeLib;

namespace TicTacToe;

public static class Program
{

    private static void Main()
    {
        while (true)
        {
            Console.WriteLine("Нажмите Enter, чтобы начать.");
            if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                return;
            }

            Map.ReadSize();
            
            try
            {
                Map.Draw();
                break;
            }
            catch (ArgumentException)
            {
                Map.Reset();
                Console.WriteLine("Ошибка! Недостаточно места консоли, попробуйте изменить размер и начать сначала.");
            }
        }
        bool step = true;
        while (true)
        {
            var win = Map.Move(Map.ReadNum(step), step);
            if (win is 1 or 0)
            {
                Console.Clear();
                switch (win)
                {
                    case 1:
                        Console.WriteLine(step ? "Крестики выиграли!" : "Нолики выиграли!");
                        break;
                    case 0:
                        Console.WriteLine("Ничья!");
                        break;
                }

                Console.WriteLine("Чтобы начать новую игру, нажмите Enter.");
                
                if (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    return;
                }

                step = true;
                Map.Reset();
                Map.ReadSize();
                Map.Draw();
                continue;
            }
            step = !step;
        }
        
    }
}