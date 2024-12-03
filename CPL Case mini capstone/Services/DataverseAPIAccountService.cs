using CPL_Case_mini_capstone.Model;
using Microsoft.Identity.Client; //Microsoft Authentification Library (MSAL)
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;


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
            } while(results.MoreRecords);
            
            return allAccounts;
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
            return dataverseConnection.Create(account);
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
            ColumnSet columns = new(true);
            return (Account)dataverseConnection.Retrieve(Account.EntityLogicalName, accountID, columns);
        }

        /// <summary>
        /// Updates an existing account record in Dataverse.
        /// </summary>
        /// <param name="account">
        /// The <see cref="Account"/> object containing the updated Account data.
        /// </param>
        /// <remarks>
        /// Ensure that the <paramref name="account"/> object contains valid and complete data.
        /// An exception may be thrown if the record fails validation or if the user lacks sufficient permissions.
        /// </remarks>
        public void Update(Account account)
        {
            dataverseConnection.Update(account);
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
        /// <b>Note:</b> An exception may be thrown if:
        /// - The account record is not found.
        /// - The provided GUID is invalid.
        /// - The user lacks sufficient delete permissions for the account table.
        /// </para>
        /// </remarks>
        public void Delete(Guid accountID)
        {
            dataverseConnection.Delete(Account.EntityLogicalName, accountID);
        }


    }
}
