using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerTap.Model;
using IQ.Platform.Framework.WebApi;

namespace BeerTap.ApiServices
{
    public interface IReplaceKegApiService :
        IUpdateAResourceAsync<ReplaceKeg, int>
    {
    }
}
