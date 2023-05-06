using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Flip_Chess.Elements
{
    public sealed partial class AnimationClipContainer : UserControl
    {
        public object CommandParameter { get; set; }
        public ICommand Command { get; set; }

        public bool CanAnimate => true;

        public AnimationClipContainer()
        {
            this.InitializeComponent();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
             
                this.Geometry.Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);
            };
            this.HideStoryboard.Completed += (s, e) =>
            {
                this.Command?.Execute(this.CommandParameter); // Command

                this.ShowStoryboard.Begin(); // Storyboard
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
    }
}