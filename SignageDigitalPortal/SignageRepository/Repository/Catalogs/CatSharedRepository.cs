using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SignageRepository.Database;
using SignageRepository.Models.Catalogs;

namespace SignageRepository.Repository.Catalogs
{
    public static class CatSharedRepository
    {
        public static async Task<List<ComboBoxModel>> GetScreenSizeAsync(SignageDigitalEntities db)
        {
            var lstData = await db.CatScreenSize.Where(e => e.IsObsolete == false)
                .Select(e => new
                {
                    Id = e.CatScreenSizeId,
                    e.SizeX,
                    e.SizeY
                }).ToListAsync();

            return lstData.Select(e => new ComboBoxModel
            {
                Id = e.Id,
                Value = new { Name = String.Format("{0} x {1}", e.SizeX, e.SizeY), e.SizeX, e.SizeY }
            }).ToList();
        }

        public static List<ComboBoxModel> GetScreenSize(SignageDigitalEntities db)
        {
            var lstData = db.CatScreenSize.Where(e => e.IsObsolete == false)
                .Select(e => new
                {
                    Id = e.CatScreenSizeId,
                    e.SizeX,
                    e.SizeY
                }).ToList();

            return lstData.Select(e => new ComboBoxModel
            {
                Id = e.Id,
                Value = new { Name = String.Format("{0} x {1}", e.SizeX, e.SizeY), e.SizeX, e.SizeY }
            }).ToList();
        }

        public static Dictionary<string, MimeListModel> GetMimeTypes()
        {
            using (var db = new SignageDigitalEntities())
            {
                return db.CatMime.Where(e => e.IsObsolete == false).
                    Select(e => new {e.Mime, e.CatMimeId, LstExt = e.CatMimeExtension.Select(i => i.Extension).ToList()}).
                    ToDictionary(e => e.Mime, e => new MimeListModel
                    {
                        CatMimeId = e.CatMimeId,
                        LstMimes = e.LstExt
                    });
            }
        }

        public static List<ComboBoxModel> GetLstMedias(SignageDigitalEntities db)
        {
            return db.Media.Select(e => new ComboBoxModel
            {
                IdLg = e.MediaId,
                Value = "( " + e.CatMime.Mime.ToUpper() + " ) " + e.LogicName
            }).ToList();
        }

        public static object GetChannels(SignageDigitalEntities db)
        {
            return db.Channel.Select(e => new ComboBoxModel
            {
                IdLg = e.ChannelId,
                Value = e.Name
            }).ToList();
        }
    }
}
