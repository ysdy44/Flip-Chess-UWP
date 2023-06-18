using System;
using System.Numerics;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Flip_Chess.Elements
{
    public sealed partial class AnimationZoomImage : UserControl
    {
        public object CommandParameter { get; set; }
        public ICommand Command { get; set; }

        public Uri PlaceholderSource { get; set; }
        Uri ImageSource;

        public AnimationZoomImage()
        {
            this.InitializeComponent();
            base.Loaded += (s, e) =>
            {
                this.Hide();
            };
            this.HideStoryboard.Completed += (s, e) =>
            {
                this.BitmapImage.UriSource = this.ImageSource;

                this.ShowStoryboard.Begin(); // Storyboard
            };
            this.ShowStoryboard.Completed += (s, e) =>
            {
                this.Command?.Execute(this.CommandParameter); // Command

                this.Hide();
            };
        }

        public void Stop()
        {
            this.HideStoryboard.Stop(); // Storyboard
            this.ShowStoryboard.Stop(); // Storyboard
        }
        public void Begin()
        {
            this.HideStoryboard.Begin(); // Storyboard
        }

        public void Hide()
        {
            this.BitmapImage.UriSource = this.PlaceholderSource;
            base.Visibility = Visibility.Collapsed;
        }
        public void Show(Vector2 position, Uri uri)
        {
            this.CompositeTransform.ScaleX = 1;
            this.CompositeTransform.ScaleY = 1;

            this.CompositeTransform.TranslateX = position.X;
            this.CompositeTransform.TranslateY = position.Y;

            this.ImageSource = uri;
            this.BitmapImage.UriSource = this.PlaceholderSource;
            base.Visibility = Visibility.Visible;
        }
    }
}