﻿using Flip_Chess.Chesses;
using Flip_Chess.Chesses.AutoAIs;
using Flip_Chess.Chesses.Extensions;
using Flip_Chess.TestApp.Models;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Flip_Chess.TestApp
{
    public sealed partial class MainPage : Page
    {
        readonly DispatcherTimer Timer = new DispatcherTimer
        {
            Interval = System.TimeSpan.FromSeconds(1)
        };

        // History
        public int Step => this.Historian.Count;
        public IList<History> Historian { get; } = new List<History>();

        // Collection
        public int ZIndex { get; set; } // Index
        public ChessType[,,] Collection { get; } = new ChessType[1024, 8, 4]; // Sertings // Index
        public Chess[] Chesses { get; } = new Chess[8 * 4]
        {
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
        };
        public Chess[] Randoms { get; } = new Chess[8 * 4] // Sertings
        {
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
        };

        public MainPage()
        {
            this.InitializeComponent();
            this.Randoms.Home();
            this.Randoms.Random();

            this.Timer.Tick += (s, e) =>
            {
                if (this.Step.IsRed())
                {
                    this.ItemClick(new RedAutoAICollection(this.Collection).FindAutoAI(this.Collection));
                }
                else if (this.Step.IsBlack())
                {
                    this.ItemClick(new BlackAutoAICollection(this.Collection).FindAutoAI(this.Collection));
                }
            };
            this.Timer.Start();
        }
    }
}