namespace VideoStore.Console
{
    internal interface IVideoStore
    {
        string Statement(Invoice invoice, IReadOnlyDictionary<string, Play> plays);
    }
}
