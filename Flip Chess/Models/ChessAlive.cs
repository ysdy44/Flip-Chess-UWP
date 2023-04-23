using Flip_Chess.Chesses;
using System;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace Flip_Chess.Models
{
    public sealed class ChessAlive : IChess, INotifyPropertyChanged
    {
        public Visibility Visibility
        {
            get => this.visibility;
            set
            {
                if (this.visibility == value) return;
                this.visibility = value;
                this.OnPropertyChanged(nameof(Visibility)); // Notify 
            }
        }
        private Visibility visibility;

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

        public Uri ImageSource
        {
            get
            {
                switch (this.Type)
                {
                    case ChessType.Unkonw:
                        return new Uri("ms-appx:///Images/Item.x2.png");
                    case ChessType.Deaded:
                        return null;
                    case ChessType.RedSoldier:
                        return new Uri("ms-appx:///Images/RedSoldier.x2.png");
                    case ChessType.BlackSoldier:
                        return new Uri("ms-appx:///Images/BlackSoldier.x2.png");
                    case ChessType.RedCannons:
                        return new Uri("ms-appx:///Images/RedCannons.x2.png");
                    case ChessType.BlackCannons:
                        return new Uri("ms-appx:///Images/BlackCannons.x2.png");
                    case ChessType.RedKnight:
                        return new Uri("ms-appx:///Images/RedKnight.x2.png");
                    case ChessType.BlackKnight:
                        return new Uri("ms-appx:///Images/BlackKnight.x2.png");
                    case ChessType.RedRook:
                        return new Uri("ms-appx:///Images/RedRook.x2.png");
                    case ChessType.BlackRook:
                        return new Uri("ms-appx:///Images/BlackRook.x2.png");
                    case ChessType.RedElephant:
                        return new Uri("ms-appx:///Images/RedElephant.x2.png");
                    case ChessType.BlackElephant:
                        return new Uri("ms-appx:///Images/BlackElephant.x2.png");
                    case ChessType.RedMandarins:
                        return new Uri("ms-appx:///Images/RedMandarins.x2.png");
                    case ChessType.BlackMandarins:
                        return new Uri("ms-appx:///Images/BlackMandarins.x2.png");
                    case ChessType.RedKing:
                        return new Uri("ms-appx:///Images/RedKing.x2.png");
                    case ChessType.BlackKing:
                        return new Uri("ms-appx:///Images/BlackKing.x2.png");
                    default:
                        return null;
                }
            }
        }

        public override string ToString() => this.Type.ToString();

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