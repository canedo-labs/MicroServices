using CanedoLab.MS.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanedoLab.MS.Catalog.API.Models
{
    public interface ICatalogItemRepository : IRepository<CatalogItem>
    {
        Task<IEnumerable<CatalogItem>> GetAll();
        Task<CatalogItem> GetById(Guid id);

        void Add(CatalogItem catalogItem);
        void Update(CatalogItem catalogItem);
    }
}
