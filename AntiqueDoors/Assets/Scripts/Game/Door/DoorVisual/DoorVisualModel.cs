using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVisualModel
{
    private readonly IStoreDoorEventsProvider _storeDoorEventsProvider;

    private List<Door> _doors = new();

    public DoorVisualModel(IStoreDoorEventsProvider storeDoorEventsProvider)
    {
        _storeDoorEventsProvider = storeDoorEventsProvider;

        _storeDoorEventsProvider.OnDoorsCreated += SetDoors;
    }

    public void Initialize()
    {
        
    }

    public void Dispose()
    {
        _storeDoorEventsProvider.OnDoorsCreated -= SetDoors;
    }

    public void ActivateInteraction()
    {
        OnActivateInteraction?.Invoke();
    }

    public void DeactivateInteraction()
    {
        OnDeactivateInteraction?.Invoke();
    }

    public void ChooseDoor(int doorId)
    {
        OnChooseDoor_Value?.Invoke(_doors[doorId]);

        OnChooseDoor?.Invoke();

        //if (_doors[doorId].HasLock)
        //{

        //}
        //else
        //{

        //}
    }

    private void SetDoors(List<Door> doors)
    {
        _doors = doors;
    }

    #region Output

    public event Action<Door> OnChooseDoor_Value;
    public event Action OnChooseDoor;


    public event Action OnActivateInteraction;
    public event Action OnDeactivateInteraction;

    #endregion
}
