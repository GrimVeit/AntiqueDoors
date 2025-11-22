using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRewardPresenter : IBonusRewardProvider, IBonusRewardEventsProvider
{
    private readonly BonusRewardModel _model;
    private readonly BonusRewardView _view;

    public BonusRewardPresenter(BonusRewardModel model, BonusRewardView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _view.OnAllBonusRewarded += _model.AllBonusRewarded;
        _view.OnAddBonus += _model.AddBonus;

        _model.OnChooseBonusesForReward += _view.ChooseBonusTypesForReward;
        _model.OnActivateMove += _view.ActivateMove;
    }

    private void DeactivateEvents()
    {
        _view.OnAllBonusRewarded -= _model.AllBonusRewarded;
        _view.OnAddBonus -= _model.AddBonus;

        _model.OnChooseBonusesForReward -= _view.ChooseBonusTypesForReward;
        _model.OnActivateMove -= _view.ActivateMove;
    }

    #region Outpuut

    public event Action OnAllBonusRewarded
    {
        add => _model.OnAllBonusRewarded += value;
        remove => _model.OnAllBonusRewarded -= value;
    }

    #endregion

    #region Input

    public void CreateBonuses(int count) => _model.CreateBonuses(count);
    public void ActivateMove() => _model.ActivateMove();

    #endregion
}

public interface IBonusRewardProvider
{
    public void CreateBonuses(int count);
    public void ActivateMove();
}

public interface IBonusRewardEventsProvider
{
    public event Action OnAllBonusRewarded;
}
