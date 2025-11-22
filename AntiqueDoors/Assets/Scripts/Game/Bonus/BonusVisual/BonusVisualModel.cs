using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusVisualModel
{
    private readonly IStoreBonusEventsProvider _storeBonusEventsProvider;

    public BonusVisualModel(IStoreBonusEventsProvider storeBonusEventsProvider)
    {
        _storeBonusEventsProvider = storeBonusEventsProvider;
    }

    public void Initialize()
    {
        _storeBonusEventsProvider.OnChangedBonusCount += ChangeBonusCount;
    }

    public void Dispose()
    {
        _storeBonusEventsProvider.OnChangedBonusCount -= ChangeBonusCount;
    }

    #region Output

    public event Action<BonusType, int> OnChangedBonusCount;

    private void ChangeBonusCount(BonusType type, int count)
    {
        OnChangedBonusCount?.Invoke(type, count);
    }

    #endregion
}
