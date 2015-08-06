using System;


namespace ApiTest
{
    using InventoryApi;


    public class Program
    {
        static int Main(string[] args)
        {
            var client = new ApiClient("test", "username", "password");
            var inventory_client = new InventoryClient(client);
            Inventory inventory;

            try
            {
                // Create inventory
                inventory = new Inventory();
                inventory.whse = "00";
                inventory.partNo = "TESTPART";
                inventory.type = InventoryType.Normal;
                inventory.status = InventoryStatus.Active;
                inventory.description = "Test Inventory";
                inventory = inventory_client.Create(inventory);

                // List inventory
                foreach (var i in inventory_client.List<Inventory>())
                {
                    // i.id
                }

                // Get inventory
                inventory = inventory_client.Fetch<Inventory>(inventory.id);

                // Update inventory
                inventory.description = "New Description";
                inventory_client.Update(inventory.id, inventory);

                // Delete inventory
                inventory_client.Delete(inventory.id);
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