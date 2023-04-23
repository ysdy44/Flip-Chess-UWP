﻿namespace Flip_Chess.Chesses.Extensions
{
    partial class ChessExtensions
    {
        public static ChessType ToBlack(this ChessCount count) => (ChessType)(1 + (int)count / 5);
        public static bool IsBlack(this ChessType type)
        {
            int c = (int)type;
            return c > 1 && c % 2 != 0;
        }

        public static ClickAction BlackClick(this ChessType previous, ChessType next)
        {
            switch (next)
            {
                case ChessType.Unkonw:
                    return ClickAction.Flip;

                case ChessType.Deaded:
                    return ClickAction.Capture;

                case ChessType.BlackSoldier:
                case ChessType.BlackCannons:
                case ChessType.BlackKnight:
                case ChessType.BlackRook:
                case ChessType.BlackElephant:
                case ChessType.BlackMandarins:
                case ChessType.BlackKing:
                    return ClickAction.Select;

                case ChessType.RedSoldier:
                    switch (previous)
                    {
                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return ClickAction.Select;

                        case ChessType.BlackSoldier:
                        case ChessType.BlackCannons:
                        case ChessType.BlackKnight:
                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                            return ClickAction.Capture;
                        default:
                            return ClickAction.None;
                    }

                case ChessType.RedCannons:
                    switch (previous)
                    {
                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return ClickAction.Select;

                        case ChessType.BlackCannons:
                        case ChessType.BlackKnight:
                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return ClickAction.Capture;
                        default:
                            return ClickAction.None;
                    }

                case ChessType.RedKnight:
                    switch (previous)
                    {
                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return ClickAction.Select;

                        case ChessType.BlackKnight:
                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return ClickAction.Capture;
                        default:
                            return ClickAction.None;
                    }

                case ChessType.RedRook:
                    switch (previous)
                    {
                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return ClickAction.Select;

                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return ClickAction.Capture;
                        default:
                            return ClickAction.None;
                    }

                case ChessType.RedElephant:
                    switch (previous)
                    {
                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return ClickAction.Select;

                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return ClickAction.Capture;
                        default:
                            return ClickAction.None;
                    }

                case ChessType.RedMandarins:
                    switch (previous)
                    {
                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return ClickAction.Select;

                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return ClickAction.Capture;
                        default:
                            return ClickAction.None;
                    }

                case ChessType.RedKing:
                    switch (previous)
                    {
                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return ClickAction.Select;

                        case ChessType.BlackSoldier:
                        case ChessType.BlackKing:
                            return ClickAction.Capture;

                        default:
                            return ClickAction.None;
                    }

                default: return ClickAction.None;
            }
        }
    }
}