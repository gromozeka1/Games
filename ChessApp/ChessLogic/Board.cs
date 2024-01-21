using ChessLogic.Moves;
using ChessLogic.Pieces;

namespace ChessLogic;
public class Board
{
    private readonly Piece[,] pieces = new Piece[8,8];
    private readonly Dictionary<Player, Position> pawnSkipPositions = new()
    {
        {Player.White, null },
        {Player.Black, null },
    };
    public Piece this[int row, int column]
    {
        get => pieces[row, column];
        set => pieces[row, column] = value;
    }

    public Piece this[Position position]
    {
        get => pieces[position.Row, position.Column];
        set => pieces[position.Row, position.Column] = value;
    }

    public Position GetPawnSkipPosition(Player player)
    {
        return pawnSkipPositions[player];
    }

    public void SetPawnSkipPosition(Player player, Position position)
    {
        pawnSkipPositions[player] = position;
    }

    public static Board Initial()
    {
        Board board = new Board();
        board.AddStartPieces();
        return board;
    }

    private void AddStartPieces()
    {
        this[0, 0] = new Rook(Player.Black);
        this[0, 1] = new Knight(Player.Black);
        this[0, 2] = new Bishop(Player.Black);
        this[0, 3] = new Queen(Player.Black);
        this[0, 4] = new King(Player.Black);
        this[0, 5] = new Bishop(Player.Black);
        this[0, 6] = new Knight(Player.Black);
        this[0, 7] = new Rook(Player.Black);

        this[7, 0] = new Rook(Player.White);
        this[7, 1] = new Knight(Player.White);
        this[7, 2] = new Bishop(Player.White);
        this[7, 3] = new Queen(Player.White);
        this[7, 4] = new King(Player.White);
        this[7, 5] = new Bishop(Player.White);
        this[7, 6] = new Knight(Player.White);
        this[7, 7] = new Rook(Player.White);

        for (int i = 0; i < 8; i++)
        {
            this[1, i] = new Pawn(Player.Black);
            this[6, i] = new Pawn(Player.White);
        }
    }

    public static bool IsInside(Position position)
    {
        return position.Row >= 0 && position.Row < 8 && position.Column >= 0 && position.Column < 8;
    }

    public bool IsEmpty(Position position)
    {
        return this[position] == null;
    }

    public IEnumerable<Position> PiecePositions()
    {
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                Position position = new Position(r,c);
                if (!IsEmpty(position))
                {
                    yield return position;
                }
            }
        }
    }

    public IEnumerable<Position> PiecePositionsFor(Player player)
    {
        return PiecePositions().Where(pos => this[pos].Color == player);
    }

    public bool IsInCheck(Player player)
    {
        return PiecePositionsFor(player.Opponent()).Any(pos =>
        {
            Piece piece = this[pos];
            return piece.CanCaptureOpponentKing(pos, this);
        });
    }

    public Board Copy()
    {
        Board copy = new Board();
        foreach (var piecePosition in PiecePositions())
        {
            copy[piecePosition] = this[piecePosition].Copy();
        }

        return copy;
    }

    public Counting CountPieces()
    {
        var counting = new Counting();
        foreach (var piecePosition in PiecePositions())
        {
            Piece piece = this[piecePosition];
            counting.Increment(piece.Color, piece.Type);
        }

        return counting;
    }

    public bool InsufficientMaterial()
    {
        Counting counting = CountPieces();

        return IsKingVsKing(counting) || IsKingBishopVsKing(counting) || IsKingKnightVsKing(counting) || IsKingBishopVsKingBishop(counting);
    }

    private static bool IsKingVsKing(Counting counting)
    {
        return counting.TotalCount == 2;
    }

    private static bool IsKingBishopVsKing(Counting counting)
    {
        return counting.TotalCount == 3 
            && (counting.White(PieceType.Bishop) == 1 || counting.Black(PieceType.Bishop) == 1);
    }

    private static bool IsKingKnightVsKing(Counting counting)
    {
        return counting.TotalCount == 3
            && (counting.White(PieceType.Knight) == 1 || counting.Black(PieceType.Knight) == 1);
    }

    private bool IsKingBishopVsKingBishop(Counting counting)
    {
        if (counting.TotalCount != 4)
        {
            return false;
        }

        if (counting.White(PieceType.Bishop) != 1 || counting.Black(PieceType.Bishop) != 1)
        {
            return false;
        }

        Position whiteBishopPosition = FindPiece(Player.White, PieceType.Bishop);
        Position blackBishopPosition = FindPiece(Player.Black, PieceType.Bishop);

        return whiteBishopPosition.SquareColor() == blackBishopPosition.SquareColor();
    }

    private Position FindPiece(Player color, PieceType type)
    {
        return PiecePositionsFor(color).First(pos => this[pos].Type == type);
    }

    private bool IsUnmovedKingAndRook(Position kingPosition, Position rookPosition)
    {
        if (IsEmpty(kingPosition) || IsEmpty(rookPosition))
        {
            return false;
        }

        Piece king = this[kingPosition];
        Piece rook = this[rookPosition];

        return king.Type == PieceType.King && rook.Type == PieceType.Rook && !king.HasMoved && !rook.HasMoved;
    }

    public bool CatleRightKingSide(Player player)
    {
        return player switch
        {
            Player.White => IsUnmovedKingAndRook(new Position(7, 4), new Position(7, 7)), 
            Player.Black => IsUnmovedKingAndRook(new Position(0, 4), new Position(0, 7)),
            _ => false,
        };
    }

    public bool CatleRightQueenSide(Player player)
    {
        return player switch
        {
            Player.White => IsUnmovedKingAndRook(new Position(7, 4), new Position(7, 0)),
            Player.Black => IsUnmovedKingAndRook(new Position(0, 4), new Position(0, 0)),
            _ => false,
        };
    }

    public bool CanCaptureEnPassant(Player player)
    {
        Position skipPosition = GetPawnSkipPosition(player.Opponent());

        if (skipPosition == null)
        {
            return false;
        }

        Position[] pawnPositions = player switch
        {
            Player.White => [skipPosition + Direction.SouthWest, skipPosition + Direction.SouthWest,],
            Player.Black => [skipPosition + Direction.NorthWest, skipPosition + Direction.NorthWest,],
            _ => [],
        };

        return HasPawnInPosition(player, pawnPositions, skipPosition);
    }

    private bool HasPawnInPosition(Player player, Position[] pawnPositions, Position skipPosition)
    {
        foreach (Position position in pawnPositions.Where(IsInside))
        {
            Piece piece = this[position];
            if (piece is null || piece.Color != player || piece.Type != PieceType.Pawn)
            {
                continue;
            }

            EnPassant move = new EnPassant(position, skipPosition);
            if (move.IsLegal(this))
            {
                return true;
            }
        }

        return false;
    }
}
