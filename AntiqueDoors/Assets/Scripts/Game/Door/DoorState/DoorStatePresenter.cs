using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStatePresenter : IDoorStateProvider, IDoorStateEventsProvider
{
    private readonly DoorStateView _view;

    public DoorStatePresenter(DoorStateView view)
    {
        _view = view;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Output

    public event Action OnEndActivateAllDoors { add => _view.OnEndActivateAllDoors += value; remove => _view.OnEndActivateAllDoors -= value; }
    public event Action OnEndDeactivateAllDoors { add => _view.OnEndDeactivateAllDoors += value; remove => _view.OnEndDeactivateAllDoors -= value; }
    public event Action OnEnnOpenDoor { add => _view.OnEnnOpenDoor += value; remove => _view.OnEnnOpenDoor -= value; }

    #endregion



    #region Input

    public void OpenDoor(int id) => _view.Open(id);
    public void DeactivateAll() => _view.DeactivateAll();
    public void ActivateAll() => _view.ActivateAll();

    #endregion
}

public interface IDoorStateProvider
{
    void OpenDoor(int id);
    void DeactivateAll();
    void ActivateAll();
}

public interface IDoorStateEventsProvider
{
    public event Action OnEndActivateAllDoors;
    public event Action OnEndDeactivateAllDoors;
    public event Action OnEnnOpenDoor;
}
