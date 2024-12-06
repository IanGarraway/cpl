
using CPL_Case_mini_capstone.Model;

namespace CPL_Case_mini_capstone.utilities
{
    internal class DisplayUtility
    {
        /// <summary>
        /// Displays a specific property or formatted string representation of each item in a list.
        /// This method is generic and can handle any object type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"> A list of objects containing the data to be displayed</param>
        /// <param name="propertySelector"> Use a lambda expression to specify the data item or items to be displayed</param>
        public static void DisplayListByProperty<T>(List<T> items, Func<T, string> propertySelector)
        {
            foreach (T item in items)
            {
                Console.WriteLine(propertySelector(item));
            }
        }

        /// <summary>
        /// Displays the Account details (Name, Phone number, Address, City, and Contact) in a table format on the console.
        /// If the list is empty or null, a message indicating no data will be displayed.
        /// </summary>
        /// <param name="accounts">A list of Account objects to be displayed.</param>
        public static void DisplayListByProperty(List<Account> accounts)
        {
            int namePadding = 25, phonePadding = 15, addressPadding = 30, cityPadding = 10;

            //Print the table's headers
            Console.WriteLine($"{"Account Name".PadRight(namePadding)}| {"Phone".PadRight(phonePadding)}| {"Address".PadRight(addressPadding)}| " +
                $"{"City".PadRight(cityPadding)}| {"Contact"}");

            if (accounts == null || accounts.Count == 0)
            {
                Console.WriteLine("No Accounts to display");
                return;
            }

            //Display's each incident's infomation in the specified format
            DisplayListByProperty(accounts, account => $"{(account.Name ?? "N/A").PadRight(namePadding)}| " +
            $"{(account.Telephone1 ?? "N/A").PadRight(phonePadding)}| " +
            $"{(account.Address1_Line1 ?? "N/A").PadRight(addressPadding)}| {(account.Address1_City ?? "N/A").PadRight(cityPadding)}| " +
            $"{(account.PrimaryContactId?.Name ?? "N/A")}");
        }

        /// <summary>
        /// Displays the details (Name, Company name, Email, and Phone number) of Contact objects in a table format on the console.
        /// If the list is empty or null, a message indicating no data will be displayed.
        /// </summary>
        /// <param name="contacts">A list of Contact objects to be displayed.</param>
        public static void DisplayListByProperty(List<Contact> contacts)
        {
            int namePadding = 25, emailPadding = 40, companyNamePadding = 25;

            //Print the table's headers
            Console.WriteLine($"{"Name".PadRight(namePadding)}| {"Company Name".PadRight(companyNamePadding)}| {"Email".PadRight(emailPadding)}| Phone ");
            
            if(contacts == null || contacts.Count == 0)
            {
                Console.WriteLine("No Contacts to display");
                return;
            }

            //Display's each incident's infomation in the specified format
            DisplayListByProperty(contacts, contact => $"{(contact.FullName ?? "N/A").PadRight(namePadding)}| " +
            $"{(contact.Company ?? "N/A").PadRight(companyNamePadding)}| {(contact.EMailAddress1 ?? "N/A").PadRight(emailPadding)}| {(contact.Telephone1 ?? "N/A")}");

        }

        /// <summary>
        /// Displays the details of Incident objects, including Title, Case number, Priority, Origin, Customer, Status code, creation date, and description.
        /// The data is displayed in a table format on the console.
        /// If the list is empty or null, a message indicating no data will be displayed.
        /// </summary>
        /// <param name="incidents">A list of Incident objects to be displayed.</param>
        public static void DisplayListByProperty(List<Incident> incidents)
        {
            int titlePadding = 60, numberPadding = 16, priorityPadding = 10, originPadding = 10, customerPadding = 30, statusPadding = 15, createdOnPadding = 23;

            //Print the table's headers
            Console.WriteLine($"{"Title".PadRight(titlePadding)}| {"Case #".PadRight(numberPadding)}| {"Priority".PadRight(priorityPadding)}| " +
                $"{"Origin".PadRight(originPadding)}| {"Customer".PadRight(customerPadding)}| {"Status".PadRight(statusPadding)}| " +
                $"{"Created".PadRight(createdOnPadding)}| Description");
            
            if (incidents == null || incidents.Count == 0)
            {
                Console.WriteLine("No Contacts to display");
                return;
            }

            //Display's each incident's infomation in the specified format
            DisplayListByProperty(incidents, incident => $"{(incident.Title ?? "N/A").PadRight(titlePadding)}| {(incident.TicketNumber ?? "N/A").PadRight(numberPadding)}| " +
            $"{(incident.PriorityCode?.ToString() ?? "N/A").PadRight(priorityPadding)}| {(incident.CaseOriginCode?.ToString() ?? "N/A").PadRight(originPadding)}| " +
            $"{(incident.CustomerId?.Name ?? "N/A").PadRight(customerPadding)}| {(incident.StatusCode?.ToString() ?? "N/A").PadRight(statusPadding)}| " +
                $"{(incident.CreatedOn?.ToString() ?? "N/A").PadRight(createdOnPadding)}| {(incident.Description ?? "N/A")}");


        }
    }

    
}
