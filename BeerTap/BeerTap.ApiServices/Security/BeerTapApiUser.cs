using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.AspNet;
using IQ.Platform.Framework.WebApi.Services.Security;

namespace BeerTap.ApiServices.Security
{

    public class BeerTapApiUser : ApiUser<UserAuthData>
    {
        public BeerTapApiUser(string name, Option<UserAuthData> authData)
            : base(authData)
        {
            Name = name;
        }

        public string Name { get; private set; }

    }

    public class BeerTapUserFactory : ApiUserFactory<BeerTapApiUser, UserAuthData>
    {
        public BeerTapUserFactory() :
            base(new HttpRequestDataStore<UserAuthData>())
        {
        }

        protected override BeerTapApiUser CreateUser(Option<UserAuthData> auth)
        {
            return new BeerTapApiUser("BeerTap user", auth);
        }
    }

}
