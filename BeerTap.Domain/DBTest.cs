using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTap.Domain
{
    public static class DBTest
    {
        public static List<Office> OficeList()
        {
            return new BeerTapContext().Offices.ToList();
        }
    }
}
