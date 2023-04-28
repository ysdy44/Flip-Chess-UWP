using Flip_Chess.Chesses.Extensions;
using System.Linq;

namespace Flip_Chess.Chesses.AutoAIs
{
    partial class AutoAI
    {
        protected AutoAI(IZIndexer indexer)
        {
            // 1. Index
            lock (indexer)
            {
                indexer.ZIndex = 0;
                this.ZIndex = 0;
            }

            // 2. History
            this.Level = indexer.Collection.GetLevel(0);

            // 3. Children
            this.CreateHistory(indexer, 0);

            // 1
            if (base.Count is 0) return;
            foreach (AutoAI item in this)
            {
                if (item.ZIndex + 1 >= indexer.Collection.ZIndex()) return;
                item.CreateHistory(indexer, item.ZIndex);
            }

            // 2
            foreach (AutoAI item in this)
            {
                if (item is null) return;
                if (item.Count is 0) return;
                foreach (AutoAI item2 in item)
                {
                    if (item2 is null) return;
                    if (item2.ZIndex + 1 >= indexer.Collection.ZIndex()) return;
                    item2.CreateHistory(indexer, item2.ZIndex);
                }
            }

            // 3
            foreach (AutoAI item in this)
                foreach (AutoAI item2 in item)
                {
                    if (item2 is null) return;
                    if (item2.Count is 0) return;
                    foreach (AutoAI item3 in item2)
                    {
                        if (item3 is null) return;
                        if (item3.ZIndex + 1 >= indexer.Collection.ZIndex()) return;
                        item3.CreateHistory(indexer, item3.ZIndex);
                    }
                }

            // 4
            foreach (AutoAI item in this)
                foreach (AutoAI item2 in item)
                    foreach (AutoAI item3 in item2)
                    {
                        if (item3 is null) return;
                        if (item3.Count is 0) return;
                        foreach (AutoAI item4 in item3)
                        {
                            if (item4 is null) return;
                            if (item4.ZIndex + 1 >= indexer.Collection.ZIndex()) return;
                            item4.CreateHistory(indexer, item4.ZIndex);
                        }
                    }

            // 5
            foreach (AutoAI item in this)
                foreach (AutoAI item2 in item)
                    foreach (AutoAI item3 in item2)
                        foreach (AutoAI item4 in item3)
                        {
                            if (item4 is null) return;
                            if (item4.Count is 0) return;
                            foreach (AutoAI item5 in item4)
                            {
                                if (item5 is null) return;
                                if (item5.ZIndex + 1 >= indexer.Collection.ZIndex()) return;
                                item5.CreateHistory(indexer, item5.ZIndex);
                            }
                        }
        }

        public History FindAutoAI()
        {
            if (base.Count is 0) return History.Noway;
            
            int defaultValue = this.DefaultValue();
            AutoAI find = null;

            System.Diagnostics.Debug.WriteLine(this.GetType().Name);
            foreach (AutoAI item in this)
            {
                System.Diagnostics.Debug.WriteLine(item.ToString());

                int value = item.GetValueForce();

                if (this.EqualsValue(defaultValue, value))
                {
                    defaultValue = value;
                    find = item;
                }
            }

            if (find != null)
            {
                if (find.History != History.Noway)
                {
                    if (this.EqualsValue(this.Level, find.Level))
                    {
                        return find.History;
                    }
                }
            }

            if (find != null)
            {
                if (find.History != History.Noway)
                {
                    return find.History;
                }
            }

            foreach (AutoAI item in this)
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