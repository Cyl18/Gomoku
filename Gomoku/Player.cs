using System;

namespace Gomoku
{
    public enum Player
    {
        Black,
        White
    }

    public static class PlayerExtensions
    {
        public static Piece ToPiece(this Player player)
        {
            switch (player)
            {
                case Player.Black:
                    return Piece.Black;

                case Player.White:
                    return Piece.White;

                default:
                    throw new ArgumentOutOfRangeException(nameof(player), player, null);
            }
        }
    }
}