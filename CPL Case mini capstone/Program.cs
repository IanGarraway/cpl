using CPL_Case_mini_capstone.Services;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using CPL_Case_mini_capstone.Configurations;
using CPL_Case_mini_capstone.utilities;
using CPL_Case_mini_capstone.ServiceDemos;

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

            //CRUD Demos
            ContactCrudDemo.ContactCrud(contactService);        
            AccountCrudDemo.AccountCrud(accountService);
            CaseCrudDemo.CaseCrud(caseService, contactService);

            
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