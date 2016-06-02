using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BeerTap.ApiServices.Security;
using BeerTap.Model;
using Castle.Windsor.Installer;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.Common.Mapping;
using IQ.Platform.Framework.WebApi;
using IQ.Platform.Framework.WebApi.Services.Security;

namespace BeerTap.ApiServices
{
    public class KegApiService : IKegApiService
    {
        readonly IApiUserProvider<BeerTapApiUser> _userProvider;
        private readonly IMapper<Domain.Keg, Model.Keg> _toResourceMapper;
        private readonly IMapper<Model.Keg, Domain.Keg> _toTransportMapper;
        private readonly Domain.Service.IKegService _kegService;

        public KegApiService(
            IApiUserProvider<BeerTapApiUser> userProvider,
            IMapper<Domain.Keg, Model.Keg> toResourceMapper,
            IMapper<Model.Keg, Domain.Keg> toTransportMapper,
            Domain.Service.IKegService kegService
            )
        {
            if (kegService == null)
                throw new ArgumentNullException("kegService");
            if (toResourceMapper == null)
                throw new ArgumentNullException("toResourceMapper");
            if (toTransportMapper == null)
                throw new ArgumentNullException("toTransportMapper");
            if (userProvider == null)
                throw new ArgumentNullException("userProvider");

            _kegService = kegService;
            _toResourceMapper = toResourceMapper;
            _toTransportMapper = toTransportMapper;
            _userProvider = userProvider;
        }

        public Task<Keg> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
        {
            var officeId = GetOfficeId(context);
            var keg = _kegService.GetKeg(id, officeId);

            return Task.FromResult(_toResourceMapper.Map(keg));
        }

        public Task<IEnumerable<Keg>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
        {
            //var kegs = _kegService.GetAll().AsEnumerable();
            var officeId = GetOfficeId(context);
            var kegs = _kegService.GetKegsByOffice(officeId);

            return Task.FromResult(kegs.AsEnumerable().Select(k => _toResourceMapper.Map(k)));
        }

        public Task<ResourceCreationResult<Keg, int>> CreateAsync(Keg resource, IRequestContext context, CancellationToken cancellation)
        {
            var officeId = GetOfficeId(context);
            var keg = _kegService.Add(resource.Name, officeId);

            return Task.FromResult(new ResourceCreationResult<Keg, int>(_toResourceMapper.Map(keg)));
        }

        public Task<Keg> UpdateAsync(Keg resource, IRequestContext context, CancellationToken cancellation)
        {
            var keg = _kegService.Update(resource.Id, resource.Name, resource.Content);

            return Task.FromResult(_toResourceMapper.Map(keg));
        }

        public Task DeleteAsync(ResourceOrIdentifier<Keg, int> input, IRequestContext context, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        private int GetOfficeId(IRequestContext context)
        {
            var officeId = context.UriParameters.GetByName<int>("OfficeId").EnsureValue();
            var linkParameter = new KegLinkParameter(officeId);
            context.LinkParameters.Set(linkParameter);

            return officeId;
        }
    }
}
