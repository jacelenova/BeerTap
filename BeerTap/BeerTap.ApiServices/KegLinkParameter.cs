using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTap.ApiServices
{
    public class KegLinkParameter
    {
        public KegLinkParameter(int officeId)
        {
            OfficeId = officeId;
        }

        public int OfficeId { get; set; }
    }
}
