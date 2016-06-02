namespace BeerTap.Model
{
    /// <summary>
    /// iQmetrix link relation names
    /// </summary>
    public static class LinkRelations
    {
        /// <summary>
        /// link relation to describe the Identity resource.
        /// </summary>
        public const string SampleResource = "iq:SampleResource";

        /// <summary>
        /// 
        /// </summary>
        public const string Office = "iq:OfficeResource";

        /// <summary>
        /// 
        /// </summary>
        public const string Keg = "iq:KegResource";

        /// <summary>
        /// 
        /// </summary>
        public const string GetBeer = "iq:GetBeerResource";

        /// <summary>
        /// 
        /// </summary>
        public const string ReplaceKeg = "iq:ReplaceKegResource";

        /// <summary>
        /// 
        /// </summary>
        public static class Offices
        {
            /// <summary>
            /// 
            /// </summary>
            public const string AddKeg = "iq:AddKeg";
        }

    }
}
