using CanedoLab.MS.Catalog.API.Models;
using CanedoLab.MS.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanedoLab.MS.Catalog.API.Controllers
{
    [Authorize]
    public class CatalogController : ApiController
    {
        private readonly ICatalogItemRepository _catalogItemRepository;

        public CatalogController(ICatalogItemRepository catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository;
        }

        [AllowAnonymous]
        [HttpGet("catalog/catalog-item")]
        public async Task<IEnumerable<CatalogItem>> Index() 
        {
            return await _catalogItemRepository.GetAll();
        }

        [ClaimAuthorize("Catalog", "Read")]
        [HttpGet("catalog/catalog-item/{catalogId}")]
        public async Task<CatalogItem> CatalogItem(Guid catalogId) 
        {
            return await _catalogItemRepository.GetById(catalogId);
        }
    }
}
