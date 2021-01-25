namespace MLinter
{
    public interface IContentFilter
    {
        bool Matches(string[] data);
    }
}
