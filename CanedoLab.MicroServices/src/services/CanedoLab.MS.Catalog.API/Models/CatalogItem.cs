using CanedoLab.MS.Core.DomainObjects;
using System;

namespace CanedoLab.MS.Catalog.API.Models
{
    public class CatalogItem : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool OnSale { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Image { get; set; }
        public int AvailableStock { get; set; }
    }
}
