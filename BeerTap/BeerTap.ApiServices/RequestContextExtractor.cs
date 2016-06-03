using System;
using System.Net;
using BeerTap.ApiServices.Security;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi;
using IQ.Platform.Framework.WebApi.AspNet;

namespace BeerTap.ApiServices
{
    public interface IExtractDataFromARequestContext
    {
        int ExtractOfficeId<TResource>(IRequestContext context) where TResource : class;
       
    }

    public class RequestContextExtractor : IExtractDataFromARequestContext
    {
        private readonly IGetDataFromHttpRequest<BeerTapApiUser> _getApiUserFromHttpRequest;

        public RequestContextExtractor(IGetDataFromHttpRequest<BeerTapApiUser> getApiUserFromHttpRequest)
        {
            if (getApiUserFromHttpRequest == null) throw new ArgumentNullException(nameof(getApiUserFromHttpRequest));
            _getApiUserFromHttpRequest = getApiUserFromHttpRequest;
        }

        public int ExtractOfficeId<TResource>(IRequestContext context) where TResource : class
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var option = context.UriParameters.GetByName<int>("officeId");
            var officeId = option.EnsureValue(() => context.CreateHttpResponseException<TResource>("Cannot find office identifier in the uri", HttpStatusCode.BadRequest));
            context.LinkParameters.Set(new LinksParametersSource(officeId));

            return officeId;
        }
        
    }
}
