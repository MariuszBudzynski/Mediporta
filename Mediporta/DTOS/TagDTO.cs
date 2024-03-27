namespace Mediporta.DTOS
{
    public record TagDTO(bool HasSynonyms, bool IsModeratorOnly, bool IsRequired, double Count, string Name) : ITagDTO { }

}
