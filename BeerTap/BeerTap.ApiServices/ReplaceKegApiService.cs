using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BeerTap.ApiServices.Security;
using BeerTap.Domain.Service;
using BeerTap.Model;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.Common.Mapping;
using IQ.Platform.Framework.WebApi;
using IQ.Platform.Framework.WebApi.Services.Security;

namespace BeerTap.ApiServices
{
    public class ReplaceKegApiService : IReplaceKegApiService
    {
        readonly IApiUserProvider<BeerTapApiUser> _userProvider;
        private readonly Domain.Service.IKegService _kegService;
        private readonly IMapper<Domain.Keg, Model.Keg> _toResourceMapper;

        public ReplaceKegApiService(
            IApiUserProvider<BeerTapApiUser> userProvider,
            IMapper<Domain.Keg, Model.Keg> toResourceMapper,
            IKegService kegService
            )
        {
            if (userProvider == null)
                throw new ArgumentNullException("userProvider");
            if (kegService == null)
                throw new ArgumentNullException("kegService");
            if (toResourceMapper == null)
                throw new ArgumentNullException("toResourceMapper");

            _kegService = kegService;
            _userProvider = userProvider;
            _toResourceMapper = toResourceMapper;
        }

        public Task<ReplaceKeg> UpdateAsync(ReplaceKeg resource, IRequestContext context, CancellationToken cancellation)
        {
            var officeId = GetOfficeId(context);
            var kegId = GetKegId(context);
            var keg = _kegService.GetKeg(kegId, officeId);
            if (keg == null) context.ThrowNotFoundHttpResponseException<ReplaceKeg, int>(resource.Id);

            var kegx = _toResourceMapper.Map(keg);
            if (kegx.KegState != KegState.SheIsDryMate && kegx.KegState != KegState.AlmostEmpty)
                context.ThrowNotFoundHttpResponseException<ReplaceKeg, int>(resource.Id);

            var replaceKeg = _kegService.ReplaceKeg(resource.OfficeId, resource.Id);

            resource.Id = kegId;
            resource.OfficeId = officeId;

            return Task.FromResult(resource);
            //throw new NotImplementedException();
        }

        private int GetOfficeId(IRequestContext context)
        {
            return context.UriParameters.GetByName<int>("OfficeId").EnsureValue();
        }

        private int GetKegId(IRequestContext context)
        {
            return context.UriParameters.GetByName<int>("Id").EnsureValue();
        }
    }
}