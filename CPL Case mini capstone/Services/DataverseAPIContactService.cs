using CPL_Case_mini_capstone.Model;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System.ServiceModel;



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
            try
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
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
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
            try
            {
                return dataverseConnection.Create(person);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
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
            try
            {
                ColumnSet columns = new(true);
                return (Contact)dataverseConnection.Retrieve(Contact.EntityLogicalName, contactID, columns);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates an existing contact record in Dataverse based on the provided contact data.
        /// </summary>
        /// <param name="contact">
        /// A <see cref="Contact"/> object containing the updated information for the contact.
        /// The object must include the unique identifier for the contact being updated.
        /// </param>
        /// <remarks>
        /// This operation modifies the specified contact record in Dataverse.
        /// Ensure the <paramref name="contact"/> object is valid and includes an existing contact ID.
        /// <para>
        /// <b>Note:</b>
        /// An exception of type <see cref="FaultException{OrganizationServiceFault}"/> may be thrown if:
        /// <list type="bullet">
        /// <item>The contact record is not found.</item>
        /// <item>The provided contact ID is invalid or malformed.</item>
        /// <item>The user lacks sufficient update permissions for the contact table.</item>
        /// <item>A Dataverse server-side error occurs, such as a <c>400 Bad Request</c> or transient issue.</item>
        /// </list>
        /// The exception includes detailed information in the <see cref="OrganizationServiceFault"/> object,
        /// such as the fault reason, error code, and message.
        /// </para>
        /// <para>
        /// <b>Developer Guidance:</b>
        /// Ensure proper exception handling is implemented in the calling code to handle potential failures gracefully.
        /// Validate that the contact object is correctly populated before calling this method.
        /// </para>
        /// </remarks>
        /// <exception cref="FaultException{OrganizationServiceFault}">
        /// Thrown when the Dataverse service encounters an error during the update operation.
        /// </exception>
        public void Update(Contact contact)
        {
            try
            {
                dataverseConnection.Update(contact);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
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
        /// <b>Note:</b>
        /// An exception of type <see cref="FaultException{OrganizationServiceFault}"/> may be thrown if:
        /// <list type="bullet">
        /// <item>The contact record is not found.</item>
        /// <item>The provided GUID is invalid or malformed.</item>
        /// <item>The user lacks sufficient delete permissions for the contact table.</item>
        /// <item>A Dataverse server-side error occurs, such as a <c>400 Bad Request</c> or transient issue.</item>
        /// </list>
        /// The exception includes detailed information in the <see cref="OrganizationServiceFault"/> object,
        /// such as the fault reason and error code.
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
        public void Delete(Guid contactID)
        {
            try
            {
                dataverseConnection.Delete(Contact.EntityLogicalName, contactID);
            }
            catch(FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
            
        }

    }
}
