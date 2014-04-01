using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SignageRepository.Database;
using SignageRepository.Models.Catalogs;

namespace SignageRepository.Repository
{
    public static class CatSharedRepository
    {
        public static async Task<List<ComboBoxModel>> GetScreenSize(SignageDigitalEntities db)
        {
            var lstData = await db.CatScreenSize.Where(e => e.IsObsolete == false)
                .Select(e => new                 {
                    Id = e.CatScreenSizeId,
                    e.SizeX,
                    e.SizeY
                }).ToListAsync();

            return lstData.Select(e => new ComboBoxModel
                    {
                        Id =  e.Id,
                        Value = new {Name = String.Format("{0} x {1}", e.SizeX, e.SizeY), e.SizeX, e.SizeY}
                    }).ToList();
        }
    }
}
