using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerTap.Domain;
using BeerTap.Model;
using IQ.Foundation;
using IQ.Platform.Framework.Common.Mapping;
using Office = BeerTap.Model.Office;

namespace BeerTap.ApiServices
{
    public class OfficeMapper :
        IMapper<Domain.Office, Model.Office>,
        IMapper<Model.Office, Domain.Office>
    {
        public Office Map(Domain.Office source)
        {
            //throw new NotImplementedException();
            if (source != null)
            {
                var resource = new Office(source.Name)
                {
                    Id = source.Id,
                };
                resource.Kegs.AddRange(source.Kegs.Select(MapKegs));
                return resource;
            }

            return null;
        }

        static Model.Keg MapKegs(Domain.Keg source)
        {
            var resource = new Model.Keg()
            {
                Id = source.Id,
                Content = source.Content,
                KegState = KegState.GoinDown,
                Name = source.Name,
                OfficeId = source.OfficeId
            };

            return resource;
        }

        public Domain.Office Map(Office source)
        {
            //throw new NotImplementedException();
            var resource = new Domain.Office(source.Name)
            {
                Id = source.Id,
                Name = source.Name
            };
            resource.Kegs.AddRange(source.Kegs.Select(MapKegs));

            return resource;
        }

        static Domain.Keg MapKegs(Model.Keg source)
        {
            var resource = new Domain.Keg(source.Name)
            {
                Id = source.Id,
                Content = source.Content,
                Name = source.Name,
                OfficeId = source.OfficeId,
            };

            return resource;
        }
    }
}
