using System;

namespace Gomoku
{
    public enum Piece
    {
        Empty,
        Black,
        White
    }

    public static class PieceExtensions
    {
        //private const char BlackSquare = '●';
        //private const char WhiteSquare = '◌';
        private const char BlackSquare = 'B';

        private const char WhiteSquare = 'W';

        public static char ToChar(this Piece piece)
        {
            switch (piece)
            {
                case Piece.Empty:
                    return ' ';

                case Piece.Black:
                    return BlackSquare;

                case Piece.White:
                    return WhiteSquare;

                default:
                    throw new ArgumentOutOfRangeException(nameof(piece), piece, null);
            }
        }

        public static Player ToPlayer(this Piece piece)
        {
            switch (piece)
            {
                case Piece.Empty:
                    throw new Exception("LG 害人不浅");
                case Piece.Black:
                    return Player.Black;

                case Piece.White:
                    return Player.White;

                default:
                    throw new ArgumentOutOfRangeException(nameof(piece), piece, null);
            }
        }
    }
}