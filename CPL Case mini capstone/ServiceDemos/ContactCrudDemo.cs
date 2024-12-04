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
    internal class ContactCrudDemo
    {
        /// <summary>
        /// Method for demonstration of Contact Services CRUD methods
        /// </summary>
        /// <param name="contactService"></param>
        public static void ContactCrud(DataverseAPIContactService contactService)
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
    }
}
