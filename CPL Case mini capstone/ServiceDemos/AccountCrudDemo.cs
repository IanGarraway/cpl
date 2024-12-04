using CPL_Case_mini_capstone.Model;
using CPL_Case_mini_capstone.Services;
using CPL_Case_mini_capstone.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static void AccountCrud(DataverseAPIAccountService accountService)
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
    }
}
