using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace CPL_Case_mini_capstone.utilities
{
    /// <summary>
    /// This class access' the WhoAmIRequest and Response for the purposes of using the Dataverse    
    /// </summary>
    internal class WhoAmI
    {
        /// <summary>
        /// Uses the WhoAmI services to determine the users GUID
        /// </summary>
        /// <remarks>
        /// This class leverages the <see cref="IOrganizationService"/> interface to interact with Dataverse.
        /// </remarks>
        /// <param name="service"></param>
        /// <returns>returns a <see cref="Guid"/> for the purposes of identifying the user </returns>
        public static Guid GetGuid(IOrganizationService service)
        {
            WhoAmIRequest whoAmIRequest = new();
            WhoAmIResponse whoAmIResponse = (WhoAmIResponse)service.Execute(whoAmIRequest);

            return whoAmIResponse.UserId;
        }
    }
}
