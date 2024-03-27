namespace Mediporta.DTOS
{
    public record TagDTO(bool HasSynonyms, bool IsModeratorOnly, bool IsRequired, int Count, string Name);

}
