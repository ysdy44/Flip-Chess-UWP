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
        public event ItemClickEventHandler ItemsItemClick { remove => this.ItemsControl.ItemClick -= value; add => this.ItemsControl.ItemClick += value; }

        public int SelectedIndex { get => this.FlipView.SelectedIndex; set => this.FlipView.SelectedIndex = value; }

        public string Text1 { get => this.Run1.Text; set => this.Run1.Text = value; }
        public string Text2 { get => this.Run2.Text; set => this.Run2.Text = value; }

        public int RedValue
        {
            get => (int)(this.RedProgressBar.Height / 4);
            set => this.RedProgressBar.Height = System.Math.Max(0, value * 4);
        }
        public int BlackValue
        {
            get => (int)(this.BlackProgressBar.Height / 4);
            set => this.BlackProgressBar.Height = System.Math.Max(0, value * 4);
        }

        public bool IsRedComputer
        {
            get => this.RedListBox.SelectedIndex == 0;
            set => this.RedListBox.SelectedIndex = value ? 0 : 1;
        }
        public bool IsBlackComputer
        {
            get => this.BlackListBox.SelectedIndex == 0;
            set => this.BlackListBox.SelectedIndex = value ? 0 : 1;
        }

        public GameMode Mode
        {
            get => (GameMode)this.ListBox.SelectedIndex;
            set => this.ListBox.SelectedIndex = (int)value;
        }

        #region DependencyProperty

        public GameState State
        {
            get => (GameState)base.GetValue(StateProperty);
            set => base.SetValue(StateProperty, value);
        }
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(nameof(State), typeof(GameState), typeof(MainPage), new PropertyMetadata(GameState.None));

        #endregion

        public void ClickFullScreen()
        {
            if (this.ApplicationView.IsFullScreenMode)
            {
                this.ClickUnFullScreen();
            }
            else
            {
                VisualStateManager.GoToState(this, nameof(FullScreen), false);
                this.ApplicationView.TryEnterFullScreenMode();
            }
        }
        public void ClickUnFullScreen()
        {
            VisualStateManager.GoToState(this, nameof(UnFullScreen), false);
            this.ApplicationView.ExitFullScreenMode();
        }

        public void HideLines()
        {
            this.CenterHighlight.Visibility = Visibility.Collapsed;

            this.LeftHighlight.Hide();
            this.TopHighlight.Hide();
            this.RightHighlight.Hide();
            this.BottomHighlight.Hide();
        }
        public void ShowLinesAt(int y, int x, NeighborType type)
        {
            Canvas.SetLeft(this.CenterHighlight, x * 100);
            Canvas.SetTop(this.CenterHighlight, y * 100);
            this.CenterHighlight.Visibility = Visibility.Visible;

            if (type.HasFlag(NeighborType.Left))
                this.LeftHighlight.ShowAt(x * 100 - 100, y * 100);
            else
                this.LeftHighlight.Hide();

            if (type.HasFlag(NeighborType.Top))
                this.TopHighlight.ShowAt(x * 100, y * 100 - 100);
            else
                this.TopHighlight.Hide();

            if (type.HasFlag(NeighborType.Right))
                this.RightHighlight.ShowAt(x * 100 + 100, y * 100);
            else
                this.RightHighlight.Hide();

            if (type.HasFlag(NeighborType.Bottom))
                this.BottomHighlight.ShowAt(x * 100, y * 100 + 100);
            else
                this.BottomHighlight.Hide();
        }

        public Vector2 GetCemeteryPosition(ChessType toType)
        {
            if (toType.IsRed())
            {
                int i = -1;
                foreach (ChessDeaded item in this.RedCemetery)
                {
                    i++;
                    if (item.Type != toType) continue;

                    Vector2 visual = this.RedCemeteryCanvas.TransformToVisual(this.ItemsControl).TransformPoint(default).ToVector2();
                    return new Vector2
                    {
                        X = visual.X - 100 / 2 + 60 / 2,
                        Y = visual.Y + i * 60 - 100 / 2 + 60 / 2
                    };
                }
            }

            if (toType.IsBlack())
            {
                int i = -1;
                foreach (ChessDeaded item in this.BlackCemetery)
                {
                    i++;
                    if (item.Type != toType) continue;

                    Vector2 visual = this.BlackCemeteryCanvas.TransformToVisual(this.ItemsControl).TransformPoint(default).ToVector2();
                    return new Vector2
                    {
                        X = visual.X - 100 / 2 + 60 / 2,
                        Y = visual.Y + i * 60 - 100 / 2 + 60 / 2
                    };
                }
            }

            return default;
        }

        public void SetMargin(int w, int h, int margin)
        {
            if (w > h + margin)
            {
                this.Viewbox.Margin = new Thickness(margin, 0, margin, 0);
            }
            else if (w < h - margin)
            {
                this.Viewbox.Margin = new Thickness(0, margin, 0, margin);
            }
            else
            {
                this.Viewbox.Margin = new Thickness(margin);
            }
        }

        private void StopFlip() => this.FlipItem.Stop();
        public void BeginFlip() => this.FlipItem.Begin(); /// <see cref="OptionType.UIFlipCompleted"/>
        private void HideFlip() => this.FlipItem.Hide();
        public void ShowFlip(Vector2 position, Uri uri) => this.FlipItem.Show(position, uri);

        private void StopCapture() => this.CaptureItem.Stop();
        public void BeginCapture() => this.CaptureItem.Begin();  /// <see cref="OptionType.UICaptureCompleted"/>
        private void HideCapture() => this.CaptureItem.Hide();
        public void ShowCapture(Vector2 from, Vector2 to, Uri uri) => this.CaptureItem.Show(from, to, uri);

        private void StopCemetery() => this.CemeteryItem.Stop();
        public void BeginCemetery() => this.CemeteryItem.Begin();  /// <see cref="OptionType.UICemeteryCompleted"/>
        private void HideCemetery() => this.CemeteryItem.Hide();
        public void ShowCemetery(Vector2 from, Vector2 to, Uri uri) => this.CemeteryItem.Show(from, to, uri);

        public void StopClip() => this.ClipContainer.Stop();
        public void BeginClip() => this.ClipContainer.Begin(); /// <see cref="OptionType.UIClipCompleted"/>
    }
}