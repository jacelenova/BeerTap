﻿using System;
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
    public class ReplaceKegSpec : SingleStateResourceSpec<ReplaceKeg, int>
    {
        public static ResourceUriTemplate UriReplaceBeer = ResourceUriTemplate.Create("Offices({OfficeId})/Kegs({Id})/ReplaceKeg");

        public override IResourceStateSpec<ReplaceKeg, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<ReplaceKeg, int>()
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Keg, KegSpec.UriKegAtOffice, resource => resource.OfficeId, resource => resource.Id)
                        },
                        Operations =
                        {
                            Get = ServiceOperations.Get,
                            InitialPost = ServiceOperations.Create,
                            Post = ServiceOperations.Update,
                            Delete = ServiceOperations.Delete
                        }
                    };
            }
        }

        protected override IEnumerable<ResourceLinkTemplate<ReplaceKeg>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriReplaceBeer, c => c.OfficeId, c => c.Id);
        }

        public override ResourceCacheControlSpec Cache
        {
            get { return ResourceCacheControl.WithCache(0); }
        }
    }
}