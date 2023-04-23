namespace Flip_Chess.Chesses.Extensions
{
    public static partial class ChessExtensions
    {
        public static int GetLevelAbs(this ChessType type) => (int)type / 2;
        public static int GetLevel(this ChessType type)
        {
            int c = (int)type;
            return c % 2 == 0 ? c / 2 : -c / 2;
        }
    }
}