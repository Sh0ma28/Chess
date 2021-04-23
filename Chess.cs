using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Chess // основной связующий класс
    {
        public string fen { get; private set; }
        Board board;
        Moves moves;

        public Chess (string fen)
        {
            this.fen = fen;
            board = new Board(fen);
            moves = new Moves(board);
        }

        Chess (Board board)
        {
            this.board = board;
            this.fen = board.fen;
            moves = new Moves(board);
        }

        public Chess Move (string move)
        {
            FigureMoving fm = new FigureMoving(move);
            if (!moves.CanMove(fm))
            {
                Console.WriteLine("Wrong move! Try again.");
                return this;
            }
            Board nextBoard = board.Move(fm);
            Chess nextChess = new Chess(nextBoard);
            return nextChess;
        }

        public char GetFigureAt (int x, int y)
        {
            Square square = new Square(x, y);
            Figure f = board.GetFigureAt(square);
            return f == Figure.none ? '.' : (char)f;
        }
    }
}
