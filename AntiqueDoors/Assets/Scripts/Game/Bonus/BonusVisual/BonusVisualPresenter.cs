using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusVisualPresenter
{
    private readonly BonusVisualModel _model;
    private readonly BonusVisualView _view;

    public BonusVisualPresenter(BonusVisualModel model, BonusVisualView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnChangedBonusCount += _view.SetBonusCount;
    }

    private void DeactivateEvents()
    {
        _model.OnChangedBonusCount -= _view.SetBonusCount;
    }
}
