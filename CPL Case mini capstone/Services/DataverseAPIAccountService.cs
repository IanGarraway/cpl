using CPL_Case_mini_capstone.Model;
using Microsoft.Identity.Client; //Microsoft Authentification Library (MSAL)
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.ServiceModel;


namespace CPL_Case_mini_capstone.Services
{
    /// <summary>
    /// Service for managing CRUD operations on the Accounts table in Dataverse.
    /// </summary>
    /// <remarks>
    /// This class leverages the <see cref="IOrganizationService"/> interface to interact with Dataverse.
    /// Ensure the user has sufficient permissions and the provided data is valid for all operations.
    /// </remarks>
    internal class DataverseAPIAccountService
    {
        private IOrganizationService dataverseConnection;
        
        public DataverseAPIAccountService(IOrganizationService dataverseConnection)
        {
            this.dataverseConnection = dataverseConnection;
        }

        /// <summary>
        /// Retrieves all Account records from Dataverse using paging.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="Account"/> objects representing all retrieved Accounts.
        /// </returns>
        /// <remarks>
        /// This method fetches records in batches of up to 5000 to handle large datasets. 
        /// Ensure the calling user has sufficient privileges to access the Contact table.
        /// </remarks>
        public IEnumerable<Account> GetAll()
        {
            try
            {
                QueryExpression query = new QueryExpression(Account.EntityLogicalName)
                {
                    ColumnSet = new ColumnSet(true),
                    PageInfo = new PagingInfo() { Count = 5000, PageNumber = 1 }
                };

                List<Account> allAccounts = [];
                EntityCollection results;

                do
                {
                    results = dataverseConnection.RetrieveMultiple(query);

                    foreach (Entity record in results.Entities)
                    {
                        Account account = (Account)record;
                        allAccounts.Add(account);
                    }
                    query.PageInfo.PageNumber++;
                } while (results.MoreRecords);

                return allAccounts;
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
        }

        /// <summary>
        /// Creates a new Account record in Dataverse and returns the GUID of the created record.
        /// </summary>
        /// <param name="account">
        /// The <see cref="Account"/> object representing the contact to be created.
        /// </param>
        /// <returns>
        /// A <see cref="Guid"/> representing the unique identifier of the created Account record.
        /// </returns>
        /// <remarks>
        /// Ensure that the <paramref name="account"/> object contains all required fields.
        /// An exception may be thrown if the record fails validation or if the user lacks sufficient permissions.
        /// </remarks>
        public Guid Create(Account account)
        {
            try
            {
                return dataverseConnection.Create(account);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
        }

        /// <summary>
        /// Retrieves and returns a single account record based on it's GUID
        /// </summary>
        /// <param name="accountID"></param>
        /// A <see cref="Guid"/> representing the unique identifier of the account record to be retrieved.
        /// <returns>The <see cref="Contact"/> object representing the account that was retrieved.</returns>
        /// <remarks>
        /// Ensure GUID is valid for a account record, and exception may be thrown if the account record fails to be found, 
        /// the user lacks sufficient permission, or the GUID is invalid.
        /// </remarks>
        public Account Get(Guid accountID)
        {
            try
            {
                ColumnSet columns = new(true);
                return (Account)dataverseConnection.Retrieve(Account.EntityLogicalName, accountID, columns);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates an existing account record in Dataverse based on the provided account data.
        /// </summary>
        /// <param name="account">
        /// An <see cref="Account"/> object containing the updated information for the account.
        /// The object must include the unique identifier for the account being updated.
        /// </param>
        /// <remarks>
        /// This operation modifies the specified account record in Dataverse.
        /// Ensure the <paramref name="account"/> object is valid and includes an existing account ID.
        /// <para>
        /// <b>Note:</b>
        /// An exception of type <see cref="FaultException{OrganizationServiceFault}"/> may be thrown if:
        /// <list type="bullet">
        /// <item>The account record is not found.</item>
        /// <item>The provided account ID is invalid or malformed.</item>
        /// <item>The user lacks sufficient update permissions for the account table.</item>
        /// <item>A Dataverse server-side error occurs, such as a <c>400 Bad Request</c> or transient issue.</item>
        /// </list>
        /// The exception includes detailed information in the <see cref="OrganizationServiceFault"/> object,
        /// such as the fault reason, error code, and message.
        /// </para>
        /// <para>
        /// <b>Developer Guidance:</b>
        /// Ensure proper exception handling is implemented in the calling code to handle potential failures gracefully.
        /// Validate that the account object is correctly populated before calling this method.
        /// </para>
        /// </remarks>
        /// <exception cref="FaultException{OrganizationServiceFault}">
        /// Thrown when the Dataverse service encounters an error during the update operation.
        /// </exception>
        public void Update(Account account)
        {
            try
            {
                dataverseConnection.Update(account);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
        }

        /// <summary>
        /// Deletes an account from Dataverse based on its GUID.
        /// </summary>
        /// <param name="accountID">
        /// A <see cref="Guid"/> representing the unique identifier of the account record to be deleted.
        /// </param>
        /// <remarks>
        /// This operation is irreversible and permanently removes the account record from Dataverse.
        /// Ensure the GUID is valid and corresponds to an existing account record.
        /// <para>
        /// <b>Note:</b>
        /// An exception of type <see cref="FaultException{OrganizationServiceFault}"/> may be thrown if:
        /// <list type="bullet">
        /// <item>The account record is not found.</item>
        /// <item>The provided GUID is invalid or malformed.</item>
        /// <item>The user lacks sufficient delete permissions for the account table.</item>
        /// <item>A Dataverse server-side error occurs, such as a <c>400 Bad Request</c> or transient issue.</item>
        /// </list>
        /// The exception includes detailed information in the <see cref="OrganizationServiceFault"/> object,
        /// such as the fault reason, error code, and message.
        /// </para>
        /// <para>
        /// <b>Developer Guidance:</b>
        /// Ensure proper exception handling is implemented in calling code to handle potential failures gracefully.
        /// Consider validating the GUID before calling this method.
        /// </para>
        /// </remarks>
        /// <exception cref="FaultException{OrganizationServiceFault}">
        /// Thrown when the Dataverse service encounters an error during the delete operation.
        /// </exception>
        public void Delete(Guid accountID)
        {
            

            try
            {
                dataverseConnection.Delete(Account.EntityLogicalName, accountID);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
        }


    }
}
