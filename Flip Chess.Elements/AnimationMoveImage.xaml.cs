﻿using System;
using System.Numerics;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Flip_Chess.Elements
{
    public sealed partial class AnimationMoveImage : UserControl
    {
        public object CommandParameter { get; set; }
        public ICommand Command { get; set; }

        public Uri ImageSource
        {
            get => this.BitmapImage.UriSource;
            set => this.BitmapImage.UriSource = value;
        }

        public AnimationMoveImage()
        {
            this.InitializeComponent();
            this.Storyboard.Completed += (s, e) =>
            {
                this.Command?.Execute(this.CommandParameter); // Command
            };
        }

        public void Stop()
        {
            this.Storyboard.Stop(); // Storyboard
        }

        public void Begin()
        {
            this.Storyboard.Begin(); // Storyboard
        }

        public void Hide()
        {
            base.Visibility = Visibility.Collapsed;
        }

        public void Show(Vector2 from, Vector2 to)
        {
            this.CompositeTransform.ScaleX = 1;
            this.CompositeTransform.ScaleY = 1;

            this.CompositeTransform.TranslateX = from.X;
            this.CompositeTransform.TranslateY = from.Y;

            this.XAnimation.From = from.X;
            this.YAnimation.From = from.Y;

            this.XAnimation.To = to.X;
            this.YAnimation.To = to.Y;

            base.Visibility = Visibility.Visible;
        }
    }
}