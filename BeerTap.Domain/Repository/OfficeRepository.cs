using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTap.Domain.Repository
{
    public class OfficeRepository : GenericRepository<BeerTapContext, Office>, IOfficeRepository
    {
        public Office GetById(int id)
        {
            var query = GetAll().SingleOrDefault(t => t.Id == id);

            return query;
        }

        //internal object GetAll<T>()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
