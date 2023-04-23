using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Flip_Chess.Elements
{
    public sealed partial class RevolvingRestaurant : UserControl
    {
        readonly Rotater R0 = new Rotater(0);
        readonly Rotater R1 = new Rotater(90);
        readonly Rotater R2 = new Rotater(180);
        readonly Rotater R3 = new Rotater(270);

        readonly string[] Text = new string[]
        {
            "中", "国", "象", "棋", "翻", "翻", "棋"
        };

        #region DependencyProperty

        public bool Paused
        {
            get => (bool)base.GetValue(PausedProperty);
            set => base.SetValue(PausedProperty, value);
        }
        public static readonly DependencyProperty PausedProperty = DependencyProperty.Register(nameof(Paused), typeof(bool), typeof(RevolvingRestaurant), new PropertyMetadata(false, (sender, e) =>
        {
            RevolvingRestaurant control = (RevolvingRestaurant)sender;
            if (control.IsLoaded is false) return;

            if (e.NewValue is bool value)
            {
                if (value)
                    control.Storyboard.Stop();
                else
                    control.Storyboard.Begin();
            }
        }));

        #endregion

        public RevolvingRestaurant()
        {
            this.InitializeComponent();
            base.Loaded += (s, e) =>
            {
                if (this.Paused)
                    this.Storyboard.Stop();
                else
                    this.Storyboard.Begin();
            };
        }
    }
}