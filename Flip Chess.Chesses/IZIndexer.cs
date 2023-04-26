namespace Flip_Chess.Chesses
{
    public interface IZIndexer
    {
        int Step { get; }
        bool IsRed { get; }
        bool IsBlack { get; }

        int ZIndex { get; set; }
        ChessType[,,] Collection { get; }
    }
}