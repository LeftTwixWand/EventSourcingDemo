namespace EventSourcingDemo.Events;

record ProductReceived(string Sku, int Quantity, DateTime DateTime) : IEvent;