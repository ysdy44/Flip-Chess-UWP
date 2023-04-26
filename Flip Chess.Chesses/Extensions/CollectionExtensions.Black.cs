using System.Collections.Generic;

namespace Flip_Chess.Chesses.Extensions
{
    partial class CollectionExtensions
    {
        public static IEnumerable<History> GetBlackHistory(this ChessType[,,] array, int zIndex)
        {
            int h = array.Height();
            int w = array.Width();

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    ChessType item = array[zIndex, y, x];
                    if (item.IsBlack() is false) continue;

                    if (x > 0)
                    {
                        ChessType left = array[zIndex, y, x - 1];
                        if (item.BlackRelateTo(left) is HistoryRelation.WeakEnemy)
                        {
                            yield return new History(y, x, y, x - 1);
                        }
                    }

                    if (y > 0)
                    {
                        ChessType top = array[zIndex, y - 1, x];
                        if (item.BlackRelateTo(top) is HistoryRelation.WeakEnemy)
                        {
                            yield return new History(y, x, y - 1, x);
                        }
                    }

                    if (x + 1 < w)
                    {
                        ChessType right = array[zIndex, y, x + 1];
                        if (item.BlackRelateTo(right) is HistoryRelation.WeakEnemy)
                        {
                            yield return new History(y, x, y, x + 1);
                        }

                        if (y + 1 < h)
                        {
                            ChessType bottom = array[zIndex, y + 1, x];
                            if (item.BlackRelateTo(bottom) is HistoryRelation.WeakEnemy)
                            {
                                yield return new History(y, x, y + 1, x);
                            }
                        }
                    }
                }
            }
        }

        public static NeighborType GetBlackNeighbor(this ChessType[,,] array, int zIndex, int y, int x)
        {
            int h = array.Height();
            int w = array.Width();

            NeighborType type = default;
            ChessType item = array[zIndex, y, x];
            if (item.IsBlack() is false) return default;

            if (x > 0)
            {
                ChessType left = array[zIndex, y, x - 1];
                if (item.BlackRelateTo(left) is HistoryRelation.WeakEnemy)
                {
                    type |= NeighborType.Left;
                }
            }

            if (y > 0)
            {
                ChessType top = array[zIndex, y - 1, x];
                if (item.BlackRelateTo(top) is HistoryRelation.WeakEnemy)
                {
                    type |= NeighborType.Top;
                }
            }

            if (x + 1 < w)
            {
                ChessType right = array[zIndex, y, x + 1];
                if (item.BlackRelateTo(right) is HistoryRelation.WeakEnemy)
                {
                    type |= NeighborType.Right;
                }
            }

            if (y + 1 < h)
            {
                ChessType bottom = array[zIndex, y + 1, x];
                if (item.BlackRelateTo(bottom) is HistoryRelation.WeakEnemy)
                {
                    type |= NeighborType.Bottom;
                }
            }

            return type;
        }
    }
}