namespace Flip_Chess.Chesses
{
    public interface IIndexer
    {
        int Step { get; }
        bool IsRed { get; }
        bool IsBlack { get; }

        int ZIndex { get; set; }
        ChessType[,,] Collection { get; }
    }
}