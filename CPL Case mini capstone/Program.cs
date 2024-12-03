using CPL_Case_mini_capstone.Services;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using CPL_Case_mini_capstone.Configurations;
using CPL_Case_mini_capstone.Model;

namespace CPL_Case_mini_capstone
{

    internal class Program
    {
        static Configuration? settings;
        //static AuthenticationToken? userToken;
        static async Task Main(string[] args)
        {
            await Start();           
        }

        static async Task Start()
        {
            Configure();
            
            IOrganizationService service = new ServiceClient(settings.ConnectionString);

            DataverseAPIContactService contactService = new DataverseAPIContactService(service);

            ContactCrud(contactService);        

            Console.ReadLine();
        }
        

        static void Configure()
        {
            settings = new Configuration("config/appsettings.json");
            if (!settings.LoadSuccessful)
            {
                FailOut("Config Load Failed.");                
            }

        }

        static void ContactCrud(DataverseAPIContactService contactService)
        {
            Console.WriteLine("Before:");
            DisplayList(contactService.GetAll().ToList());

            Contact contact = new()
            {
                FirstName = "Ian",
                LastName = "Garroway",
            };

            Guid contactID = contactService.Create(contact);

            Console.WriteLine("After creation");

            DisplayList(contactService.GetAll().ToList());

            Contact retrievedContact = contactService.Get(contactID);

            Console.WriteLine($"Retrieved Contact: {retrievedContact.Id}: {retrievedContact.FullName} created on: {retrievedContact.CreatedOn} ");

            retrievedContact.LastName = "Garraway";

            contactService.Update(retrievedContact);

            retrievedContact = contactService.Get(contactID);

            Console.WriteLine($"Retrieved Contact after typo correction: {retrievedContact.Id}: {retrievedContact.FullName} created on: {retrievedContact.CreatedOn} modified: {retrievedContact.ModifiedOn} ");

            contactService.Delete(contactID);

            Console.WriteLine("After Deletion:");
            DisplayList(contactService.GetAll().ToList());
        }

        

        /// <summary>
        /// Method for exiting the program after informing the user about the cause.
        /// </summary>
        /// <param name="message"></param>
        static void FailOut(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
            Environment.Exit(0);
        }

        static void DisplayList(List<Contact> contacts)
        {           

            foreach (Contact contact in contacts)
            {
                Console.WriteLine(contact.FullName);
            }

        }
       
}
}