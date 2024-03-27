namespace Mediporta.DTOS.Interfaces
{
    public interface ITagDTO
    {
        double Count { get; init; }
        bool HasSynonyms { get; init; }
        bool IsModeratorOnly { get; init; }
        bool IsRequired { get; init; }
        string Name { get; init; }

        void Deconstruct(out bool HasSynonyms, out bool IsModeratorOnly, out bool IsRequired, out double Count, out string Name);
        bool Equals(object? obj);
        bool Equals(TagDTO? other);
        int GetHashCode();
        string ToString();
    }
}