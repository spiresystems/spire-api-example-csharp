using System;


namespace ApiTest
{
    using InventoryApi;


    public class Program
    {
        static int Main(string[] args)
        {
            var client = new ApiClient("test", "username", "password");
            var inventoryClient = new InventoryClient(client);

            try
            {
                // Create inventory
                var inventory = new Inventory();
                inventory.whse = "00";
                inventory.partNo = "TESTPART";
                inventory.type = InventoryType.Normal;
                inventory.status = InventoryStatus.Active;
                inventory.description = "Test Inventory";
                inventory = inventoryClient.Create(inventory);

                // List inventory matching the query "TEST"
                foreach (var i in inventoryClient.List(0, 100, "TEST"))
                {
                    // i.id
                }

                // Get inventory
                inventory = inventoryClient.Fetch(inventory.id);

                // Update inventory
                inventory.description = "New Description";
                inventoryClient.Update(inventory.id, inventory);

                // Delete inventory
                inventoryClient.Delete(inventory.id);
            }
            catch (ApiException e)
            {
                Console.Error.WriteLine(e.Message);
                return 1;
            }

            return 0;
        }
    }
}