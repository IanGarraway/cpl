using CPL_Case_mini_capstone.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.ServiceModel;


namespace CPL_Case_mini_capstone.Services
{
    /// <summary>
    /// Service for managing CRUD operations on the Case table in Dataverse.
    /// </summary>
    /// <remarks>
    /// This class leverages the <see cref="IOrganizationService"/> interface to interact with Dataverse.
    /// Ensure the user has sufficient permissions and the provided data is valid for all operations.
    /// </remarks>
    internal class DataverseAPICaseService
    {
        private IOrganizationService dataverseConnection;
        public DataverseAPICaseService(IOrganizationService dataverseConnection)
        {
            this.dataverseConnection = dataverseConnection;
        }

        /// <summary>
        /// Retrieves all incident cases from Dataverse using paging.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="Incident"/> objects representing all retrieved cases.
        /// </returns>
        /// <remarks>
        /// This method fetches records in batches of up to 5000 to handle large datasets. 
        /// Ensure the calling user has sufficient privileges to access the Case table.
        /// </remarks>
        public IEnumerable<Incident> GetAll()
        {
            try
            {
                QueryExpression query = new QueryExpression(Incident.EntityLogicalName)
                {
                    ColumnSet = new ColumnSet(true),
                    PageInfo = new PagingInfo() { Count = 5000, PageNumber = 1 }
                };

                List<Incident> allCases = [];
                EntityCollection results;

                do
                {
                    results = dataverseConnection.RetrieveMultiple(query);

                    foreach (Entity record in results.Entities)
                    {
                        Incident aCase = (Incident)record;
                        allCases.Add(aCase);
                    }
                    query.PageInfo.PageNumber++;
                } while (results.MoreRecords);

                return allCases;
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
        }

        /// <summary>
        /// Creates a new incident case in Dataverse and returns the GUID of the created record.
        /// </summary>
        /// <param name="aCase">
        /// The <see cref="Incident"/> object representing the case to be created.
        /// </param>
        /// <returns>
        /// A <see cref="Guid"/> representing the unique identifier of the created incident case.
        /// </returns>
        /// <remarks>
        /// Ensure that the <paramref name="aCase"/> object contains all required fields.
        /// An exception of type <see cref="FaultException{OrganizationServiceFault}"/> may be thrown if:
        /// <list type="bullet">
        /// <item>record fails validation</item>
        /// <item>User lacks sufficient permissions</item>
        /// </list>        
        /// </remarks>
        /// <exception cref="FaultException{OrganizationServiceFault}">
        /// Thrown when the Dataverse service encounters an error during the create operation.
        /// </exception>
        public Guid Create(Incident aCase)
        {
            try
            {
                return dataverseConnection.Create(aCase);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
        }

        /// <summary>
        /// Retrieves and returns a single incident case based on it's GUID
        /// </summary>
        /// <param name="caseID"></param>
        /// A <see cref="Guid"/> representing the unique identifier of the incident case to be retrieved.
        /// <returns>The <see cref="Incident"/> object representing the case that was retrieved.</returns>
        /// <remarks>
        /// Ensure GUID is valid for a case record, and exception may be thrown if the case record fails to be found, 
        /// the user lacks sufficient permission, or the GUID is invalid.
        /// </remarks>
        /// <exception cref="FaultException{OrganizationServiceFault}">
        /// Thrown when the Dataverse service encounters an error during the retrieve operation.
        /// </exception>
        public Incident Get(Guid caseID)
        {
            try
            {
                ColumnSet columns = new(true);
                return (Incident)dataverseConnection.Retrieve(Incident.EntityLogicalName, caseID, columns);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates an existing incident case in Dataverse.
        /// </summary>
        /// <param name="aCase">
        /// An <see cref="Incident"/> object containing the updated case data.
        /// The object must include the unique identifier for the case being updated.
        /// </param>
        /// <remarks>
        /// This operation modifies the specified case record in Dataverse.
        /// Ensure that the <paramref name="aCase"/> object contains valid and complete data.
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
        /// Validate that the Incident object is correctly populated before calling this method.
        /// </para>
        /// </remarks>
        /// <exception cref="FaultException{OrganizationServiceFault}">
        /// Thrown when the Dataverse service encounters an error during the update operation.
        /// </exception>
        public void Update(Incident aCase)
        {
            try
            {
                dataverseConnection.Update(aCase);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
        }

        /// <summary>
        /// Deletes an incident case from Dataverse based on its GUID.
        /// </summary>
        /// <param name="caseID">
        /// A <see cref="Guid"/> representing the unique identifier of the case to be deleted.
        /// </param>
        /// <remarks>
        /// This operation is irreversible and permanently removes the case record from Dataverse.
        /// Ensure the GUID is valid and corresponds to an existing case.
        /// <para>
        /// An exception of type <see cref="FaultException{OrganizationServiceFault}"/> may be thrown if:
        /// <list type="bullet">
        /// <item> The case record is not found. </item>
        /// <item> The provided GUID is invalid. </item>
        /// <item> The user lacks sufficient delete permissions for the Case table. </item>
        /// <item>A Dataverse server-side error occurs, such as a <c>400 Bad Request</c> or transient issue.</item>
        /// The exception includes detailed information in the <see cref="OrganizationServiceFault"/> object,
        /// such as the fault reason, error code, and message.
        /// </list>
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
        public void Delete(Guid caseID)
        {
            try
            {
                dataverseConnection.Delete(Incident.EntityLogicalName, caseID);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                Console.WriteLine($"Dataverse error: {ex.Detail.Message}");
                throw;
            }
        }
    }
}
