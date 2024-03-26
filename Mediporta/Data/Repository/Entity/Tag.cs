using System.ComponentModel.DataAnnotations.Schema;

namespace Mediporta.Data.Repository.Entity
{
    public class Tag : IEntity
    {
        public int TagId { get; set; }
        public bool HasSynonyms { get; set; }
        public bool IsModeratorOnly { get; set; }
        public bool IsRequired { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
    }
}