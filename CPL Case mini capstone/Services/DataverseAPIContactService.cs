using CPL_Case_mini_capstone.Model;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;



namespace CPL_Case_mini_capstone.Services
{
    /// <summary>
    /// Service for managing CRUD operations on the Contacts table in Dataverse.
    /// </summary>
    /// <remarks>
    /// This class leverages the <see cref="IOrganizationService"/> interface to interact with Dataverse.
    /// Ensure the user has sufficient permissions and the provided data is valid for all operations.
    /// </remarks>
    internal class DataverseAPIContactService
    {
        private IOrganizationService dataverseConnection;
        public DataverseAPIContactService(IOrganizationService dataverseConnection)
        {
            this.dataverseConnection = dataverseConnection;
        }

        /// <summary>
        /// Retrieves all Contact records from Dataverse using paging.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="Contact"/> objects representing all retrieved Contacts.
        /// </returns>
        /// <remarks>
        /// This method fetches records in batches of up to 5000 to handle large datasets. 
        /// Ensure the calling user has sufficient privileges to access the Contact table.
        /// </remarks>
        public IEnumerable<Contact> GetAll()
        {
            QueryExpression query = new QueryExpression(Contact.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(true),
                PageInfo = new PagingInfo() { Count = 5000, PageNumber = 1 }
            };

            List<Contact> allAccounts = [];
            EntityCollection results;

            do
            {
                results = dataverseConnection.RetrieveMultiple(query);

                foreach (Entity record in results.Entities)
                {
                    Contact contact = (Contact)record;
                    allAccounts.Add(contact);
                }
                query.PageInfo.PageNumber++;
            } while (results.MoreRecords);

            return allAccounts;
        }

        /// <summary>
        /// Creates a new Contact record in Dataverse and returns the GUID of the created record.
        /// </summary>
        /// <param name="person">
        /// The <see cref="Contact"/> object representing the contact to be created.
        /// </param>
        /// <returns>
        /// A <see cref="Guid"/> representing the unique identifier of the created contact record.
        /// </returns>
        /// <remarks>
        /// Ensure that the <paramref name="person"/> object contains all required fields.
        /// An exception may be thrown if the record fails validation or if the user lacks sufficient permissions.
        /// </remarks>
        public Guid Create(Contact person)
        {
            return dataverseConnection.Create(person);
        }

        /// <summary>
        /// Retrieves and returns a single contact record based on it's GUID
        /// </summary>
        /// <param name="contactID"></param>
        /// A <see cref="Guid"/> representing the unique identifier of the contact record to be retrieved.
        /// <returns>The <see cref="Contact"/> object representing the contact that was retrieved.</returns>
        /// <remarks>
        /// Ensure GUID is valid for a contact record, and exception may be thrown if the contact record fails to be found, 
        /// the user lacks sufficient permission, or the GUID is invalid.
        /// </remarks>
        public Contact Get(Guid contactID)
        {
            ColumnSet columns = new(true);
            return (Contact)dataverseConnection.Retrieve(Contact.EntityLogicalName, contactID, columns);
        }

        /// <summary>
        /// Updates an existing contact in Dataverse.
        /// </summary>
        /// <param name="person">
        /// The <see cref="Contact"/> object containing the updated contact data.
        /// </param>
        /// <remarks>
        /// Ensure that the <paramref name="person"/> object contains valid and complete data.
        /// An exception may be thrown if the record fails validation or if the user lacks sufficient permissions.
        /// </remarks>
        public void Update(Contact person)
        {
            dataverseConnection.Update(person);
        }

        /// <summary>
        /// Deletes a contact from Dataverse based on its GUID.
        /// </summary>
        /// <param name="contactID">
        /// A <see cref="Guid"/> representing the unique identifier of the contact record to be deleted.
        /// </param>
        /// <remarks>
        /// This operation is irreversible and permanently removes the contact record from Dataverse.
        /// Ensure the GUID is valid and corresponds to an existing contact.
        /// <para>
        /// <b>Note:</b> An exception may be thrown if:
        /// - The contact record is not found.
        /// - The provided GUID is invalid.
        /// - The user lacks sufficient delete permissions for the contact table.
        /// </para>
        /// </remarks>
        public void Delete(Guid contactID)
        {
            dataverseConnection.Delete(Contact.EntityLogicalName,contactID);
        }

    }
}
