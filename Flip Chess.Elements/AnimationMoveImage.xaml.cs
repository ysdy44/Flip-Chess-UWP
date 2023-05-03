using System;
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

        public bool CanAnimate => false;

        public AnimationMoveImage()
        {
            this.InitializeComponent();
            this.Storyboard.Completed += (s, e) =>
            {
                this.Command?.Execute(this.CommandParameter); // Command
            };
        }

        public void Stop() => this.Storyboard.Stop(); // Storyboard
        public void Begin() => this.Storyboard.Begin(); // Storyboard

        public void Hide()
        {
            base.Visibility = Visibility.Collapsed;
            this.BitmapImage.UriSource = null;
        }
        public void Show(Vector2 from, Vector2 to, Uri uri)
        {
            this.CompositeTransform.TranslateX = from.X;
            this.CompositeTransform.TranslateY = from.Y;

            this.XAnimation.From = from.X;
            this.YAnimation.From = from.Y;

            this.XAnimation.To = to.X;
            this.YAnimation.To = to.Y;

            this.BitmapImage.UriSource = uri;
            base.Visibility = Visibility.Visible;
        }
    }
}