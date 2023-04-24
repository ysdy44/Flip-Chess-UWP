using Flip_Chess.Chesses;
using Flip_Chess.Chesses.AutoAIs;
using Flip_Chess.Chesses.Extensions;
using Flip_Chess.TestApp.Models;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Flip_Chess.TestApp
{
    public sealed partial class MainPage : Page, IIndexer
    {
        readonly DispatcherTimer Timer = new DispatcherTimer
        {
            Interval = System.TimeSpan.FromSeconds(1)
        };

        // History
        public int Step => this.Historian.Count;
        public bool IsRed => this.Step % 2 == 0; // Index
        public bool IsBlack => this.Step % 2 != 0; // Index
        public IList<History> Historian { get; } = new List<History>();

        // Collection
        public int Index { get; set; } // Index
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
                if (this.IsRed)
                {
                    this.ItemClick(this.Collection.RandomFlip(new RedAutoAICollection(this).FindAutoAI()));
                }
                else if (this.IsBlack)
                {
                    this.ItemClick(this.Collection.RandomFlip(new BlackAutoAICollection(this).FindAutoAI()));
                }
            };
            this.Timer.Start();
        }
    }
}