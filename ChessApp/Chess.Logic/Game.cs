using System.Text;

namespace Chess.GameLogic;

public class Game
{
    private const string startPositionFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    private Board board;
    private Moves moves;

    public string? Fen { get; private set; }
    public Game(string fen = startPositionFen)
    {
        Fen = fen;
        board = new Board(fen);
        moves = new Moves(board);
    }

    public void Move(string? move) // Pe2e4   Pe7e8Q
    {
        move = move ?? throw new ArgumentNullException(nameof(move));
        
        MovingFigure figureMoving = new MovingFigure(move);
        if (!moves.CanMove(figureMoving) || board.IsCheckedAfterMove(figureMoving))
        {
            return;
        }
        
        board = board.Move(figureMoving);
        Fen = board.Fen;
        moves = new Moves(board);
    }

    private char GetFigureAt(int x, int y)
    {
        Square square = new Square(x, y);
        Figure figure = board.GetFigureAt(square);
        return figure == Figure.None ? '.' : (char)figure;
    }

    private List<MovingFigure> FindAllMoves()
    {
        List<MovingFigure> allMoves = new();
        foreach (FigureOnSquare figure in board.GetAllFigures())
        {
            foreach (Square to in Board.GetAllSquares)
            {
                MovingFigure figureMoving = new MovingFigure(figure, to);
                if (moves.CanMove(figureMoving) && !board.IsCheckedAfterMove(figureMoving))
                {
                    allMoves.Add(figureMoving);
                }
            }
        }

        return allMoves;
    }

    public List<string> GetAllMoves()
        => FindAllMoves()
        .Select(figureMoving => figureMoving.ToString()).ToList();

    public bool IsCheck() => board.IsCheck();

    public bool IsCheckMate() => board.IsCheckMate();

    public string DrawBoard()
    {
        string border = "  +-----------------+\n";
        string labelLetter = "    a b c d e f g h\n";
        StringBuilder text = new StringBuilder();
        text.Append(border);
        for (int y = 7; y >= 0; y--)
        {
            text.Append(y + 1);
            text.Append(" | ");
            for (int x = 0; x < 8; x++)
            {
                text.Append(GetFigureAt(x, y));
                text.Append(' ');
            }

            text.Append("|\n");
        }

        text.Append(border);
        text.Append(labelLetter);
        return text.ToString();
    }
}