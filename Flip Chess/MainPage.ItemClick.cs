﻿using Flip_Chess.Chesses;
using Flip_Chess.Chesses.Extensions;
using Flip_Chess.Models;
using System.Numerics;
using Windows.UI.Xaml;

namespace Flip_Chess
{
    partial class MainPage
    {
        private void ItemClick(IChess item)
        {
            int index = -1;
            foreach (ChessAlive item2 in this.Chesses)
            {
                index++;
                if (item2 == item) break;
            }

            int y = index / this.Collection.Width(); // Y
            int x = index % this.Collection.Width();  // X

            HistoryRelation relation =
                this.IsBlack ?
                this.Previous.BlackRelateTo(item.Type) :
                this.Previous.RedRelateTo(item.Type);

            switch ((HistoryAction)(int)relation)
            {
                case HistoryAction.Noway:
                    break;
                case HistoryAction.Select:
                    NeighborType type =
                        this.IsBlack ?
                        this.Collection.GetBlackNeighbor(0, y, x) :
                        this.Collection.GetRedNeighbor(0, y, x);

                    this.ShowLinesAt(y, x, type);

                    this.Previous = item.Type;
                    this.PreviousY = y;
                    this.PreviousX = x;
                    break;
                case HistoryAction.Flip:
                    this.ItemClick(new History(y, x));

                    this.Previous = default;
                    this.PreviousY = -1;
                    this.PreviousX = -1;
                    break;
                case HistoryAction.Capture:
                    if (this.Previous == default) break;
                    if (this.PreviousY == -1) break;
                    if (this.PreviousX == -1) break;

                    this.ItemClick(new History(this.PreviousY, this.PreviousX, y, x));

                    this.Previous = default;
                    this.PreviousY = -1;
                    this.PreviousX = -1;
                    break;
                default:
                    break;
            }
        }

        public void ItemClick(History history)
        {
            if (history.Distance is HistoryDistance.Others) return;

            this.Timer.Stop();
            this.Timer.Start();

            this.Historian.Add(history);
            this.SaveStep(this.Step);
   
            switch ((HistoryAction)history.Distance)
            {
                case HistoryAction.Noway:
                    {
                        if (this.IsBlack) this.Click(OptionType.Lose);
                        else if (this.IsRed) this.Click(OptionType.Win);
                    }
                    break;
                case HistoryAction.Flip:
                    {
                        this.HideFlip();
                        this.StopFlip();

                        this.Shown();


                        // Info
                        var position = 100 * new Vector2
                        {
                            Y = history.Y,
                            X = history.X
                        };

                        int index = this.Collection.IndexOf(history.Y, history.X);
                        ChessType random = this.Randoms[index].Type;
                        this.Collection[0, history.Y, history.X] = random;
                        this.WriteCollection(); // Sertings

                        // UI
                        ChessAlive model = this.Chesses[index];

                        model.Type = random;
                        model.Visibility = Visibility.Collapsed;

                        // Storyboard
                        this.ShowFlip(position, model.ImageSource);
                        this.BeginFlip(); /// <see cref="OptionType.UIFlipCompleted"/>
                    }
                    break;
                case HistoryAction.Capture:
                    {
                        this.HideFlip();
                        this.StopFlip();

                        this.HideCapture();
                        this.StopCapture();

                        this.HideCemetery();
                        this.StopCemetery();

                        this.Deaded();
                        this.Shown();

                        // Info
                        ChessType fromType = this.Collection[0, history.Y1, history.X1];
                        ChessType toType = this.Collection[0, history.Y2, history.X2];

                        Vector2 from = 100 * new Vector2
                        {
                            Y = history.Y1,
                            X = history.X1
                        };
                        Vector2 to = 100 * new Vector2
                        {
                            Y = history.Y2,
                            X = history.X2
                        };

                        this.Collection[0, history.Y1, history.X1] = ChessType.Deaded;
                        this.Collection[0, history.Y2, history.X2] = fromType;
                        this.WriteCollection(); // Sertings

                        // UI
                        int index1 = this.Collection.IndexOf(history.Y1, history.X1);
                        ChessAlive previous = this.Chesses[index1];

                        int index2 = this.Collection.IndexOf(history.Y2, history.X2);
                        ChessAlive next = this.Chesses[index2];

                        // Storyboard
                        this.ShowCapture(from, to, previous.ImageSource);
                        this.ShowCemetery(to, this.GetCemeteryPosition(toType), next.ImageSource);

                        this.BeginCapture();  /// <see cref="OptionType.UICaptureCompleted"/>

                        // UI
                        previous.Type = ChessType.Deaded;
                        previous.Visibility = Visibility.Collapsed;

                        next.Type = fromType;
                        next.Visibility = Visibility.Collapsed;
                    }
                    break;
                default:
                    break;
            }


            this.HideLines();


            bool allRed = true;
            bool allBlack = true;

            for (int y = 0; y < this.Collection.Height(); y++)
            {
                for (int x = 0; x < this.Collection.Width(); x++)
                {
                    ChessType item = this.Collection[0, y, x];
                    if (item == ChessType.Deaded) continue;
                    else if (item == ChessType.Unkonw)
                    {
                        return; 
                    }
                    else if (item.IsRed())
                    {
                        allBlack = false;
                    }
                    else if (item.IsBlack())
                    {
                        allRed = false;
                    }
                }
            }

            if (allRed) // Winner is Red
            {
                this.Click(OptionType.Win);
            }
            else if (allBlack) // Winner is Black
            {
                this.Click(OptionType.Lose);
            }
        }
    }
}