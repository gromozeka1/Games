using System.Text;

namespace Chess.GameLogic;

public class Board
{
    private readonly Figure[,] figures = new Figure[8, 8];
    private readonly static List<Square> allSquares = InitSquares();

    public static List<Square> GetAllSquares => allSquares.ToList();
    public string Fen { get; private set; }
    public Color MoveColor { get; private set; }
    public int MoveNumber { get; private set; }

    public Board(string fen)
    {
        Fen = fen;
        Init();
    }

    void Init()
    {
        // "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
        //  0                                           1 2    3 4 5
        if (!IsFenValid(out string[] parts))
        {
            return;
        }

        InitFigures(parts[0]);
        InitColor(parts[1]);
        InitNumber(parts[5]);
    }

    private bool IsFenValid(out string[] parts)
    {
        parts = Fen.Split();
        return parts.Length == 6;
    }

    private void InitFigures(string figures)
    {
        for (int j = 8; j >= 2; j--)
        {
            figures = figures.Replace(j.ToString(), (j -1).ToString() + "1");
        }

        figures = figures.Replace("1", ".");

        string[] lines = figures.Split('/');
        for (int y = 7; y >= 0; y--)
        {
            for (int x = 0; x < 8; x++)
            {
                this.figures[x, y]
                    = lines[7 - y][x] == '.'
                    ? Figure.None
                    : (Figure)lines[7 - y][x];
            }
        }
    }

    public List<FigureOnSquare> GetAllFigures() => GetAllSquares
        .Where(s => GetFigureAt(s).GetColor() == MoveColor)
        .Select(s => new FigureOnSquare(GetFigureAt(s), s))
        .ToList();

    private void InitColor(string color)
        => MoveColor = color == "b" ? Color.Black : Color.White;

    private void InitNumber(string number)
        => MoveNumber = int.Parse(number);

    public Figure GetFigureAt(Square square)
        => square.OnBoard()
        ? figures[square.X, square.Y]
        : Figure.None;

    public void SetFigureAt(Square square, Figure figure)
    {
        if (square.OnBoard())
        {
            figures[square.X, square.Y] = figure;
        }
    }

    public Board Move(MovingFigure figureMoving)
    {
        Board next = new Board(Fen);
        next.SetFigureAt(figureMoving.From, Figure.None);
        next.SetFigureAt(figureMoving.To,
            figureMoving.Promotion == Figure.None
            ? figureMoving.Figure
            : figureMoving.Promotion);
        if (MoveColor == Color.Black)
        {
            next.MoveNumber++;
        }

        next.MoveColor = MoveColor.FlipColor();
        next.Fen = next.GetFen();
        return next;
    }

    private string GetFen()
        => $"{GetFenFigures()} {GetFenColor()} KQkq - 0 {MoveNumber}";

    private string GetFenFigures()
    {
        StringBuilder sb = new StringBuilder();
        for (int y = 7; y >= 0; y--)
        {
            for (int x = 0; x < 8; x++)
            {
                sb.Append((char)figures[x, y]);
            }

            if (y > 0)
            {
                sb.Append('/');
            }
        }

        string eight = "11111111";
        for (int j = 8; j >= 2; j--)
        {
            sb = sb.Replace(eight[..j], j.ToString());
        }

        return sb.ToString();
    }

    private string GetFenColor()
        => MoveColor == Color.White
        ? "w"
        : "b";

    public bool IsCheck()
    {
        Board after = new Board(Fen)
        {
            MoveColor = MoveColor.FlipColor()
        };
        return after.CanEatKing();
    }

    public bool IsCheckedAfterMove(MovingFigure figureMoving)
    {
        Board after = Move(figureMoving);
        return after.CanEatKing();
    }

    internal bool IsCheckMate()
    {
        return IsCheck() && NoEscapeMoves();
    }

    private bool NoEscapeMoves()
    {
        throw new NotImplementedException();
    }

    private bool CanEatKing()
    {
        Square enemyKingSquare = FindEnemyKing();
        Moves moves = new Moves(this);
        foreach (FigureOnSquare figureOnSquare in GetAllFigures())
        {
            MovingFigure figureMoving = new MovingFigure(figureOnSquare, enemyKingSquare);
            if (moves.CanMove(figureMoving))
            {
                return true;
            }
        }

        return false;
    }

    private Square FindEnemyKing()
    {
        Figure enemyKing = MoveColor == Color.White
            ? Figure.BlackKing
            : Figure.WhiteKing;
        foreach (Square square in allSquares)
        {
            if (GetFigureAt(square) == enemyKing)
            {
                return square;
            }
        }

        return Square.None;
    }

    public static List<Square> InitSquares()
    {
        List<Square> squareList = new List<Square>();
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                squareList.Add(new Square(x, y));
            }
        }

        return squareList;
    }
}
