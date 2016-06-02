using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTap.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Keg : IStatefulResource<KegState>, IIdentifiable<int>, IStatefulKeg
    {
        /// <summary>
        /// 
        /// </summary>
        public Keg() : this("")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Keg(string name)
        {
            this.Content = MaxContent;
            this.Name = Name;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OfficeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public KegState KegState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public const int MaxContent = 10000;
    }
}
