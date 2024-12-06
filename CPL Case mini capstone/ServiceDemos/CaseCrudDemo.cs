using CPL_Case_mini_capstone.Model;
using CPL_Case_mini_capstone.Services;
using CPL_Case_mini_capstone.utilities;
using Microsoft.Xrm.Sdk;


namespace CPL_Case_mini_capstone.ServiceDemos
{
    internal class CaseCrudDemo
    {
        public static void CaseCrud(DataverseAPICaseService caseService, DataverseAPIContactService contactService)
        {
            Console.WriteLine("******************************************");
            Console.WriteLine("Demonstration of CRUD on the Case table");

            //Creation of a Contact
            Contact contact = new()
            {
                FirstName = "Bucky",
                LastName = "OHaire",
            };

            Guid contactID = contactService.Create(contact);
            contact = contactService.Get(contactID);
            
            //Before 
            Console.WriteLine("\nBefore creation:");
            DisplayUtility.DisplayListByProperty(caseService.GetAll().ToList());

            //Create an an incident (case)

            Console.WriteLine("Creating an Incident Case...");

            Incident aCase = new()
            {
                Title = "Manifold Replacement",
                Description = "Manifold in need of replacement",
                PriorityCode = incident_prioritycode.Normal,
                CustomerId = new EntityReference(Contact.EntityLogicalName, contactID),
                CaseOriginCode = incident_caseorigincode.Phone,
                StatusCode = incident_statuscode.Researching,

            };

            Guid caseID = caseService.Create(aCase); //creates an incident and returns a GUID for the incident

            Console.WriteLine("Incident Case created");


            Console.WriteLine("\nAfter Creation:");
            DisplayUtility.DisplayListByProperty(caseService.GetAll().ToList());

            //Retrieve a specific account
            aCase = caseService.Get(caseID);

            Console.WriteLine($"\nCase: {aCase.Description} priority: {aCase.PriorityCode} created on: {aCase.CreatedOn}");

            //Update the account
            Console.WriteLine("Updating the Case...");
            aCase.Description = "Manifold in DESPERATE need of replacement";
            aCase.PriorityCode = incident_prioritycode.High;

            caseService.Update(aCase);

            //Retrieve the updated account
            aCase = caseService.Get(caseID);

            Console.WriteLine($"\nCase after modification: {aCase.Description} priority: {aCase.PriorityCode} created on: {aCase.CreatedOn} modified on: {aCase.ModifiedOn}");

            Console.WriteLine("\nAfter Modification: ");
            DisplayUtility.DisplayListByProperty(caseService.GetAll().ToList());

            //Delete the Account and Contact
            caseService.Delete(caseID);
            contactService.Delete(contactID);

            Console.WriteLine("\nAfter Deletion: ");
            DisplayUtility.DisplayListByProperty(caseService.GetAll().ToList());
            Console.WriteLine("\n******************************************");
            Console.WriteLine("Enter to Continue");
            Console.WriteLine("******************************************");
            Console.ReadLine();
        }
    }
}
