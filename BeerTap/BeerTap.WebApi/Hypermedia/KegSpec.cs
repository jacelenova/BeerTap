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
    public class KegSpec : ResourceSpec<Keg, KegState, int>
    {
        public static ResourceUriTemplate UriKegAtOffice = ResourceUriTemplate.Create("Offices({OfficeId})/Kegs({Id})");

        public override string EntrypointRelation
        {
            get { return LinkRelations.Keg; }
        }

        protected override IEnumerable<ResourceLinkTemplate<Keg>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriKegAtOffice, c => c.OfficeId, c => c.Id);
        }

        protected override IEnumerable<IResourceStateSpec<Keg, KegState, int>> GetStateSpecs()
        {
            yield return new ResourceStateSpec<Keg, KegState, int>(KegState.New)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.GetBeer, GetBeerSpec.UriGetBeer, c => c.OfficeId, c => c.Id),
                    //CreateLinkTemplate("Test", KegSpec.UriGetTaps, c => c.OfficeId)
                },
                Operations = new StateSpecOperationsSource<Keg, int>()
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Put = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete
                }
            };
            yield return new ResourceStateSpec<Keg, KegState, int>(KegState.GoinDown)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.GetBeer, GetBeerSpec.UriGetBeer, c => c.OfficeId, c => c.Id)
                    //CreateLinkTemplate(LinkRelations.Keg, KegSpec.UriKegAtOffice, c => c.OfficeId, c => c.Id)
                },
                Operations = new StateSpecOperationsSource<Keg, int>()
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Put = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete
                }
            };
            yield return new ResourceStateSpec<Keg, KegState, int>(KegState.AlmostEmpty)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.ReplaceKeg, ReplaceKegSpec.UriReplaceBeer, c => c.OfficeId, c => c.Id)
                    //CreateLinkTemplate(LinkRelations.Keg, KegSpec.UriKegAtOffice, c => c.OfficeId, c => c.Id),
                },
                Operations = new StateSpecOperationsSource<Keg, int>()
                {
                    Post = ServiceOperations.Update,
                }
            };
            yield return new ResourceStateSpec<Keg, KegState, int>(KegState.SheIsDryMate)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.ReplaceKeg, ReplaceKegSpec.UriReplaceBeer, c => c.OfficeId, c => c.Id)
                },
                Operations = new StateSpecOperationsSource<Keg, int>()
                {
                    Post = ServiceOperations.Update,
                }
            };
        }

        public override ResourceCacheControlSpec Cache
        {
            get { return ResourceCacheControl.WithCache(0); }
        }

    }
}