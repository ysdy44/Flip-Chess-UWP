using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Flip_Chess.Elements
{
    public sealed class CommandListView : ListView
    {
        public ICommand Command { get; set; }
        public CommandListView()
        {
            base.ItemClick += (s, e) =>
            {
                this.Command?.Execute(e.ClickedItem); // Command
            };
        }
    }
}