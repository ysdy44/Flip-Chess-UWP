using Flip_Chess.Chesses.Extensions;
using System.Linq;

namespace Flip_Chess.Chesses.AutoAIs
{
    public sealed class RedAutoAICollection
    {
        public int Level;
        public readonly RedAutoAI[] Children;

        public RedAutoAICollection(IIndexer indexer)
        {
            // 1. Index
            lock (indexer)
            {
                indexer.Index = 0;
            }

            // 2. History
            this.Level = indexer.Collection.GetLevel(0);

            // 3. Children
            var historion = indexer.Collection.GetRedHistory(0).ToArray();

            if (historion is null) return;
            if (historion.Length is 0) return;

            this.Children = new RedAutoAI[historion.Length];
            for (int i = 0; i < historion.Length; i++)
            {
                var item = historion[i];
                this.Children[i] = new RedAutoAI(indexer, item, 0, indexer.Step);
            }

            // 1
            if (this.Children is null) return;
            if (this.Children.Length is 0) return;
            foreach (var item in this.Children)
            {
                if (item.Birth(indexer) is false)
                {
                    return;
                }
            }

            // 2
            foreach (var item in this.Children)
            {
                if (item.Children is null) return;
                if (item.Children.Length is 0) return;
                foreach (var item2 in item.Children)
                {
                    if (item2.Birth(indexer) is false)
                    {
                        return;
                    }
                }
            }

            // 3
            foreach (var item in this.Children)
                foreach (var item2 in item.Children)
                {
                    if (item2.Children is null) return;
                    if (item2.Children.Length is 0) return;
                    foreach (var item3 in item2.Children)
                    {
                        if (item3.Birth(indexer) is false)
                        {
                            return;
                        }
                    }
                }

            // 4
            foreach (var item in this.Children)
                foreach (var item2 in item.Children)
                    foreach (var item3 in item2.Children)
                    {
                        if (item3.Children is null) return;
                        if (item3.Children.Length is 0) return;
                        foreach (var item4 in item3.Children)
                        {
                            if (item4.Birth(indexer) is false)
                            {
                                return;
                            }
                        }
                    }

            // 5
            foreach (var item in this.Children)
                foreach (var item2 in item.Children)
                    foreach (var item3 in item2.Children)
                        foreach (var item4 in item3.Children)
                        {
                            if (item4.Children is null) return;
                            if (item4.Children.Length is 0) return;
                            foreach (var item5 in item4.Children)
                            {
                                if (item5.Birth(indexer) is false)
                                {
                                    return;
                                }
                            }
                        }
        }

        public RedAutoAI FindNext()
        {
            if (this.Children is null) return null;
            if (this.Children.Length is 0) return null;

            float level = this.Level;
            RedAutoAI ai = null;

            // Capture
            foreach (RedAutoAI item in this.Children)
            {
                float amout = item.GetAmout();

                if (level <= amout)
                {
                    level = amout;
                    ai = item;
                }
            }

            return ai;
        }

        public History FindAutoAI(IIndexer indexer)
        {
            RedAutoAI next = this.FindNext();

            if (next != null)
            {
                if (next.History != default)
                {
                    if (this.Level <= next.Level)
                    {
                        return next.History;
                    }
                }
            }

            // Flip
            for (int y = 0; y < indexer.Collection.Height(); y++)
            {
                for (int x = 0; x < indexer.Collection.Width(); x++)
                {
                    ChessType item = indexer.Collection[0, y, x];

                    if (item is ChessType.Unkonw)
                    {
                        return new History(y, x);
                    }
                }
            }

            if (next != null)
            {
                if (next.History != default)
                {
                    return next.History;
                }
            }

            if (this.Children != null)
            {
                foreach (var item in this.Children)
                {
                    return item.History;
                }
            }

            return History.Noway;
        }
    }
}