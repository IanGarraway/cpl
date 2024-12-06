using CPL_Case_mini_capstone.Model;
using CPL_Case_mini_capstone.Services;
using CPL_Case_mini_capstone.utilities;
using Microsoft.Xrm.Sdk;


namespace CPL_Case_mini_capstone.ServiceDemos
{
    internal class AccountCrudDemo
    {
        /// <summary>
        /// Method for demonstration of Account Services CRUD methods
        /// </summary>
        /// <param name="contactService"></param>
        /// <param name="accountService"></param>
        /// <param name="userID"></param>
        public static void AccountCrud(DataverseAPIAccountService accountService, DataverseAPIContactService contactService)
        {
            Console.WriteLine("******************************************");
            Console.WriteLine("Demonstration of CRUD on the Account table");

            //Contact

            Contact contact = new Contact()
            {
                FirstName = "Bucky",
                LastName = "OHaire",
                EMailAddress1 = "Bucky@RIShipping.com",
                Telephone1 = "0777 2456 567",
            };

            Guid contactID = contactService.Create(contact);
            contact = contactService.Get(contactID);


            //Before 
            Console.WriteLine("\nBefore creation:");
            DisplayUtility.DisplayListByProperty(accountService.GetAll().ToList());

            //Create an account
            Console.WriteLine("Creating an Account...");

            Account account = new()
            {
                Name = "Righteous Indignation",
                Address1_Line1 = "RIShipping House",
                Address1_City = "Leeds",
                Telephone1 = "0777 2456 567",
                PrimaryContactId = new EntityReference(Account.EntityLogicalName, contactID),
            };

            
            

            Guid accountID = accountService.Create(account); //creates an account and returns a GUID for the account

            Console.WriteLine("Account Created.");


            Console.WriteLine("\nAfter Creation:");
            DisplayUtility.DisplayListByProperty(accountService.GetAll().ToList());

            //Retrieve a specific account
            account = accountService.Get(accountID);

            Console.WriteLine($"\nAccount: {account.Name} created on: {account.CreatedOn}");

            //Update the account
            account.Name = "Righteous Indignation plc";

            accountService.Update(account);

            //Retrieve the updated account
            account = accountService.Get(accountID);

            Console.WriteLine($"\nAccount after modification: {account.Name} created on: {account.CreatedOn} modified on: {account.ModifiedOn}");
            Console.WriteLine("\nAfter Update: ");
            DisplayUtility.DisplayListByProperty(accountService.GetAll().ToList());

            //Delete the Account
            accountService.Delete(accountID);
            contactService.Delete(contactID);

            Console.WriteLine("\nAfter Deletion: ");
            DisplayUtility.DisplayListByProperty(accountService.GetAll().ToList());
            Console.WriteLine("\n******************************************");
            Console.WriteLine("Enter to Continue");
            Console.WriteLine("******************************************");
            Console.ReadLine();

        }
    }
}
