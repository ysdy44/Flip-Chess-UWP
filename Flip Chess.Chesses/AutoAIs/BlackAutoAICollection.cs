﻿using Flip_Chess.Chesses.Extensions;

namespace Flip_Chess.Chesses.AutoAIs
{
    public sealed class BlackAutoAICollection : AutoAI
    {
        public BlackAutoAICollection(IZIndexer indexer) : base(indexer) { }
        internal BlackAutoAICollection(IZIndexer indexer, int parentZIndex, History history) : base(indexer, parentZIndex, history) { }

        protected override int DefaultValue() => int.MaxValue;
        protected override bool EqualsValue(int thanDefault, int amout) => thanDefault > amout;
        protected override void CreateHistory(IZIndexer indexer, int zIndex)
        {
            ChessType[,,] array = indexer.Collection;
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
                            base.Add(new BlackAutoAICollection(indexer, zIndex, new History(y, x, y, x - 1)));
                        }
                    }

                    if (y > 0)
                    {
                        ChessType top = array[zIndex, y - 1, x];
                        if (item.BlackRelateTo(top) is HistoryRelation.WeakEnemy)
                        {
                            base.Add(new BlackAutoAICollection(indexer, zIndex, new History(y, x, y - 1, x)));
                        }
                    }

                    if (x + 1 < w)
                    {
                        ChessType right = array[zIndex, y, x + 1];
                        if (item.BlackRelateTo(right) is HistoryRelation.WeakEnemy)
                        {
                            base.Add(new BlackAutoAICollection(indexer, zIndex, new History(y, x, y, x + 1)));
                        }

                        if (y + 1 < h)
                        {
                            ChessType bottom = array[zIndex, y + 1, x];
                            if (item.BlackRelateTo(bottom) is HistoryRelation.WeakEnemy)
                            {
                                base.Add(new BlackAutoAICollection(indexer, zIndex, new History(y, x, y + 1, x)));
                            }
                        }
                    }
                }
            }

            if (base.Count == 0)
            {
                base.Add(new BlackAutoAICollection(indexer, zIndex, History.Noway));
            }
        }
    }
}