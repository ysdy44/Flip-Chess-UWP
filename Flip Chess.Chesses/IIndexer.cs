namespace Flip_Chess.Chesses
{
    public interface IIndexer
    {
        int Step { get; }
        bool IsRed { get; }
        bool IsBlack { get; }

        int Index { get; set; }
        ChessType[,,] Collection { get; }
    }
}