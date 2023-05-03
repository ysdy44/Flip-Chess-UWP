using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Flip_Chess.Elements
{
    public sealed partial class AnimationFadeContainer : UserControl
    {
        public bool CanAnimate => false;

        public AnimationFadeContainer()
        {
            this.InitializeComponent();

            if (base.IsEnabled)
            {
                base.Visibility = Visibility.Visible;
                base.IsHitTestVisible = true;
            }
            else
            {
                base.Visibility = Visibility.Collapsed;
                base.IsHitTestVisible = false;
            }

            base.IsEnabledChanged += (s, e) =>
            {
                if (base.IsLoaded is false) return;

                if (base.IsEnabled)
                {
                    this.ShowStoryboard.Begin(); // Storyboard
                    base.Visibility = Visibility.Visible;
                }
                else
                {
                    this.HideStoryboard.Begin(); // Storyboard
                    base.IsHitTestVisible = false;
                }
            };

            this.HideStoryboard.Completed += (s, e) =>
            {
                base.Visibility = Visibility.Collapsed;
            };

            this.ShowStoryboard.Completed += (s, e) =>
            {
                base.IsHitTestVisible = true;
            };
        }
    }
}