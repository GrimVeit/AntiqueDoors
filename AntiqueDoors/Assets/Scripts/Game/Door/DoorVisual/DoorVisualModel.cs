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
        _storeDoorEventsProvider.OnDoorsCreated -= SetDoors;
    }

    public void Dispose()
    {

    }

    public void ChooseVisual(int doorId)
    {
        if (_doors[doorId].HasLock)
        {

        }
        else
        {

        }
    }

    private void SetDoors(List<Door> doors)
    {
        _doors = doors;
    }

    #region Output

    #endregion
}
