internal sealed class WarehouseProduct
{
    private readonly List<IEvent> _events = new();

    private readonly CurrentState _currentState = new();

    public WarehouseProduct(string sku)
    {
        Sku = sku;
    }

    public string Sku { get; }

    public void ShipProduct(int quantity)
    {
        if (quantity > _currentState.QuantityOnHand)
        {
            throw new InvalidOperationException("Cannot ship more than available");
        }

        AddEvent(new ProductShipped(Sku, quantity, DateTime.UtcNow));
    }

    public void ReceiveProduct(int quantity)
    {
        if (quantity <= 0)
        {
            throw new InvalidOperationException("Cannot receive a negative quantity");
        }

        AddEvent(new ProductReceived(Sku, quantity, DateTime.UtcNow));
    }

    public void AdjustInventory(int quantity, string reason)
    {
        if (quantity == 0)
        {
            throw new InvalidOperationException("Cannot adjust inventory by zero");
        }

        AddEvent(new InventoryAdjusted(Sku, quantity, reason, DateTime.UtcNow));
    }

    public void AddEvent(IEvent @event)
    {
        switch (@event)
        {
            case ProductShipped productShipped:
                Apply(productShipped);
                break;
            case ProductReceived productReceived:
                Apply(productReceived);
                break;
            case InventoryAdjusted inventoryAdjusted:
                Apply(inventoryAdjusted);
                break;
        }

        _events.Add(@event);
    }

    private void Apply(InventoryAdjusted @event)
    {
        _currentState.QuantityOnHand -= @event.Quantity;
    }

    private void Apply(ProductReceived @event)
    {
        _currentState.QuantityOnHand -= @event.Quantity;
    }

    private void Apply(ProductShipped @event)
    {
        _currentState.QuantityOnHand += @event.Quantity;
    }

    internal IList<IEvent> GetEvents()
    {
        return _events;
    }
}

internal sealed class CurrentState
{
    public int QuantityOnHand { get; internal set; }
}