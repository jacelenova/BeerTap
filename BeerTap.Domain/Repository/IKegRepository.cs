using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTap.Domain.Repository
{
    public interface IKegRepository : IGenericRepository<Keg>
    {
        Keg GetById(int id);
    }
}
