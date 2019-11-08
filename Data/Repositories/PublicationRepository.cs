using BlogHost.Data.Interfaces;
using BlogHost.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogHost.Data.Repositories
{
    public class PublicationRepository : IPublication
    {
        private readonly AppDBContext _appDbContext;

        public PublicationRepository(AppDBContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public void AddPublicationDB(Publication publ)
        {
            _appDbContext.Publications.Add(publ);
            _appDbContext.SaveChanges();
            int a = _appDbContext.Publications.Count();
        }

        public IEnumerable<Publication> AllPost()
        {
            return _appDbContext.Publications.ToList();
        }
    }
}
