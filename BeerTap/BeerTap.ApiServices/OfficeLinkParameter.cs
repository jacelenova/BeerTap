using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTap.ApiServices
{
    public class OfficeLinkParameter
    {
        public OfficeLinkParameter(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}
