using System.Collections.Generic;

namespace Flip_Chess.Chesses.Extensions
{
    partial class CollectionExtensions
    {
        public static IEnumerable<History> GetBlackHistory(this ChessType[,,] collection, int index)
        {
            int h = collection.Height();
            int w = collection.Width();

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    ChessType item = collection[index, y, x];
                    if (item.IsBlack() is false) continue;

                    if (x > 0)
                    {
                        ChessType left = collection[index, y, x - 1];
                        if (item.BlackClick(left) is ClickAction.Capture)
                        {
                            yield return new History(y, x, y, x - 1);
                        }
                    }

                    if (y > 0)
                    {
                        ChessType top = collection[index, y - 1, x];
                        if (item.BlackClick(top) is ClickAction.Capture)
                        {
                            yield return new History(y, x, y - 1, x);
                        }
                    }

                    if (x + 1 < w)
                    {
                        ChessType right = collection[index, y, x + 1];
                        if (item.BlackClick(right) is ClickAction.Capture)
                        {
                            yield return new History(y, x, y, x + 1);
                        }

                        if (y + 1 < h)
                        {
                            ChessType bottom = collection[index, y + 1, x];
                            if (item.BlackClick(bottom) is ClickAction.Capture)
                            {
                                yield return new History(y, x, y + 1, x);
                            }
                        }
                    }
                }
            }
        }

        public static NeighborType GetBlackNeighbor(this ChessType[,,] collection, int index, int y, int x)
        {
            int h = collection.Height();
            int w = collection.Width();

            NeighborType type = default;
            ChessType item = collection[index, y, x];
            if (item.IsBlack() is false) return default;

            if (x > 0)
            {
                ChessType left = collection[index, y, x - 1];
                if (item.BlackClick(left) is ClickAction.Capture)
                {
                    type |= NeighborType.Left;
                }
            }

            if (y > 0)
            {
                ChessType top = collection[index, y - 1, x];
                if (item.BlackClick(top) is ClickAction.Capture)
                {
                    type |= NeighborType.Top;
                }
            }

            if (x + 1 < w)
            {
                ChessType right = collection[index, y, x + 1];
                if (item.BlackClick(right) is ClickAction.Capture)
                {
                    type |= NeighborType.Right;
                }
            }

            if (y + 1 < h)
            {
                ChessType bottom = collection[index, y + 1, x];
                if (item.BlackClick(bottom) is ClickAction.Capture)
                {
                    type |= NeighborType.Bottom;
                }
            }

            return type;
        }
    }
}