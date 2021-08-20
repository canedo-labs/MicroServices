using CanedoLab.MS.Catalog.API.Models;
using CanedoLab.MS.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanedoLab.MS.Catalog.API.Data.Repositories
{
    public class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly CatalogContext _context;

        public CatalogItemRepository(CatalogContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<CatalogItem>> GetAll()
        {
            return await _context.CatalogItem.AsNoTracking().ToListAsync();
        }

        public async Task<CatalogItem> GetById(Guid id)
        {
            return await _context.CatalogItem.FindAsync(id);
        }

        public void Add(CatalogItem catalogItem)
        {
            _context.CatalogItem.Add(catalogItem);
        }

        public void Update(CatalogItem catalogItem)
        {
            _context.CatalogItem.Update(catalogItem);
        }


        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
