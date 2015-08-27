using System.Collections.Generic;


namespace ApiTest.InventoryApi
{
    public class UnitOfMeasure
    {
        public int id { get; set; }
        public string code { get; set; }
    }


    public sealed class InventoryStatus
    {
        public static int Active = 0;
        public static int OnHold = 1;
        public static int Inactive = 2;
    }


    public sealed class InventoryType
    {
        public static string Normal = "N";
        public static string NonPhysical = "V";
        public static string Manufactured = "M";
        public static string Kitted = "K";
        public static string RawMaterial = "R";
    }


    public class Inventory
    {
        public int id { get; set; }
        public string partNo { get; set; }
        public string whse { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public int status { get; set; }
        public decimal onHandQty { get; set; }
        public decimal committedQty { get; set; }
        public decimal backorderQty { get; set; }
        public Dictionary<string, UnitOfMeasure> unitOfMeasures { get; set; }
    }


    public class InventoryClient : BaseObjectClient<Inventory>
    {
        public InventoryClient(ApiClient client) : base(client) { }

        public override string Resource
        {
            get
            {
                return "inventory/items/";
            }
        }
    }
}
