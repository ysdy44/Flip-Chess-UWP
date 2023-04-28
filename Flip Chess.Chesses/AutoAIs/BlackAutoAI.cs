using Flip_Chess.Chesses.Extensions;
using System;
using System.Collections.Generic;

namespace Flip_Chess.Chesses.AutoAIs
{
    public abstract partial class AutoAI : List<AutoAI>, IDisposable
    {
        readonly int ZIndex;
        readonly History History;
        readonly int Level;

        internal AutoAI(IZIndexer indexer, int parentZIndex, History history)
        {
            // 1. Index
            lock (indexer)
            {
                indexer.ZIndex++;
                this.ZIndex = indexer.ZIndex;
            }

            if (this.ZIndex + 1 >= indexer.Collection.ZIndex()) return;
            indexer.Collection.Copy(this.ZIndex, parentZIndex);

            // 2. History
            this.History = history;
            switch ((HistoryAction)history.Distance)
            {
                case HistoryAction.Capture:
                    indexer.Collection[this.ZIndex, history.Y1, history.X1] = ChessType.Deaded;
                    indexer.Collection[this.ZIndex, history.Y2, history.X2] = indexer.Collection[parentZIndex, history.Y1, history.X1];
                    break;
                default:
                    break;
            }

            this.Level = indexer.Collection.GetLevel(this.ZIndex);
        }
        
        protected abstract int DefaultValue();
        protected abstract bool EqualsValue(int thanDefault, int amout);
        protected abstract void CreateHistory(IZIndexer indexer, int zIndex);

        private int GetValueForce()
        {
            if (base.Count is 0) return this.Level;
            
            int defaultValue = this.DefaultValue();
            bool find = false;

            foreach (AutoAI item in this)
            {
                if (item.History == default) continue;
                if (item.History == History.Noway) continue;
                
                int value = item.GetValueForce();

                if (this.EqualsValue(defaultValue, value))
                {
                    defaultValue = value;
                    find = true;
                }
            }

            return find ? defaultValue : this.Level;
        }

        public override string ToString() => $"{this.ZIndex:000} V:{this.GetValueForce()} L:{this.Level} {this.History}";

        public void Dispose()
        {
            foreach (AutoAI item in this)
            {
                item.Dispose();
            }
            base.Clear();
        }
    }
}