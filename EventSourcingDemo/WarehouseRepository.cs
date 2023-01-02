internal sealed class WarehouseRepository
{
    private readonly Dictionary<string, IList<IEvent>> _inMomoryStreams = new();

    public WarehouseProduct Get(string sku)
    {
        var warehouseProduct = new WarehouseProduct(sku);

        if (_inMomoryStreams.TryGetValue(sku, out var events))
        {
            foreach (var @event in events)
            {
                warehouseProduct.AddEvent(@event);
            }
        }

        return warehouseProduct;
    }

    public void Save(WarehouseProduct warehouseProduct)
    {
        if (!_inMomoryStreams.ContainsKey(warehouseProduct.Sku))
        {
            _inMomoryStreams.Add(warehouseProduct.Sku, new List<IEvent>());
        }

        _inMomoryStreams[warehouseProduct.Sku] = warehouseProduct.GetEvents();
    }
}