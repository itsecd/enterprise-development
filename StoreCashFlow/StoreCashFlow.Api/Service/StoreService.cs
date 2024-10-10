using StoreCashFlow.Domain;
namespace StoreCashFlow.Api.Service;

public class StoreService
{
    private List<Store> _stores = [];
    private int _storeId = 1;
    public Store AddStore(Store newStore)
    {
        newStore.StoreId = _storeId++;
        _stores.Add(newStore);
        return newStore;
    }

    public List<Store> GetStores()
    {
        return _stores;
    }
    public Store? GetStoreById(int id)
    {
        return _stores.First(c => c.StoreId == id);
    }

    public bool DeleteStore(int id)
    {
        var store = GetStoreById(id);
        if (store == null)
        {
            return false;
        }
        _stores.Remove(store);
        return true;
    }

    public bool UpdateStore(Store updateStore)
    {
        var store = GetStoreById(updateStore.StoreId);
        if (store == null)
        {
            return false;
        }
        store.Location = updateStore.Location;
        return true;
    }
}
