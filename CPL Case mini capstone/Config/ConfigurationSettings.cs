using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;

namespace CPL_Case_mini_capstone.Configurations
{
    public class ConfigurationSettings
    {
        public string? Resource { get; set; }
        public string? Secret { get; set; }
        public string? ClientID { get; set; }
        public string? RedirectURI { get; set; }
    }

    /// <summary>
    /// class for containing and accessing the environmental properties 
    /// 
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Class properties, read only once set.
        /// </summary>
        public string? Resource { get; }
        public string? Secret { get; }
        public string? ClientID { get; }
        public string? RedirectURI { get; }
        public bool LoadSuccessful { get; }
        public string? ConnectionString { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class by loading settings from a JSON file.
        /// </summary>
        /// <param name="configFileName">The path to the configuration file.</param>
        /// <remarks>
        /// The configuration file must be in JSON format and match the structure defined by <see cref="ConfigurationSettings"/>.
        /// Throws exceptions if the file cannot be read or the content is invalid.
        /// </remarks>
        /// <exception cref="ArgumentException">Thrown if the file path is null or empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown if the configuration file is not found.</exception>
        /// <exception cref="JsonException">Thrown if the JSON is not well-formed.</exception>
        /// <exception cref="ArgumentNullException">Thrown if required properties are missing in the JSON.</exception>
        public Configuration(string configFileName)
        {

            //load the settings from the appsettings.json
            try
            {
                //read and deserialise the config file
                // Json File Format - appsettings.json
                //{
                //  "Resource": "", connection url
                //  "Secret": "", the secret
                //  "ClientID": "", the client id
                //  "RedirectURI": "http://localhost"
                //}

                string json = File.ReadAllText(configFileName);
                ConfigurationSettings? settings = JsonSerializer.Deserialize<ConfigurationSettings>(json);

                if (settings == null) //check for deserialisation failure
                {
                    throw new InvalidOperationException("Deserialization returned null. Check the JSON format or file path.");
                }

                //Set the properties

                Resource = settings?.Resource ?? throw new ArgumentNullException(nameof(settings.Resource));
                Secret = settings?.Secret ?? throw new ArgumentNullException(nameof(settings.Secret));
                ClientID = settings?.ClientID ?? throw new ArgumentNullException(nameof(settings.ClientID));
                RedirectURI = settings?.RedirectURI ?? throw new ArgumentNullException(nameof(settings.RedirectURI));

                ConnectionString = $@"AuthType=ClientSecret;
                    SkipDiscovery=true; url={Resource};
                    Secret={Secret};
                    ClientId={ClientID};
                    RequireNewInstance=true";

                //Set the flag to indicate a successful load of settings
                LoadSuccessful = true;
            }

            //loading error handling
            catch (FileNotFoundException) //error handling for missing files
            {
                Console.WriteLine("Error: Configuration file not found");
                LoadSuccessful = false;
            }
            catch (JsonException) //error handling for invalid formats
            {
                Console.WriteLine("Error: Invalid Json format.");
                LoadSuccessful = false;
            }
            catch (ArgumentNullException ex) //error handling for null exceptions
            {
                Console.WriteLine($"Error:{ex.Message} is null. ");
                LoadSuccessful = false;
            }
            catch (Exception ex) //other potential errors
            {
                Console.WriteLine($"Error: {ex.Message}");
                LoadSuccessful = false;
            }

        }        
    }
}
