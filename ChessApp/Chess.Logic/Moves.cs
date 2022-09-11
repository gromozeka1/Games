namespace Chess.GameLogic
{
    public class Moves
    {
        private MovingFigure? figureMoving;

        private readonly Board board;

        public Moves(Board board) => this.board = board;

        public bool CanMove(MovingFigure figureMoving)
        {
            this.figureMoving = figureMoving;
            return CanMoveFrom()
                && CanMoveTo()
                && CanFigureMove();
        }

        private bool CanMoveFrom()
            => figureMoving.From.OnBoard()
            && figureMoving.Figure.GetColor() == board.MoveColor;

        private bool CanMoveTo()
            => figureMoving.To.OnBoard()
            && figureMoving.From != figureMoving.To
            && board.GetFigureAt(figureMoving.To).GetColor() != board.MoveColor;

        private bool CanFigureMove() => figureMoving.Figure switch
        {
            Figure.WhiteKing or Figure.BlackKing => CanKingMove(),
            Figure.WhiteQueen or Figure.BlackQueen => CanQueenMove(),
            Figure.WhiteRook or Figure.BlackRook => CanRookMove(),
            Figure.WhiteBishop or Figure.BlackBishop => CanBishopMove(),
            Figure.WhiteKnight or Figure.BlackKnight => CanKnightMove(),
            Figure.WhitePawn or Figure.BlackPawn => CanPawnMove(),
            Figure.None => false,
            _ => false,
        };

        private bool CanPawnMove()
        {
            if (figureMoving.From.Y is < 1 or > 8)
            {
                return false;
            }

            int stepY = figureMoving.Figure.GetColor() == Color.White ? 1 : -1;
            return CanPawnGo(stepY) || CanPawnJump(stepY) || CanPawnEat(stepY);
        }

        private bool CanPawnEat(int stepY)
            => !NoFigureAt(figureMoving.To)
            && figureMoving.AbsDeltaX == 1
            && figureMoving.DeltaY == stepY;

        private bool CanPawnJump(int stepY)
            => NoFigureAt(figureMoving.To)
            && IsJump(figureMoving.DeltaX, figureMoving.AbsDeltaY)
            && IsStartPosition(figureMoving.From.Y)
            && NoFigureAt(new Square(figureMoving.From.X, figureMoving.From.Y + stepY));

        private bool NoFigureAt(Square to)
            => board.GetFigureAt(to) == Figure.None;

        private static bool IsStartPosition(int y) => y is 1 or 6;

        private static bool IsJump(int deltaX, int absDeltaY)
            => deltaX == 0 && absDeltaY == 2;

        private bool CanPawnGo(int stepY)
            => board.GetFigureAt(figureMoving.To) == Figure.None
            && figureMoving.DeltaX == 0
            && figureMoving.DeltaY == stepY;

        private bool CanKnightMove() => figureMoving.AbsDeltaX switch
        {
            1 when figureMoving.AbsDeltaY == 2 => true,
            2 when figureMoving.AbsDeltaY == 1 => true,
            _ => false
        };

        private bool CanBishopMove()
            => figureMoving.SignX != 0 && figureMoving.SignY != 0
            && CanStraightMove();

        private bool CanRookMove()
            => (figureMoving.SignX == 0 || figureMoving.SignY == 0)
            && CanStraightMove();

        private bool CanQueenMove() => CanStraightMove();

        private bool CanKingMove()
            => figureMoving.AbsDeltaX <= 1 && figureMoving.AbsDeltaY <= 1;

        private bool CanStraightMove()
        {
            Square at = figureMoving.From;

            do
            {
                at = new Square(at.X + figureMoving.SignX, at.Y + figureMoving.SignY);
                if (at == figureMoving.To)
                {
                    return true;
                }
            }
            while (at.OnBoard() && board.GetFigureAt(at) == Figure.None);

            return false;
        }
    }
}
