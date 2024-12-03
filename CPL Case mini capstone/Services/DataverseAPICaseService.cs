using CPL_Case_mini_capstone.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;


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
        /// An exception may be thrown if the record fails validation or if the user lacks sufficient permissions.
        /// </remarks>
        public Guid Create(Incident aCase)
        {
            return dataverseConnection.Create(aCase);
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
        public Incident Get(Guid caseID)
        {
            ColumnSet columns = new(true);
            return (Incident)dataverseConnection.Retrieve(Incident.EntityLogicalName, caseID, columns);
        }

        /// <summary>
        /// Updates an existing incident case in Dataverse.
        /// </summary>
        /// <param name="aCase">
        /// The <see cref="Incident"/> object containing the updated case data.
        /// </param>
        /// <remarks>
        /// Ensure that the <paramref name="aCase"/> object contains valid and complete data.
        /// An exception may be thrown if the record fails validation or if the user lacks sufficient permissions.
        /// </remarks>
        public void Update(Incident aCase)
        {
            dataverseConnection.Update(aCase);
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
        /// <b>Note:</b> An exception may be thrown if:
        /// - The case record is not found.
        /// - The provided GUID is invalid.
        /// - The user lacks sufficient delete permissions for the Case table.
        /// </para>
        /// </remarks>
        public void Delete(Guid caseID)
        {
            dataverseConnection.Delete(Incident.EntityLogicalName, caseID);
        }
    }
}
