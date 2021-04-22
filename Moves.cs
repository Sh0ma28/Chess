using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Moves
    {
        FigureMoving fm;
        Board board;

        public Moves (Board board)
        {
            this.board = board;
        }

        public bool CanMove (FigureMoving fm)
        {
            this.fm = fm;
            return CanMoveFrom() && CanMoveTo() && CanFigureMove();
        }

        bool CanMoveFrom ()
        {
            return fm.from.OnBoard() && fm.figure.GetColor() == board.moveColor &&
                    fm.figure == board.GetFigureAt(fm.from);
        }

        bool CanMoveTo()
        {
            return fm.to.OnBoard() && fm.from != fm.to && board.GetFigureAt(fm.to).GetColor() != board.moveColor;
        }

        bool CanFigureMove ()
        {
            switch (fm.figure)
            {
                case Figure.whiteKing:
                case Figure.blackKing:
                    return CanKingMove();
                case Figure.whiteQueen:
                case Figure.blackQueen:
                    return CanStraightMove();
                case Figure.whiteRook:
                case Figure.blackRook:
                    return CanStraightMove() && (fm.AbsDeltaX == 0 || fm.AbsDeltaY == 0);
                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return CanStraightMove() && (fm.AbsDeltaX == fm.AbsDeltaY);
                case Figure.whiteKnight:
                case Figure.blackKnight:
                    return CanKnightMove();
                case Figure.whitePawn:
                case Figure.blackPawn:
                    return CanPawnMove();
                default: return false;
            }
        }

        private bool CanPawnMove()
        {
            if (fm.from.y < 1 || fm.from.y > 6) return false;
            int stepY = fm.figure.GetColor() == Color.white ? 1 : -1;
            return CanPawnGo(stepY) || CanPawnJump(stepY) || CanPawnEat(stepY);
        }

        private bool CanPawnGo(int stepY)
        {
            if (board.GetFigureAt(fm.to) == Figure.none && fm.DeltaX == 0 && fm.DeltaY == stepY)
                return true;
            return false;
        }

        private bool CanPawnJump(int stepY)
        {
            if (board.GetFigureAt(fm.to) == Figure.none && fm.DeltaX == 0 && fm.DeltaY == 2 * stepY)
                if (fm.from.y == 1 || fm.from.y == 6)
                    if (board.GetFigureAt(new Square(fm.from.x, fm.from.y + stepY)) == Figure.none)
                        return true;
            return false;
        }

        private bool CanPawnEat(int stepY)
        {
            if (board.GetFigureAt(fm.to) != Figure.none && fm.AbsDeltaX == 1 && fm.DeltaY == stepY)
                return true;
            return false;
        }

        private bool CanKingMove()
        {
            if (fm.AbsDeltaX <= 1 && fm.AbsDeltaY <= 1) return true;
            return false;
        }
        private bool CanStraightMove()
        {
            Square at = fm.from;
            do
            {
                at = new Square(at.x + fm.SignX, at.y + fm.SignY);
                if (at == fm.to)
                    return true;
            } while (at.OnBoard() && board.GetFigureAt(at) == Figure.none);
            return false;
        }

        private bool CanKnightMove()
        {
            if (fm.AbsDeltaX <= 1 && fm.AbsDeltaY <= 2) return true;
            if (fm.AbsDeltaX <= 2 && fm.AbsDeltaY <= 1) return true;
            return false;

        }
    }
}
