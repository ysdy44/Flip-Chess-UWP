using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Flip_Chess.Elements
{
    public abstract partial class LinesCanvas : Canvas
    {
        protected LinesCanvas(int w, int h, int unit)
        {
            this.InitializeComponent();
            foreach (Liner item in LinesCanvas.GetLines(w, h, unit))
            {
                base.Children.Add(new Line { X1 = item.X1, Y1 = item.Y1, X2 = item.X2, Y2 = item.Y2 });
            }
        }

        private static IEnumerable<Liner> GetLines(int w, int h, int unit)
        {
            // X-Coordinate
            for (int i = 0; i <= w; i++)
            {
                yield return new Liner(i * unit, 0, i * unit, h * unit);
            }
            // Y-Coordinate
            for (int i = 0; i <= h; i++)
            {
                yield return new Liner(0, i * unit, w * unit, i * unit);
            }

            //LeftTop
            Corner lt = new Corner(0, 0);
            yield return new Liner(lt.R, lt.B, lt.R, lt.BB);
            yield return new Liner(lt.R, lt.B, lt.RR, lt.B);

            // LeftBottom
            Corner lb = new Corner(0, h * unit);
            yield return new Liner(lb.R, lb.T, lb.R, lb.TT);
            yield return new Liner(lb.R, lb.T, lb.RR, lb.T);

            // RightTop
            Corner rt = new Corner(w * unit, 0);
            yield return new Liner(rt.L, rt.B, rt.L, rt.BB);
            yield return new Liner(rt.L, rt.B, rt.LL, rt.B);

            // RightBottom
            Corner rb = new Corner(w * unit, h * unit);
            yield return new Liner(rb.L, rb.T, rb.L, rb.TT);
            yield return new Liner(rb.L, rb.T, rb.LL, rb.T);

            for (int i = 1; i < h / 2; i++)
            {
                // Left Lines
                Corner l = new Corner(0, i * 2 * unit);
                yield return new Liner(l.R, l.T, l.R, l.TT);
                yield return new Liner(l.R, l.T, l.RR, l.T);
                yield return new Liner(l.R, l.B, l.R, l.BB);
                yield return new Liner(l.R, l.B, l.RR, l.B);

                // Right Lines
                Corner r = new Corner(w * unit, i * 2 * unit);
                yield return new Liner(r.L, r.T, r.L, r.TT);
                yield return new Liner(r.L, r.T, r.LL, r.T);
                yield return new Liner(r.L, r.B, r.L, r.BB);
                yield return new Liner(r.L, r.B, r.LL, r.B);
            }

            // Center Lines
            int xp = 2; //Padding
            int xs = 2; // Scale
            int yp = 1; //Padding
            int ys = 3; // Scale
            for (int y = 0; y < (h - yp - yp + ys) / ys; y++)
            {
                var y3 = y * ys + yp;
                for (int x = 0; x < (w - xp - xp + xs) / xs; x++)
                {
                    var x2 = x * xs + xp;

                    Corner c = new Corner(x2 * unit, y3 * unit);
                    yield return new Liner(c.L, c.T, c.L, c.TT);
                    yield return new Liner(c.L, c.T, c.LL, c.T);
                    yield return new Liner(c.R, c.T, c.R, c.TT);
                    yield return new Liner(c.R, c.T, c.RR, c.T);
                    yield return new Liner(c.R, c.B, c.R, c.BB);
                    yield return new Liner(c.R, c.B, c.RR, c.B);
                    yield return new Liner(c.L, c.B, c.L, c.BB);
                    yield return new Liner(c.L, c.B, c.LL, c.B);
                }
            }
        }

        public void Hide()
        {
            this.Center.Visibility = Visibility.Collapsed;

            this.Left.Visibility = Visibility.Collapsed;
            this.LeftStoryboard.Stop();

            this.Top.Visibility = Visibility.Collapsed;
            this.TopStoryboard.Stop();

            this.Right.Visibility = Visibility.Collapsed;
            this.RightStoryboard.Stop();

            this.Bottom.Visibility = Visibility.Collapsed;
            this.BottomStoryboard.Stop();
        }

        public void ShowAt(int x, int y, int unit)
        {
            Canvas.SetLeft(this.Center, x * unit);
            Canvas.SetTop(this.Center, y * unit);
            this.Center.Visibility = Visibility.Visible;
        }

        public void ShowLeftAt(int x, int y, int unit, bool isLeft)
        {
            if (isLeft)
            {
                Canvas.SetLeft(this.Left, x * unit - unit);
                Canvas.SetTop(this.Left, y * unit);
                this.Left.Visibility = Visibility.Visible;
                this.LeftStoryboard.Begin();
            }
            else
            {
                this.Left.Visibility = Visibility.Collapsed;
                this.LeftStoryboard.Stop();
            }
        }

        public void ShowTopAt(int x, int y, int unit, bool isTop)
        {
            if (isTop)
            {
                Canvas.SetLeft(this.Top, x * unit);
                Canvas.SetTop(this.Top, y * unit - unit);
                this.Top.Visibility = Visibility.Visible;
                this.TopStoryboard.Begin();
            }
            else
            {
                this.Top.Visibility = Visibility.Collapsed;
                this.TopStoryboard.Stop();
            }
        }

        public void ShowRightAt(int x, int y, int unit, bool isRight)
        {
            if (isRight)
            {
                Canvas.SetLeft(this.Right, x * unit + unit);
                Canvas.SetTop(this.Right, y * unit);
                this.Right.Visibility = Visibility.Visible;
                this.RightStoryboard.Begin();
            }
            else
            {
                this.Right.Visibility = Visibility.Collapsed;
                this.RightStoryboard.Stop();
            }
        }

        public void ShowBottomAt(int x, int y, int unit, bool isBottom)
        {
            if (isBottom)
            {
                Canvas.SetLeft(this.Bottom, x * unit);
                Canvas.SetTop(this.Bottom, y * unit + unit);
                this.Bottom.Visibility = Visibility.Visible;
                this.BottomStoryboard.Begin();
            }
            else
            {
                this.Bottom.Visibility = Visibility.Collapsed;
                this.BottomStoryboard.Stop();
            }
        }
    }
}