using System;
using System.Collections.Generic;


namespace ApiTest
{
    namespace InventoryApi
    {
        public class UnitOfMeasure
        {
            public int id { get; set; }
            public string code { get; set; }
        }


        public sealed class InventoryStatus
        {
            public static string Active = "active";
            public static string OnHold = "onhold";
            public static string Inactive = "inactive";
        }


        public sealed class InventoryType
        {
            public static string Normal = "normal";
            public static string NonPhysical = "nonPhysical";
            public static string Manufactured = "manufactured";
            public static string Kitted = "kitted";
            public static string RawMaterial = "rawMaterial";
        }


        public class Inventory
        {
            public int id { get; set; }
            public string partNo { get; set; }
            public string whse { get; set; }
            public string description { get; set; }
            public string type { get; set; }
            public string status { get; set; }
            public decimal onHandQty { get; set; }
            public decimal committedQty { get; set; }
            public decimal backorderQty { get; set; }
            public Dictionary<string, UnitOfMeasure> unitOfMeasures { get; set; }
        }


        public class InventoryClient : BaseObjectClient
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
}