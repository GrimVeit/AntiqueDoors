using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopVisualModel
{
    private readonly IStoreShopEventsProvider _storeShopEventsProvider;
    private readonly IMoneyProvider _moneyProvider;
    private readonly IStoreShopProvider _storeShopProvider;
    private readonly IHealthStoreProvider _healthStoreProvider;

    public ShopVisualModel(IStoreShopEventsProvider storeShopEventsProvider, IMoneyProvider moneyProvider, IStoreShopProvider storeShopProvider, IHealthStoreProvider healthStoreProvider)
    {
        _storeShopEventsProvider = storeShopEventsProvider;
        _moneyProvider = moneyProvider;
        _storeShopProvider = storeShopProvider;
        _healthStoreProvider = healthStoreProvider;

        _storeShopEventsProvider.OnLevelShopChanged += SetChange;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _storeShopEventsProvider.OnLevelShopChanged -= SetChange;
    }

    public void Buy(ShopGroup shopGroup, int levelId, int price)
    {
        if (!_moneyProvider.CanAfford(price))
        {
            Debug.Log("No money");
            return;
        }

        _moneyProvider.SendMoney(-price);

        switch (shopGroup)
        {
            case ShopGroup.Shield:
                _healthStoreProvider.IncreaseMaxShield(1);
                break;
        }

        _storeShopProvider.SetLevel(shopGroup, levelId);

        Debug.Log("BUYED");
    }

    private void SetChange(ShopGroup group, int indexLevel)
    {
        OnLevelShopChanged?.Invoke(group, indexLevel + 1);
    }

    #region Output

    public event Action<ShopGroup, int> OnLevelShopChanged;

    #endregion
}
