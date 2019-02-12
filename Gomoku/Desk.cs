using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gomoku
{
    public class Desk
    {
        private static string _currentMessage = string.Empty;

        public static void Play()
        {
            var currentPlayer = Player.Black;
            var grid = Grid.CreateDefault();
            var cursor = Point.Of(0, 0);

            while (true)
            {
                try
                {
                    while (true)
                    {
                        PrintGrid(grid, cursor);

                        var key = Console.ReadKey();
                        switch (key.Key)
                        {
                            case ConsoleKey.LeftArrow:
                                Move(-1, 0);
                                continue;

                            case ConsoleKey.RightArrow:
                                Move(1, 0);
                                continue;

                            case ConsoleKey.UpArrow:
                                Move(0, -1);
                                continue;

                            case ConsoleKey.DownArrow:
                                Move(0, 1);
                                continue;
                            case ConsoleKey.Spacebar:
                                break;
                        }
                        break;
                    }
                    grid.Set(cursor, currentPlayer.ToPiece());
                    _currentMessage = new string(' ', _currentMessage.Length);
                }
                catch (Exception e)
                {
                    WriteMessage(e.Message);
                    continue;
                }
                SwitchPlayer();
                PrintGrid(grid, cursor);

                if (!CheckWin(grid, out var player)) continue;

                WriteMessage($"Player: {player} win!");
                PrintGrid(grid, cursor);
                Thread.Sleep(3000);
                break;
            }

            void SwitchPlayer()
            {
                // 不好看
                currentPlayer = (Player)(((int)currentPlayer + 1) % 2);
                // currentPlayer = currentPlayer == Player.Black ? Player.White : Player.Black;
            }

            void Move(int x, int y)
            {
                var ex = cursor.X + x;
                var ey = cursor.Y + y;
                if (ex < 0 || ey < 0 || ex >= grid.MaxX || ey >= grid.MaxY) return;

                cursor.X = ex;
                cursor.Y = ey;
            }
        }

        private static void PrintGrid(Grid grid, Point cursor)
        {
            //Console.Clear();
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            Console.CursorVisible = false;
            Console.WriteLine("* Gomoku by Cyl18 *");
            Console.WriteLine("-------------------------------");
            for (var y = 0; y < grid.MaxY; y++)
            {
                for (var x = 0; x < grid.MaxX; x++)
                {
                    Console.Write($"-{grid.Get(Point.Of(x, y)).ToChar()}");
                }
                Console.WriteLine();
            }
            Console.WriteLine(_currentMessage);
            Console.CursorLeft = 0;
            Console.CursorTop = 2;
            DrawCursor(cursor);
        }

        private static void DrawCursor(Point cursor)
        {
            var x = cursor.X * 2;
            var y = cursor.Y;
            Console.CursorLeft += x;
            Console.CursorTop += y;
            Console.Write("[");
            Console.CursorLeft += 1;
            Console.Write("]");
        }

        private static void WriteMessage(string s)
        {
            _currentMessage = s;
        }

        private static bool CheckWin(Grid grid, out Player player)
        {
            player = Player.Black;

            for (var y = 0; y < grid.MaxY; y++)
                for (var x = 0; x < grid.MaxX; x++)
                {
                    var point = Point.Of(x, y);
                    var piece = grid.Get(point);
                    if (piece == Piece.Empty) continue;

                    if (!Check(grid, point, p => Point.Of(p.X + 1, p.Y)) && // 暴力
                        !Check(grid, point, p => Point.Of(p.X, p.Y + 1)) &&
                        !Check(grid, point, p => Point.Of(p.X + 1, p.Y + 1)) &&
                        !Check(grid, point, p => Point.Of(p.X + 1, p.Y - 1))) continue;
                    player = piece.ToPlayer();
                    return true;
                }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool Check(Grid grid, Point point, Func<Point, Point> action)
        {
            var currentPiece = grid.Get(point);
            return Enumerable.Range(0, 4).All(_ => currentPiece == grid.Get(GetPoint()));
            // 更加暴力
            /* return currentPiece == grid.Get(GetPoint()) &&
                   currentPiece == grid.Get(GetPoint()) &&
                   currentPiece == grid.Get(GetPoint()) &&
                   currentPiece == grid.Get(GetPoint()); // 暴力
                   */
            Point GetPoint()
            {
                point = action(point);
                return point;
            }
        }
    }

    public struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point Of(int x, int y) => new Point(x, y);
    }
}