using Flip_Chess.Chesses;
using Flip_Chess.Chesses.Extensions;
using Flip_Chess.Models;
using System;
using System.Numerics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Flip_Chess
{
    partial class MainPage
    {
        #region DependencyProperty

        public GameState State
        {
            get => (GameState)base.GetValue(StateProperty);
            set => base.SetValue(StateProperty, value);
        }
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(nameof(State), typeof(GameState), typeof(MainPage), new PropertyMetadata(GameState.None));

        #endregion
    }
}