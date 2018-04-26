using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    public class Grid
    {
        public const int DefaultX = 15;
        public const int DefaultY = 15;
        internal readonly Piece[,] Pieces;

        public Grid(int x, int y)
        {
            if (x <= 0 || y <= 0) throw new ArgumentOutOfRangeException();
            Pieces = new Piece[x, y];
        }

        public Piece Get(Point point) =>
            point.X >= MaxX || point.Y >= MaxY || point.X < 0 || point.Y < 0
            ? Piece.Empty
            : Pieces[point.X, point.Y];

        public void Set(Point point, Piece piece)
        {
            if (Pieces[point.X, point.Y] != Piece.Empty)
            {
                throw new Exception("Piece already placed.");
            }
            Pieces[point.X, point.Y] = piece;
        }

        public int MaxX => Pieces.GetLength(0);

        public int MaxY => Pieces.GetLength(1);

        public static Grid CreateDefault() => new Grid(DefaultX, DefaultY);
    }
}