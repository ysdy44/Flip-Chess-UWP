using Flip_Chess.Chesses;
using Flip_Chess.Chesses.AutoAIs;
using Flip_Chess.Chesses.Extensions;
using Flip_Chess.Models;
using System;
using System.Windows.Input;

namespace Flip_Chess
{
    partial class MainPage
    {

        //@Delegate
        public event EventHandler CanExecuteChanged;

        //@Command
        public bool CanExecute(object parameter) => parameter != default;
        public void Execute(object parameter)
        {
            if (parameter is OptionType item)
            {
                this.Click(item);
            }
        }

        public ICommand Command => this;
        public void Click(OptionType type)
        {
        }

    }
}