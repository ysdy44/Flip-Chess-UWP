using Flip_Chess.Chesses;
using Flip_Chess.Chesses.AutoAIs;
using Flip_Chess.Chesses.Extensions;
using Flip_Chess.Elements;
using Flip_Chess.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Flip_Chess
{
    public sealed partial class MainPage : Page, ICommand
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
    }
}