# Spire API Example Using C&#35;

## Overview

This project is intends to provide example usage of the Spire REST API using
Microsoft .NET Framework 4.6. The project will build and run as is, and can be
modified to suit your own requirements.

## Example

Instantiate an API client with a company name, username, and password:

```C#
var client = new ApiClient("test", "username", "password");
var inventory_client = new InventoryClient(client);
```

Create a new inventory item:

```C#
Inventory inventory = new Inventory();
inventory.whse = "00";
inventory.partNo = "TESTPART";
inventory.type = InventoryType.Normal;
inventory.status = InventoryStatus.Active;
inventory.description = "Test Inventory";
inventory = inventory_client.Create(inventory);
```

List inventory matching the query "TEST":

```C#
foreach (var i in inventory_client.List(0, 100, "TEST"))
{
    Console.WriteLine(i.partNo);
}
```

Retreive an inventory item by ID:

```C#
inventory = inventory_client.Fetch(inventory.id);
```

Update a field on an existing inventory item:

```C#
inventory.description = "New Description";
inventory_client.Update(inventory.id, inventory);
```

Delete an inventory item (if inventory is in use this will throw an
`ApiException`):

```C#
inventory_client.Delete(inventory.id);
```


## Documentation

Please see the following for the most up-to-date documentation:

 * [API Documentation](https://localhost:10880/doc) (requires Spire Server)
 * [RestSharp](http://restsharp.org)
 * [Json.NET](http://www.newtonsoft.com/json)
 * [REST API Tutorial](http://www.restapitutorial.com/)
