using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BeerTap.Model;
using IQ.Platform.Framework.WebApi.CacheControl;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace BeerTap.WebApi.Hypermedia
{
    public class GetBeerSpec : SingleStateResourceSpec<GetBeer, int>
    {
        public static ResourceUriTemplate UriGetBeer = ResourceUriTemplate.Create("Offices({OfficeId})/Kegs({Id})/GetBeer");

        public override IResourceStateSpec<GetBeer, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<GetBeer, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Keg, KegSpec.UriKegAtOffice, resource => resource.OfficeId, resource => resource.Id)
                        },
                        Operations =
                        {
                            Post = ServiceOperations.Update
                        }
                    };
            }
        }

        protected override IEnumerable<ResourceLinkTemplate<GetBeer>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriGetBeer, c => c.OfficeId, c => c.Id);
        }

        public override ResourceCacheControlSpec Cache
        {
            get { return ResourceCacheControl.WithCache(0); }
        }
    }
}