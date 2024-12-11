# City Power and Light (CPL) Scenario repo

private repo for back up purposes

## Summary

A console application demonstrating CRUD operations on the Dataverse tables of Contact, Account and Incident.

## Usage

Applying Microsoft.Xrm.Sdk.IOrganizationService to manipulate data on the Dataverse. 

## Requires

application requires a json file called appsettings.json in the Config folder.

The file's format:
{
  "Resource": "", connection uri
  "Secret": "", the secret
  "ClientID": "", the client id
  "RedirectURI": "http://localhost"
}

this is used to connect and interact with the dataverse

### Access requirements

The above connection requires read/write access to the Account, Contact and Incident tables of the Dataverse URI supplied
