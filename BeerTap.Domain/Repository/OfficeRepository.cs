using System.Data.Entity;
using System.Linq;

namespace BeerTap.Domain.Repository
{
    public class OfficeRepository : GenericRepository<BeerTapContext, Office>, IOfficeRepository
    {
        public Office GetById(int id)
        {
            //var query = GetAll().SingleOrDefault(t => t.Id == id);
            var query = GetAll().Include(k => k.Kegs).SingleOrDefault(o => o.Id == id);

            return query;
        }
    }
}
