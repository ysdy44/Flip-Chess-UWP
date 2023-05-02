using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Flip_Chess.Elements
{
    public sealed partial class Highlight : Canvas
    {
        public Highlight()
        {
            this.InitializeComponent();
        }

        public void Hide()
        {
            base.Visibility = Visibility.Collapsed;
            this.Storyboard.Stop(); // Storyboard
        }

        public void ShowAt(int x, int y)
        {
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
            base.Visibility = Visibility.Visible;
            this.Storyboard.Begin(); // Storyboard
        }
    }
}