using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Flip_Chess.Elements
{
    public sealed class CommandListBox : ListBox
    {
        public object CommandParameter { get; set; }
        public ICommand Command { get; set; }
        public CommandListBox()
        {
            base.SelectionChanged += (s, e) =>
            {
                this.Command?.Execute(this.CommandParameter); // Command
            };
        }
    }
}