using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using BeerTap.Domain;
using BeerTap.Model;
using IQ.Platform.Framework.Common.Mapping;
using Keg = BeerTap.Model.Keg;

namespace BeerTap.ApiServices
{
    public class KegMapper :
        IMapper<Domain.Keg, Model.Keg>,
        IMapper<Model.Keg, Domain.Keg>
    {
        public Keg Map(Domain.Keg source)
        {
            if (source != null)
            {
                var keg = new Model.Keg()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Content = source.Content,
                    OfficeId = source.OfficeId
                };

                keg.KegState = GetKegState(keg);

                return keg;
            }

            return null;
        }

        public Domain.Keg Map(Keg source)
        {
            if (source != null)
            {
                var keg = new Domain.Keg(source.Name)
                {
                    Id = source.Id,
                    Name = source.Name,
                    Content = source.Content,
                    OfficeId = source.OfficeId
                };

                return keg;
            }

            return null;
        }

        private KegState GetKegState(Keg source)
        {
            if (Keg.MaxContent == source.Content) return KegState.New;
            if ((double)source.Content/Keg.MaxContent >= 0.25) return KegState.GoinDown;
            if ((double)source.Content/Keg.MaxContent > 0) return KegState.AlmostEmpty;
            return KegState.SheIsDryMate;
        }
    }
}
