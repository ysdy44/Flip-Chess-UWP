using Flip_Chess.Chesses;
using System;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace Flip_Chess.Models
{
    public sealed class ChessDeaded : IChess, INotifyPropertyChanged
    {
        public double Opacity => System.Math.Clamp(this.Count, 0, 1);
        public Visibility Visibility => this.Count > 1 ? Visibility.Visible : Visibility.Collapsed;

        public int Count
        {
            get => this.count;
            set
            {
                if (this.count == value) return;
                this.count = value;
                this.OnPropertyChanged(nameof(Opacity)); // Notify 
                this.OnPropertyChanged(nameof(Visibility)); // Notify 
                this.OnPropertyChanged(nameof(Count)); // Notify 
            }
        }
        private int count;

        public ChessType Type
        {
            get => this.type;
            set
            {
                if (this.type == value) return;
                this.type = value;
                this.OnPropertyChanged(nameof(ImageSource)); // Notify 
                this.OnPropertyChanged(nameof(Type)); // Notify 
            }
        }
        private ChessType type;

        public ChessDeaded(ChessType type) => this.type = type;

        public Uri ImageSource
        {
            get
            {
                switch (this.Type)
                {
                    case ChessType.Unkonw:
                        return new Uri("ms-appx:///Images/Item.png");
                    case ChessType.Deaded:
                        return null;
                    case ChessType.RedSoldier:
                        return new Uri("ms-appx:///Images/RedSoldier.png");
                    case ChessType.BlackSoldier:
                        return new Uri("ms-appx:///Images/BlackSoldier.png");
                    case ChessType.RedCannons:
                        return new Uri("ms-appx:///Images/RedCannons.png");
                    case ChessType.BlackCannons:
                        return new Uri("ms-appx:///Images/BlackCannons.png");
                    case ChessType.RedKnight:
                        return new Uri("ms-appx:///Images/RedKnight.png");
                    case ChessType.BlackKnight:
                        return new Uri("ms-appx:///Images/BlackKnight.png");
                    case ChessType.RedRook:
                        return new Uri("ms-appx:///Images/RedRook.png");
                    case ChessType.BlackRook:
                        return new Uri("ms-appx:///Images/BlackRook.png");
                    case ChessType.RedElephant:
                        return new Uri("ms-appx:///Images/RedElephant.png");
                    case ChessType.BlackElephant:
                        return new Uri("ms-appx:///Images/BlackElephant.png");
                    case ChessType.RedMandarins:
                        return new Uri("ms-appx:///Images/RedMandarins.png");
                    case ChessType.BlackMandarins:
                        return new Uri("ms-appx:///Images/BlackMandarins.png");
                    case ChessType.RedKing:
                        return new Uri("ms-appx:///Images/RedKing.png");
                    case ChessType.BlackKing:
                        return new Uri("ms-appx:///Images/BlackKing.png");
                    default:
                        return null;
                }
            }
        } 

        public override string ToString() => $"{this.Type} {this.Count}";

        //@Notify 
        /// <summary> Multicast event for property change notifications.</summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName"> Name of the property used to notify listeners.</param>
        private void OnPropertyChanged(string propertyName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}