namespace EventSourcingDemo.Events;

record ProductShipped(string Sku, int Quantity, DateTime DateTime) : IEvent;