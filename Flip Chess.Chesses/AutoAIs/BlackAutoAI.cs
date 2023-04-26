using Flip_Chess.Chesses.Extensions;
using System.Linq;
using Ene = Flip_Chess.Chesses.AutoAIs.RedAutoAI;
using Fri = Flip_Chess.Chesses.AutoAIs.BlackAutoAI;

namespace Flip_Chess.Chesses.AutoAIs
{
    public sealed class BlackAutoAI
    {
        public readonly int Index;

        public readonly int Step;
        public readonly History History;
        public readonly int Level;

        internal Ene[] Children;

        internal BlackAutoAI(IIndexer indexer, History history, int parentIndex, int parentStep)
        {
            // 1. Index
            lock (indexer)
            {
                indexer.Index++;
                this.Index = indexer.Index;
            }

            if (this.Index + 1 >= indexer.Collection.ZIndex()) return;
            indexer.Collection.Copy(this.Index, parentIndex);

            // 2. History
            this.Step = parentStep + 1;

            this.History = history;
            switch ((HistoryAction)history.Distance)
            {
                case HistoryAction.Capture:
                    indexer.Collection[this.Index, history.Y1, history.X1] = ChessType.Deaded;
                    indexer.Collection[this.Index, history.Y2, history.X2] = indexer.Collection[parentIndex, history.Y1, history.X1];
                    break;
                default:
                    break;
            }

            this.Level = indexer.Collection.GetLevel(this.Index);
        }

        internal bool Birth(IIndexer indexer)
        {
            if (this.Index + 1 >= indexer.Collection.ZIndex()) return false;

            // 3. Children
            History[] historion = indexer.Collection.GetRedHistory(this.Index).ToArray();

            if (historion is null)
                this.Children = new Ene[] { new Ene(indexer, History.Noway, this.Index, this.Step) };
            else if (historion.Length is 0)
                this.Children = new Ene[] { new Ene(indexer, History.Noway, this.Index, this.Step) };
            else
            {
                this.Children = new Ene[historion.Length];
                for (int i = 0; i < historion.Length; i++)
                {
                    History item = historion[i];
                    this.Children[i] = new Ene(indexer, item, this.Index, this.Step);
                }
            }
            return true;
        }

        internal float GetAmout() => (float)GetLevelSum(this) / (float)GetCount(this);

        internal static int GetLevelSum(Fri autoAI)
        {
            if (autoAI.Children is null) return autoAI.Level;
            if (autoAI.Children.Length is 0) return autoAI.Level;

            return autoAI.Level + autoAI.Children.Sum(Ene.GetLevelSum);
        }

        internal static int GetCount(Fri autoAI)
        {
            if (autoAI.Children is null) return 1;
            if (autoAI.Children.Length is 0) return 1;

            return 1 + autoAI.Children.Sum(Ene.GetCount);
        }

        public override string ToString() => $"⭕{this.Index:000} A:{this.GetAmout()} L:{this.Level} {this.History}";
    }
}