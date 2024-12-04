using CPL_Case_mini_capstone.Model;
using CPL_Case_mini_capstone.Services;
using CPL_Case_mini_capstone.utilities;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            DisplayUtility.DisplayListByProperty(caseService.GetAll().ToList(), aCase => $"{aCase.Title}: {aCase.PriorityCode} - {aCase.Description}");

            //Create an an incident (case)

            Incident aCase = new()
            {
                Title = "Manifold Replacement",
                Description = "Manifold in need of replacement",
                PriorityCode = incident_prioritycode.Normal,
                CustomerId = new EntityReference(Contact.EntityLogicalName, contactID),

            };

            Guid caseID = caseService.Create(aCase); //creates an incident and returns a GUID for the incident


            Console.WriteLine("\nAfter Creation:");
            DisplayUtility.DisplayListByProperty(caseService.GetAll().ToList(), aCase => $"{aCase.Title}: {aCase.PriorityCode} - {aCase.Description}");

            //Retrieve a specific account
            aCase = caseService.Get(caseID);

            Console.WriteLine($"\nAccount: {aCase.Description} priority: {aCase.PriorityCode} created on: {aCase.CreatedOn}");

            //Update the account
            aCase.Description = "Manifold in DESPERATE need of replacement";
            aCase.PriorityCode = incident_prioritycode.High;

            caseService.Update(aCase);

            //Retrieve the updated account
            aCase = caseService.Get(caseID);

            Console.WriteLine($"\nAccount: {aCase.Description} priority: {aCase.PriorityCode} created on: {aCase.CreatedOn} modified on: {aCase.ModifiedOn}");

            //Delete the Account and Contact
            caseService.Delete(caseID);
            contactService.Delete(contactID);

            Console.WriteLine("\nAfter Deletion: ");
            DisplayUtility.DisplayListByProperty(caseService.GetAll().ToList(), aCase => $"{aCase.Title}: {aCase.PriorityCode} - {aCase.Description}");
            Console.WriteLine("\n******************************************");
            Console.WriteLine("Enter to Continue");
            Console.WriteLine("******************************************");
            Console.ReadLine();
        }
    }
}
