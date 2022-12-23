namespace EventSourcingDemo.Events;

record InventoryAdjusted(string Sku, int Quantity, string Reason, DateTime DateTime) : IEvent;