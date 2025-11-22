using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusVisualView : View
{
    [SerializeField] private List<BonusVisual> visuals = new();

    public void SetBonusCount(BonusType bonusType, int count)
    {
        var visual = GetBonusVisual(bonusType);

        if(visual == null)
        {
            Debug.LogWarning("Not found BonusVisual with BonusType - " + bonusType);
            return;
        }

        visual.SetBonusCount(count);
    }

    private BonusVisual GetBonusVisual(BonusType bonusType)
    {
        return visuals.Find(data => data.BonusType == bonusType);
    }
}
