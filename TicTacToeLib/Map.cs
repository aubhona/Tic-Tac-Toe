

namespace TicTacToeLib;

public static class Map
{
    public static int Size { get; set; } = 4;
    private static int[,] _map = new int[Size, Size];
    private const string Xup = @"\/";
    private const string XDown = @"/\";
    private const string Oup = @"/\";
    private const string ODown = @"\/";
    private static Dictionary<int, (int X, int Y)> _coordinates = new();
    private static int _count;

    private static void DrawSym(bool isX)
    {
        int len = Math.Min(Console.WindowHeight, Console.WindowWidth) / Size - 2;
        string up, down;
        ConsoleColor color;
        if (isX)
        {
            up = Xup;
            down = XDown;
            color = ConsoleColor.Red;
        }
        else
        {
            up = Oup;
            down = ODown;
            color = ConsoleColor.Green;
        }

        int x = Console.CursorLeft;
        int y = Console.CursorTop;
        Console.SetCursorPosition(x - (len - 2) / 2, y - (len - 2) / 2);
        Console.Write("  ");
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(up);
        Console.SetCursorPosition(x, y + 1);
        Console.Write(down);
        Console.ResetColor();
    }
    public static void Draw()
    {
        Console.Clear();
        int len = Math.Min(Console.WindowHeight, Console.WindowWidth) / Size - 2;

        if (len <= 0)
        {
            throw new ArgumentException();
        }
        
        for (int i = Size - 1; i > 0; i--)
        {
            Console.SetCursorPosition(len * (i + 1), 0);
            Console.Write(string.Join('\n' + new string(' ', len * (i + 1)),
                new string('|', len * Size).ToCharArray()));
        }
        
        for (int i = 1; i < Size; i++)
        {
            for (int j = 1; j < Size + 1; j++)
            {
                Console.SetCursorPosition(len * j + 1, len * i - 1);
                Console.Write(new string('_', len - 1));
            }
        }

        int num = 1;
        Console.ForegroundColor = ConsoleColor.Cyan;
        for (int i = 1; i < Size + 1; i++)
        {
            for (int j = 1; j < Size + 1; j++)
            {
                Console.SetCursorPosition(len * j + 1, len * (i - 1));
                _coordinates.Add(num, (len * j + 1 + (len - 2) / 2, len * (i - 1) + (len - 2) / 2));
                Console.Write(num++ + ".");
                
            }
        }
        Console.ResetColor();
        Console.SetCursorPosition(0, Console.WindowHeight - 3);
    }

    private static bool CheckGame(int x, int y)
    {
        bool isWin1 = true;
        bool isWin2 = true;
        bool isWin3 = false;
        bool isWin4 = false;
        
        for (int i = 0; i < Size; i++)
        {
            if (_map[i, y] != _map[x, y])
            {
                isWin1 = false;
            }

            if (_map[x, i] != _map[x, y])
            {
                isWin2 = false;
            }

            if (!(isWin1 || isWin2))
            {
                break;
            }
        }

        if (x == y || x == Size - 1 - y)
        {
            isWin3 = true;
            isWin4 = true;
            for (int i = 0; i < Size; i++)
            {
                if (_map[i, i] != _map[x, y])
                {
                    isWin3 = false;
                }
                if (_map[i, Size - 1 - i] != _map[x, y])
                {
                    isWin4 = false;
                }
                
                if (!(isWin3 || isWin4))
                {
                    break;
                }
            }
        }
        return isWin1 || isWin2 || isWin3 || isWin4;
    }

    public static int Move(int num, bool isX)
    {
        if (_map[num / Size + (num % Size - 1 == -1 ? -1 : 0), num % Size - 1 == -1 ? Size - 1 : num % Size - 1] != 0)
        {
            return -1;
        }
        
        Console.SetCursorPosition(_coordinates[num].X, _coordinates[num].Y);
        DrawSym(isX);
        
        _map[num / Size + (num % Size - 1 == -1 ? -1 : 0), num % Size - 1 == -1 ? Size - 1 : num % Size - 1] =
            isX ? 1 : -1;
        _count++;
        
        Console.SetCursorPosition(0, Console.WindowHeight);

        return CheckGame(num / Size + (num % Size - 1 == -1 ? -1 : 0),
            num % Size - 1 == -1 ? Size - 1 : num % Size - 1) ? 1 : _count == Size * Size ? 0 : -1;

    }

    public static int ReadNum(bool isX)
    {
        string space = new string(' ', Console.WindowWidth);
        Console.SetCursorPosition(0, Console.WindowHeight - 3);
        Console.Write(space);
        Console.SetCursorPosition(0, Console.WindowHeight - 3);
        Console.WriteLine($"Введите номер ячейки, куда хотите посавить {(isX ? "крестик" : "нолик")}.");
        int num;
        while (!int.TryParse(Console.ReadLine(), out num))
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.Write(space);
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
        }
        Console.SetCursorPosition(0, Console.WindowHeight - 2);
        Console.Write(space);
        Console.SetCursorPosition(0, Console.WindowHeight - 2);
        return num;
    }

    public static void Reset()
    {
        _coordinates = new();
        _count = 0;
        Size = 3;
    }

    public static void ReadSize()
    {
        Console.WriteLine("Введите размеры поля:");
        int num;
        while (!int.TryParse(Console.ReadLine(), out num))
        {
            Console.WriteLine("Попробуйте снова.");
        }

        Size = num;
        _map = new int[Size, Size];
    }
}