using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeerTap.ApiServices.Security;
using IQ.Platform.Framework.Common.Mapping;
using IQ.Platform.Framework.WebApi;
using IQ.Platform.Framework.WebApi.Services.Security;
using Office = BeerTap.Model.Office;

namespace BeerTap.ApiServices
{
    public class OfficeApiService : IOfficeApiService
    {
        readonly IApiUserProvider<BeerTapApiUser> _userProvider;
        private readonly IMapper<Domain.Office, Model.Office> _toResourceMapper;
        private readonly IMapper<Model.Office, Domain.Office> _toTransportMapper;
        private readonly Domain.Service.IOfficeService _officeService;

        public OfficeApiService(
            IApiUserProvider<BeerTapApiUser> userProvider,
            IMapper<Domain.Office, Model.Office> toResourceMapper,
            IMapper<Model.Office, Domain.Office> toTransportMapper,
            Domain.Service.IOfficeService officeService
            )
        {
            if (officeService == null)
                throw new ArgumentNullException("officeService");
            if (toResourceMapper == null)
                throw new ArgumentNullException("toResourceMapper");
            if (toTransportMapper == null)
                throw new ArgumentNullException("toTransportMapper");
            if (userProvider == null)
                throw new ArgumentNullException("userProvider");

            _officeService = officeService;
            _toResourceMapper = toResourceMapper;
            _toTransportMapper = toTransportMapper;
            _userProvider = userProvider;
        }

        public Task<Model.Office> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
        {
            var office = _officeService.GetOfficeById(id);
            
            return Task.FromResult(_toResourceMapper.Map(office));
        }

        public Task<IEnumerable<Model.Office>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
        {
            //var offices = _officeService.GetAll().AsEnumerable();
            var offices = _officeService.GetAll().Include(k => k.Kegs).AsEnumerable();

            return Task.FromResult(offices.AsEnumerable().Select(o => _toResourceMapper.Map(o)));
        }

        public Task<ResourceCreationResult<Model.Office, int>> CreateAsync(Model.Office resource, IRequestContext context, CancellationToken cancellation)
        {
            //var domainOffice = _toTransportMapper.Map(resource);
            var modelOffice = _officeService.Add(resource.Name);

            return Task.FromResult(new ResourceCreationResult<Office, int>(_toResourceMapper.Map(modelOffice)));
        }

        public Task<Model.Office> UpdateAsync(Model.Office resource, IRequestContext context, CancellationToken cancellation)
        {
            var office = _officeService.Update(resource.Id, resource.Name, resource.Description);

            return Task.FromResult(_toResourceMapper.Map(office));
            //throw new NotImplementedException();
        }

        public Task DeleteAsync(ResourceOrIdentifier<Model.Office, int> input, IRequestContext context, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
