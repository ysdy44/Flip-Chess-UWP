using Flip_Chess.Chesses;
using Flip_Chess.Chesses.AutoAIs;
using Flip_Chess.Chesses.Extensions;
using Flip_Chess.Elements;
using Flip_Chess.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Flip_Chess
{
    internal sealed class LinesCanvas4x2 : LinesCanvas
    {
        public LinesCanvas4x2() : base(4, 2, 100) { }
    }

    internal sealed class MainLinesCanvas : LinesCanvas
    {
        public MainLinesCanvas() : base(App.Width, App.Height, 100) { }
    }

    public sealed partial class MainPage : Page, ICommand
    {
        //@Strings
        private int W => App.Width * 100;
        private int H => App.Height * 100;
        private Uri GithubLink => new Uri(App.Resource.GetString(UIType.GithubLink.ToString()));
        private Uri FeedbackLink => new Uri(App.Resource.GetString(UIType.FeedbackLink.ToString()));

        //@Converter
        private string IntToGlyphConverter(int value) => value == 0 ? "\uE115" : "\uE13D";
        private bool NoneToBoolenConverter(GameState value) => value == default;
        private bool ReverseNoneToBooleanConverter(GameState value) => value != default;
        private double BooleanToDoubleConverter(bool value) => value ? 0 : -36;
        private Visibility BooleanToVisibilityConverter(bool value) => value ? Visibility.Visible : Visibility.Collapsed;
        private Visibility WinToVisibilityConverter(GameState value) => value == GameState.Win ? Visibility.Visible : Visibility.Collapsed;
        private Visibility LoseToVisibilityConverter(GameState value) => value == GameState.Lose ? Visibility.Visible : Visibility.Collapsed;
        private Visibility PauseToVisibilityConverter(GameState value)
        {
            switch (value)
            {
                case GameState.None:
                case GameState.Pause:
                    return Visibility.Visible;
                default:
                    return Visibility.Collapsed;
            }
        }

        readonly DispatcherTimer Timer = new DispatcherTimer
        {
            Interval = System.TimeSpan.FromSeconds(1)
        };

        // Previous
        public ChessType Previous { get; set; }
        public int PreviousY { get; set; } = -1;
        public int PreviousX { get; set; } = -1;

        // History
        public int HistorianCount { get; set; } // Sertings
        public int Step => this.HistorianCount + this.Historian.Count; // Indexer
        public ObservableCollection<History> Historian { get; } = new ObservableCollection<History>();

        // Collection
        public int ZIndex { get; set; } // Indexer
        public ChessType[,,] Collection { get; } = new ChessType[1024, App.Height, App.Width]; // Sertings // Indexer
        public Chess[] Randoms { get; } = new Chess[App.Height * App.Width] // Sertings
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
        public ChessAlive[] Chesses { get; } = new ChessAlive[App.Height * App.Width]
        {
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
        };

        // Storyboard
        public ChessDeaded[] BlackCemetery { get; } = new ChessDeaded[]
        {
            new ChessDeaded(ChessType.BlackKing),
            new ChessDeaded(ChessType.BlackMandarins),
            new ChessDeaded(ChessType.BlackElephant),
            new ChessDeaded(ChessType.BlackRook),
            new ChessDeaded(ChessType.BlackKnight),
            new ChessDeaded(ChessType.BlackCannons),
            new ChessDeaded(ChessType.BlackSoldier)
        };
        public ChessDeaded[] RedCemetery { get; } = new ChessDeaded[]
        {
            new ChessDeaded(ChessType.RedKing),
            new ChessDeaded(ChessType.RedMandarins),
            new ChessDeaded(ChessType.RedElephant),
            new ChessDeaded(ChessType.RedRook),
            new ChessDeaded(ChessType.RedKnight),
            new ChessDeaded(ChessType.RedCannons),
            new ChessDeaded(ChessType.RedSoldier)
        };

        public MainPage()
        {
            this.InitializeComponent();

            this.IsRedComputer = this.SettingsRed; // Sertings
            this.IsBlackComputer = this.SettingsBlack; // Sertings
            this.Mode = (GameMode)this.SettingsMode; // Sertings
            this.State = (GameState)this.SettingsState; // Sertings
            this.HistorianCount = this.SettingsStep; // Sertings

            if (this.ReadCollection()) // Sertings
            {
                this.Shown();
            }
            else
            {
                this.Collection.Clear();
                this.WriteCollection(); // Sertings
            }

            if (this.ReadRandom()) // Sertings
            {
                this.Deaded();
            }
            else
            {
                this.Randoms.Home();
                this.Randoms.Random();
                this.WriteRandom(); // Sertings
            }

            this.Timer.Tick += (s, e) => this.Click();
            this.Timer.Start();

            // UI
            this.Text1 = this.Step.ToString();
            this.Text2 = this.Step.IsBlack() ? "黑方" : "红方";
            this.Historian.CollectionChanged += (s, e) =>
            {
                this.Text1 = this.Step.ToString();
                this.Text2 = this.Step.IsBlack() ? "黑方" : "红方";
            };

            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                this.SetMargin((int)e.NewSize.Width, (int)e.NewSize.Height, 50);
            };
        }
    }
}