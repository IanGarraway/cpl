using CPL_Case_mini_capstone.Services;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using CPL_Case_mini_capstone.Configurations;
using CPL_Case_mini_capstone.Model;
using CPL_Case_mini_capstone.utilities;

namespace CPL_Case_mini_capstone
{

    internal class Program
    {
        static Configuration? settings;
        
        static void Main(string[] args)
        {
            Start();           
        }

        static void Start()
        {
            Configure();
            
            IOrganizationService service = new ServiceClient(settings.ConnectionString);

            DataverseAPIContactService contactService = new DataverseAPIContactService(service);
            DataverseAPIAccountService accountService = new DataverseAPIAccountService(service);
            DataverseAPICaseService caseService = new DataverseAPICaseService(service);

            Guid userID = WhoAmI.GetGuid(service);

            ContactCrud(contactService);        
            AccountCrud(contactService, accountService, userID);

            
        }
        

        /// <summary>
        /// This method loads the settings from the appsettings.json, if unsucessful will close the program
        /// </summary>
        static void Configure()
        {
            settings = new Configuration("config/appsettings.json");
            if (!settings.LoadSuccessful)
            {
                FailOut("Config Load Failed.");                
            }

        }

        /// <summary>
        /// Method for demonstration of Account Services CRUD methods
        /// </summary>
        /// <param name="contactService"></param>
        /// <param name="accountService"></param>
        /// <param name="userID"></param>
        static void AccountCrud(DataverseAPIContactService contactService, DataverseAPIAccountService accountService, Guid userID)
        {
            Console.WriteLine("******************************************");
            Console.WriteLine("Demonstration of CRUD on the Account table");
            
                       
            //Before 
            Console.WriteLine("\nBefore creation:");
            DisplayUtility.DisplayListByProperty(accountService.GetAll().ToList(), account => account.Name);

            Console.WriteLine();

            //Create an account

            Account account = new()
            {
                Name = "Righteous Indignation",                
                EMailAddress1 = "RIShipping@ship.com",
                Telephone1 = "0777 2456 567",                
            };

            Guid accountID = accountService.Create(account); //creates an account and returns a GUID for the account

            
            Console.WriteLine("\nAfter Creation:");
            DisplayUtility.DisplayListByProperty(accountService.GetAll().ToList(), account => account.Name);

            //Retrieve a specific account
            account = accountService.Get(accountID);
            
            Console.WriteLine($"\nAccount: {account.Name} created on: {account.CreatedOn}");

            //Update the account
            account.Name = "Righteous Indignation plc";

            accountService.Update(account);

            //Retrieve the updated account
            account = accountService.Get(accountID);
            
            Console.WriteLine($"\nAccount: {account.Name} created on: {account.CreatedOn} modified on: {account.ModifiedOn}");

            //Delete the Account
            accountService.Delete(accountID);
            
            Console.WriteLine("\nAfter Deletion: ");
            DisplayUtility.DisplayListByProperty(accountService.GetAll().ToList(), account => account.Name);
            Console.WriteLine("\n******************************************");
            Console.WriteLine("Enter to Continue");
            Console.WriteLine("******************************************");
            Console.ReadLine();

        }

        /// <summary>
        /// Method for demonstration of Contact Services CRUD methods
        /// </summary>
        /// <param name="contactService"></param>
        static void ContactCrud(DataverseAPIContactService contactService)
        {
            Console.WriteLine("******************************************");
            Console.WriteLine("Demonstration of CRUD on the Contact table");
            
            //Before
            Console.WriteLine("\nBefore:");
            DisplayUtility.DisplayListByProperty(contactService.GetAll().ToList(), contact => contact.FullName);

            //Creation of a Contact
            Contact contact = new()
            {
                FirstName = "Ian",
                LastName = "Garroway",
                
            };

            Guid contactID = contactService.Create(contact);
            
            //After creation
            Console.WriteLine("\nAfter creation");

            DisplayUtility.DisplayListByProperty(contactService.GetAll().ToList(), contact => contact.FullName);

            //Retrieve a single Contact
            Contact retrievedContact = contactService.Get(contactID);            
            Console.WriteLine($"\nRetrieved Contact: {retrievedContact.Id}: {retrievedContact.FullName} created on: {retrievedContact.CreatedOn} ");

            //Update a single Contact
            retrievedContact.LastName = "Garraway"; //Fix the typo
            contactService.Update(retrievedContact); 

            //Retrieve the now updated contact
            retrievedContact = contactService.Get(contactID);
            Console.WriteLine($"\nRetrieved Contact after typo correction: {retrievedContact.Id}: {retrievedContact.FullName} created on: {retrievedContact.CreatedOn} modified: {retrievedContact.ModifiedOn} ");

            //Delete the Contact
            contactService.Delete(contactID);

            //After Deletion
            Console.WriteLine("\nAfter Deletion:");
            DisplayUtility.DisplayListByProperty(contactService.GetAll().ToList(), contact => contact.FullName);

            Console.WriteLine("\n******************************************");
            Console.WriteLine("Enter to Continue");
            Console.WriteLine("******************************************");
            Console.ReadLine();
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
    }
}