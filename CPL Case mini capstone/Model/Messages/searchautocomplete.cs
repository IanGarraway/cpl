#pragma warning disable CS1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CPL_Case_mini_capstone.Model
{
	
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011/new/")]
	[Microsoft.Xrm.Sdk.Client.RequestProxyAttribute("searchautocomplete")]
	public partial class searchautocompleteRequest : Microsoft.Xrm.Sdk.OrganizationRequest
	{
		
		public string filter
		{
			get
			{
				if (this.Parameters.Contains("filter"))
				{
					return ((string)(this.Parameters["filter"]));
				}
				else
				{
					return default(string);
				}
			}
			set
			{
				this.Parameters["filter"] = value;
			}
		}
		
		public string entities
		{
			get
			{
				if (this.Parameters.Contains("entities"))
				{
					return ((string)(this.Parameters["entities"]));
				}
				else
				{
					return default(string);
				}
			}
			set
			{
				this.Parameters["entities"] = value;
			}
		}
		
		public string options
		{
			get
			{
				if (this.Parameters.Contains("options"))
				{
					return ((string)(this.Parameters["options"]));
				}
				else
				{
					return default(string);
				}
			}
			set
			{
				this.Parameters["options"] = value;
			}
		}
		
		public bool fuzzy
		{
			get
			{
				if (this.Parameters.Contains("fuzzy"))
				{
					return ((bool)(this.Parameters["fuzzy"]));
				}
				else
				{
					return default(bool);
				}
			}
			set
			{
				this.Parameters["fuzzy"] = value;
			}
		}
		
		public string propertybag
		{
			get
			{
				if (this.Parameters.Contains("propertybag"))
				{
					return ((string)(this.Parameters["propertybag"]));
				}
				else
				{
					return default(string);
				}
			}
			set
			{
				this.Parameters["propertybag"] = value;
			}
		}
		
		public string search
		{
			get
			{
				if (this.Parameters.Contains("search"))
				{
					return ((string)(this.Parameters["search"]));
				}
				else
				{
					return default(string);
				}
			}
			set
			{
				this.Parameters["search"] = value;
			}
		}
		
		public searchautocompleteRequest()
		{
			this.RequestName = "searchautocomplete";
			this.search = default(string);
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011/new/")]
	[Microsoft.Xrm.Sdk.Client.ResponseProxyAttribute("searchautocomplete")]
	public partial class searchautocompleteResponse : Microsoft.Xrm.Sdk.OrganizationResponse
	{
		
		public searchautocompleteResponse()
		{
		}
		
		public string response
		{
			get
			{
				if (this.Results.Contains("response"))
				{
					return ((string)(this.Results["response"]));
				}
				else
				{
					return default(string);
				}
			}
		}
	}
}
#pragma warning restore CS1591
