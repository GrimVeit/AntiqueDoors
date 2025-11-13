using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPresenter : IVideoProvider
{
    private readonly VideoModel _model;
    private readonly VideoView _view;

    public VideoPresenter(VideoModel model, VideoView view)
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
        _model.OnPlay += _view.Play;
    }

    private void DeactivateEvents()
    {
        _model.OnPlay -= _view.Play;
    }

    #region Input

    public void Play(string id)
    {
        _model.Play(id);
    }

    #endregion
}

public interface IVideoProvider
{
    public void Play(string id);
}
