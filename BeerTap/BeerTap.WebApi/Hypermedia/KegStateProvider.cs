using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using BeerTap.Model;

namespace BeerTap.WebApi.Hypermedia
{
    public class KegStateProvider : KegStateProvider<Keg>
    {
    }

    public abstract class KegStateProvider<TKegResource> : ResourceStateProviderBase<TKegResource, KegState>
    where TKegResource : IStatefulResource<KegState>, IStatefulKeg
    {
        public override KegState GetFor(TKegResource resource)
        {
            return resource.KegState;
        }
        protected override IDictionary<KegState, IEnumerable<KegState>> GetTransitions()
        {
            return new Dictionary<KegState, IEnumerable<KegState>>
            {
                // from, to
                {
                    KegState.New, new[]
                    {
                        KegState.GoinDown,
                        KegState.AlmostEmpty,
                        KegState.SheIsDryMate
                    }
                },
                {
                    KegState.GoinDown, new[]
                    {
                        KegState.AlmostEmpty, 
                        KegState.SheIsDryMate, 
                    }
                },
                {
                    KegState.AlmostEmpty, new[]
                    {
                        KegState.SheIsDryMate, 
                    }
                },
                {
                    KegState.SheIsDryMate, new[]
                    {
                        KegState.New
                    }
                }
            };
        }
        public override IEnumerable<KegState> All
        {
            get { return EnumEx.GetValuesFor<KegState>(); }
        }
    }
}