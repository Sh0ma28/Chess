using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Square // класс клетки доски
    {

        public int x { get; private set; }
        public int y { get; private set; }

        public Square (int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Square (string e2) // перевод в координаты x, y
        {
            if (e2.Length == 2 &&
                e2[0] >= 'a' && e2[0] <= 'h' &&
                e2[1] >= '1' && e2[1] <= '8')
            {
                x = e2[0] - 'a';
                y = e2[1] - '1';
            }
            else this.x = this.y = -1;
        }

        public bool OnBoard () 
        {
            return x >= 0 && x < 8 && y >= 0 && y < 8;
        }

        //операторы для сравнения клеток
        public static bool operator == (Square a, Square b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator != (Square a, Square b)
        {
            return !(a == b);
        }

    }
}
