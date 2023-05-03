using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Flip_Chess.Elements
{
    public sealed partial class Highlight : Canvas
    {
        public bool CanAnimate => false;

        public Highlight()
        {
            this.InitializeComponent();
        }

        public void Stop() => this.Storyboard.Stop(); // Storyboard
        public void Begin() => this.Storyboard.Begin(); // Storyboard

        public void Hide() => base.Visibility = Visibility.Collapsed;
        public void ShowAt(int x, int y)
        {
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
            base.Opacity = 1;
            base.Visibility = Visibility.Visible;
        }
    }
}