using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVisualPresenter
{
    private readonly DoorVisualModel _model;
    private readonly DoorVisualView _view;

    public DoorVisualPresenter(DoorVisualModel model, DoorVisualView view)
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

    }

    private void DeactivateEvents()
    {

    }
}
