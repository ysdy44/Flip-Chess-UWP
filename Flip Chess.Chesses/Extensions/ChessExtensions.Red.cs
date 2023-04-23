namespace Flip_Chess.Chesses.Extensions
{
	partial class ChessExtensions
	{
		public static ChessType ToRed(this ChessCount count) => (ChessType)(0 + (int)count / 5);
		public static bool IsRed(this ChessType type)
		{
			int c = (int)type;
			return c > 1 && c % 2 == 0;
		}

		public static ClickAction RedClick(this ChessType previous, ChessType next)
		{
			switch (next)
			{
				case ChessType.Unkonw:
					return ClickAction.Flip;

				case ChessType.Deaded:
					return ClickAction.Capture;

				case ChessType.RedSoldier:
				case ChessType.RedCannons:
				case ChessType.RedKnight:
				case ChessType.RedRook:
				case ChessType.RedElephant:
				case ChessType.RedMandarins:
				case ChessType.RedKing:
					return ClickAction.Select;

				case ChessType.BlackSoldier:
					switch (previous)
					{
						case ChessType.BlackSoldier:
						case ChessType.BlackCannons:
						case ChessType.BlackKnight:
						case ChessType.BlackRook:
						case ChessType.BlackElephant:
						case ChessType.BlackMandarins:
						case ChessType.BlackKing:
							return ClickAction.Select;

						case ChessType.RedSoldier:
						case ChessType.RedCannons:
						case ChessType.RedKnight:
						case ChessType.RedRook:
						case ChessType.RedElephant:
						case ChessType.RedMandarins:
							return ClickAction.Capture;
						default:
							return ClickAction.None;
					}

				case ChessType.BlackCannons:
					switch (previous)
					{
						case ChessType.BlackSoldier:
						case ChessType.BlackCannons:
						case ChessType.BlackKnight:
						case ChessType.BlackRook:
						case ChessType.BlackElephant:
						case ChessType.BlackMandarins:
						case ChessType.BlackKing:
							return ClickAction.Select;

						case ChessType.RedCannons:
						case ChessType.RedKnight:
						case ChessType.RedRook:
						case ChessType.RedElephant:
						case ChessType.RedMandarins:
						case ChessType.RedKing:
							return ClickAction.Capture;
						default:
							return ClickAction.None;
					}

				case ChessType.BlackKnight:
					switch (previous)
					{
						case ChessType.BlackSoldier:
						case ChessType.BlackCannons:
						case ChessType.BlackKnight:
						case ChessType.BlackRook:
						case ChessType.BlackElephant:
						case ChessType.BlackMandarins:
						case ChessType.BlackKing:
							return ClickAction.Select;

						case ChessType.RedKnight:
						case ChessType.RedRook:
						case ChessType.RedElephant:
						case ChessType.RedMandarins:
						case ChessType.RedKing:
							return ClickAction.Capture;
						default:
							return ClickAction.None;
					}

				case ChessType.BlackRook:
					switch (previous)
					{
						case ChessType.BlackSoldier:
						case ChessType.BlackCannons:
						case ChessType.BlackKnight:
						case ChessType.BlackRook:
						case ChessType.BlackElephant:
						case ChessType.BlackMandarins:
						case ChessType.BlackKing:
							return ClickAction.Select;

						case ChessType.RedRook:
						case ChessType.RedElephant:
						case ChessType.RedMandarins:
						case ChessType.RedKing:
							return ClickAction.Capture;
						default:
							return ClickAction.None;
					}

				case ChessType.BlackElephant:
					switch (previous)
					{
						case ChessType.BlackSoldier:
						case ChessType.BlackCannons:
						case ChessType.BlackKnight:
						case ChessType.BlackRook:
						case ChessType.BlackElephant:
						case ChessType.BlackMandarins:
						case ChessType.BlackKing:
							return ClickAction.Select;

						case ChessType.RedElephant:
						case ChessType.RedMandarins:
						case ChessType.RedKing:
							return ClickAction.Capture;
						default:
							return ClickAction.None;
					}

				case ChessType.BlackMandarins:
					switch (previous)
					{
						case ChessType.BlackSoldier:
						case ChessType.BlackCannons:
						case ChessType.BlackKnight:
						case ChessType.BlackRook:
						case ChessType.BlackElephant:
						case ChessType.BlackMandarins:
						case ChessType.BlackKing:
							return ClickAction.Select;

						case ChessType.RedMandarins:
						case ChessType.RedKing:
							return ClickAction.Capture;
						default:
							return ClickAction.None;
					}

				case ChessType.BlackKing:
					switch (previous)
					{
						case ChessType.BlackSoldier:
						case ChessType.BlackCannons:
						case ChessType.BlackKnight:
						case ChessType.BlackRook:
						case ChessType.BlackElephant:
						case ChessType.BlackMandarins:
						case ChessType.BlackKing:
							return ClickAction.Select;

						case ChessType.RedSoldier:
						case ChessType.RedKing:
							return ClickAction.Capture;

						default:
							return ClickAction.None;
					}

				default: return ClickAction.None;
			}
		}
	}
}