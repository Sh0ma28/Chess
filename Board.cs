using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Board // класс доски
    {
        public string fen { get; private set; }
        Figure[,] figures;
        public Color moveColor { get; private set; }

        public Board (string fen)
        {
            this.fen = fen;
            figures = new Figure[8, 8];
            Init();
        }

        void Init ()
        {
            string[] parts = fen.Split();
            if (parts.Length != 2) return;
            InitFigures(parts[0]);
            moveColor = parts[1] == "b" ? Color.black : Color.white;
        }

        void InitFigures (string data) // заполнение доски
        {
            for(int i = 8; i >= 2; i--)
                data = data.Replace(i.ToString(), (i - 1).ToString() + "1");
            data = data.Replace("1", ".");
            string[] lines = data.Split('/');
            for (int y = 7; y >= 0; y--)
                for (int x = 0; x < 8; x++)
                    figures[x, y] = lines[7-y][x] == '.' ? Figure.none : (Figure)lines[7 - y][x];
        }

        public Board Move(FigureMoving fm)
        {
            Board next = new Board(fen);
            next.SetFigureAt(fm.from, Figure.none);
            next.SetFigureAt(fm.to, fm.figure);
            next.moveColor = moveColor.FlipColor();
            next.GenerateFen();
            return next;
        }

        public Figure GetFigureAt(Square square)
        {
            if (square.OnBoard())
                return figures[square.x, square.y];
            return Figure.none;
        }

        void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBoard())
                figures[square.x, square.y] = figure;
        }

        void GenerateFen () 
        {
            fen = FenFigures() + " " + (moveColor == Color.white ? "w" : "b");
        }

        string FenFigures() // генерация фигур в фене
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                    sb.Append(figures[x, y] == Figure.none ? '1' : (char)figures[x, y]);
                if (y > 0)
                    sb.Append('/');
            }
            string eights = "11111111";
            for (int i = 8; i >= 2; i--)
                sb.Replace(eights.Substring(0, i), i.ToString());
            return sb.ToString();
        }
    }
}
