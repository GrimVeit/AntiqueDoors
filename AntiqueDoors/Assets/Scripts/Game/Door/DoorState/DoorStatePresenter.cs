using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStatePresenter : IDoorStateProvider
{
    private readonly DoorStateModel _model;
    private readonly DoorStateView _view;

    public DoorStatePresenter(DoorStateModel model, DoorStateView view)
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
        _model.OnActivateAll += _view.ActivateAll;
        _model.OnDeactivateAll += _view.DeactivateAll;
        _model.OnOpen += _view.Open;
    }

    private void DeactivateEvents()
    {
        _model.OnActivateAll -= _view.ActivateAll;
        _model.OnDeactivateAll -= _view.DeactivateAll;
        _model.OnOpen -= _view.Open;
    }

    #region Input

    public void OpenDoor(int id) => _model.OpenDoor(id);
    public void DeactivateAll() => _model.DeactivateAll();
    public void ActivateAll() => _model.ActivateAll();

    #endregion
}

public interface IDoorStateProvider
{
    void OpenDoor(int id);
    void DeactivateAll();
    void ActivateAll();
}
