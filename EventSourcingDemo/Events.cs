interface IEvent { }

record InventoryAdjusted(string Sku, int Quantity, string Reason, DateTime DateTime) : IEvent;

record ProductReceived(string Sku, int Quantity, DateTime DateTime) : IEvent;

record ProductShipped(string Sku, int Quantity, DateTime DateTime) : IEvent;