using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTap.Domain.Repository
{
    public class KegRepository : GenericRepository<BeerTapContext, Keg>, IKegRepository
    {
        public Keg GetById(int id)
        {
            var query = GetAll().SingleOrDefault(k => k.Id == id);

            return query;
            //throw new NotImplementedException();
        }

        public IQueryable<Keg> GetKegsByOffice(int officeId)
        {
            var query = FindBy(c => c.OfficeId == officeId);

            return query;
        }
    }
}
