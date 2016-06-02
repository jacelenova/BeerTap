using System;
using System.Threading;
using System.Threading.Tasks;
using BeerTap.ApiServices.Security;
using BeerTap.Domain.Service;
using BeerTap.Model;
using IQ.Platform.Framework.WebApi;
using IQ.Platform.Framework.WebApi.Services.Security;
using IQ.Platform.Framework.Common;

namespace BeerTap.ApiServices
{
    public class GetBeerApiService : IGetBeerApiService
    {
        readonly IApiUserProvider<BeerTapApiUser> _userProvider;
        private readonly Domain.Service.IKegService _kegService;

        public GetBeerApiService(
            IApiUserProvider<BeerTapApiUser> userProvider,
            IKegService kegService
            )
        {
            if (userProvider == null)
                throw  new ArgumentNullException("userProvider");
            if (kegService == null)
                throw new ArgumentNullException("kegService");

            _kegService = kegService;
            _userProvider = userProvider;
        }

        public Task<GetBeer> UpdateAsync(GetBeer resource, IRequestContext context, CancellationToken cancellation)
        {
            var officeId = GetOfficeId(context);
            var kegId = GetKegId(context);

            var getBeer = _kegService.GetBeer(kegId, officeId, resource.Size);
            if (getBeer == -1) context.ThrowNotFoundHttpResponseException<GetBeer, int>(resource.Id);

            resource.OfficeId = officeId;
            resource.Size = getBeer;

            return Task.FromResult(resource);
        }

        private int GetOfficeId(IRequestContext context)
        {
            var officeId = context.UriParameters.GetByName<int>("OfficeId").EnsureValue();
            var linkParameter = new OfficeLinkParameter(officeId);
            context.LinkParameters.Set(linkParameter);

            return officeId;
        }

        private int GetKegId(IRequestContext context)
        {
            return context.UriParameters.GetByName<int>("Id").EnsureValue();
        }
    }
}