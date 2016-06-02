using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTap.Model
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStatefulKeg
    {
        /// <summary>
        /// 
        /// </summary>
        KegState KegState { get; }
    }
}
