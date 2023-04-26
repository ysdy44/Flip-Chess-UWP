using Flip_Chess.Chesses.Extensions;
using System.Linq;
using Ene = Flip_Chess.Chesses.AutoAIs.RedAutoAI;
using Fri = Flip_Chess.Chesses.AutoAIs.BlackAutoAI;

namespace Flip_Chess.Chesses.AutoAIs
{
    public sealed class BlackAutoAICollection
    {
        public int Level;
        public readonly Fri[] Children;

        public BlackAutoAICollection(IIndexer indexer)
        {
            // 1. Index
            lock (indexer)
            {
                indexer.Index = 0;
            }

            // 2. History
            this.Level = indexer.Collection.GetLevel(0);

            // 3. Children
            History[] historion = indexer.Collection.GetBlackHistory(0).ToArray();

            if (historion is null) return;
            if (historion.Length is 0) return;

            this.Children = new Fri[historion.Length];
            for (int i = 0; i < historion.Length; i++)
            {
                History item = historion[i];
                this.Children[i] = new Fri(indexer, item, 0, indexer.Step);
            }

            // 1
            if (this.Children is null) return;
            if (this.Children.Length is 0) return;
            foreach (Fri item in this.Children)
            {
                if (item.Birth(indexer) is false)
                {
                    return;
                }
            }

            // 2
            foreach (Fri item in this.Children)
            {
                if (item.Children is null) return;
                if (item.Children.Length is 0) return;
                foreach (Ene item2 in item.Children)
                {
                    if (item2.Birth(indexer) is false)
                    {
                        return;
                    }
                }
            }

            // 3
            foreach (Fri item in this.Children)
                foreach (Ene item2 in item.Children)
                {
                    if (item2.Children is null) return;
                    if (item2.Children.Length is 0) return;
                    foreach (Fri item3 in item2.Children)
                    {
                        if (item3.Birth(indexer) is false)
                        {
                            return;
                        }
                    }
                }

            // 4
            foreach (Fri item in this.Children)
                foreach (Ene item2 in item.Children)
                    foreach (Fri item3 in item2.Children)
                    {
                        if (item3.Children is null) return;
                        if (item3.Children.Length is 0) return;
                        foreach (Ene item4 in item3.Children)
                        {
                            if (item4.Birth(indexer) is false)
                            {
                                return;
                            }
                        }
                    }

            // 5
            foreach (Fri item in this.Children)
                foreach (Ene item2 in item.Children)
                    foreach (Fri item3 in item2.Children)
                        foreach (Ene item4 in item3.Children)
                        {
                            if (item4.Children is null) return;
                            if (item4.Children.Length is 0) return;
                            foreach (Fri item5 in item4.Children)
                            {
                                if (item5.Birth(indexer) is false)
                                {
                                    return;
                                }
                            }
                        }
        }

        public History FindAutoAI()
        {
            if (this.Children is null) return History.Noway;
            if (this.Children.Length is 0) return History.Noway;

            float level = this.Level;
            Fri next = null;

            foreach (Fri item in this.Children)
            {
                float amout = item.GetAmout();

                if (level >= amout)
                {
                    level = amout;
                    next = item;
                }
            }

            if (next != null)
            {
                if (next.History != History.Noway)
                {
                    if (this.Level >= next.Level)
                    {
                        return next.History;
                    }
                }
            }

            if (next != null)
            {
                if (next.History != History.Noway)
                {
                    return next.History;
                }
            }

            foreach (Fri item in this.Children)
            {
                if (item.History != History.Noway)
                {
                    return item.History;
                }
            }

            return History.Noway;
        }
    }
}