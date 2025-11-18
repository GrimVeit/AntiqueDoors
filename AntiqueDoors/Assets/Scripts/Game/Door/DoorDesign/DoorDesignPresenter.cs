using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDesignPresenter
{
    private readonly DoorDesignModel _model;
    private readonly DoorDesignView _view;

    public DoorDesignPresenter(DoorDesignModel model, DoorDesignView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnDesignChanged += _view.SetDesigns;
    }

    public void DeactivateEvents()
    {
        _model.OnDesignChanged -= _view.SetDesigns;
    }
}
