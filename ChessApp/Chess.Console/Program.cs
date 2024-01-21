using Chess.GameLogic;

namespace Chess.ConsoleClient;

public class Program
{
    public static void Main(string[] args)
    {
        var chess = new Game();
        
        List<string> allMoves;
        
        while (true)
        {
            Console.WriteLine(chess.Fen);
            Print(chess.DrawBoard());

            Console.WriteLine(chess.IsCheck() ? "CHECK" : "-");

            allMoves = chess.GetAllMoves();
            PrintAllMoves(allMoves);

            if (!ContinueGame(allMoves, out string? move))
            {
                break;
            }

            chess.Move(move);
        }
    }

    private static bool ContinueGame(List<string> allMoves, out string? move)
    {
        Random random = new Random();
        Console.Write("> ");
        move = Console.ReadLine();
        if (move == "q")
        {
            return false;
        }

        if (move == "")
        {
            move = allMoves[random.Next(allMoves.Count)];
        }

        Console.WriteLine($"Your move: {move}");
        return true;
    }

    private static void PrintAllMoves(List<string> allMoves)
    {
        allMoves.ForEach(x => Console.Write(x + "\t"));
        Console.WriteLine();
    }

    public static void Print(string text)
    {
        ConsoleColor oldColor = Console.ForegroundColor;
        foreach (char x in text)
        {
            Console.ForegroundColor =
                x is >= 'a' and <= 'z' ? ConsoleColor.Red :
                x is >= 'A' and <= 'Z' ? ConsoleColor.White :
                ConsoleColor.Cyan;
            Console.Write(x);
        }

        Console.ForegroundColor = oldColor;
    }
}