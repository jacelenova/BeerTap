using System.Linq;
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

        public Model.Keg MapKegs(Domain.Keg source)
        {
            var resource = new Model.Keg()
            {
                Id = source.Id,
                Content = source.Content,
                //KegState = KegState.GoinDown,
                Name = source.Name,
                OfficeId = source.OfficeId
            };
            resource.KegState = GetKegState(resource);

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

        public Domain.Keg MapKegs(Model.Keg source)
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

        private KegState GetKegState(Model.Keg source)
        {
            if (Model.Keg.MaxContent == source.Content) return KegState.New;
            if ((double)source.Content / Model.Keg.MaxContent >= 0.25) return KegState.GoinDown;
            if ((double)source.Content / Model.Keg.MaxContent > 0) return KegState.AlmostEmpty;
            return KegState.SheIsDryMate;
        }
    }
}
