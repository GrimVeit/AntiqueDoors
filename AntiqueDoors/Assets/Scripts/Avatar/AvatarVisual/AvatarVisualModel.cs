using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarVisualModel
{
    private readonly IAvatarEventsProvider _avatarEventsProvider;
    private readonly IAvatarInfoProvider _avatarInfoProvider;
    private readonly IAvatarProvider _avatarProvider;

    private int _currentAvatarId;

    public AvatarVisualModel(IAvatarEventsProvider avatarEventsProvider, IAvatarInfoProvider avatarInfoProvider, IAvatarProvider avatarProvider)
    {
        _avatarEventsProvider = avatarEventsProvider;
        _avatarInfoProvider = avatarInfoProvider;
        _avatarProvider = avatarProvider;

        _avatarEventsProvider.OnChooseAvatar += OnSelect;
    }

    public void Initialize()
    {
        _currentAvatarId = _avatarInfoProvider.GetCurrentAvatar();
        OnSelectAvatar?.Invoke(_currentAvatarId);
    }

    public void Dispose()
    {
        _avatarEventsProvider.OnChooseAvatar -= OnSelect;
    }

    public void LeftClick()
    {
        OnClickToLeft?.Invoke();
    }

    public void RightClick()
    {
        OnClickToRight?.Invoke();
    }

    public void SelectAvatar(int id)
    {
        _avatarProvider.SelectAvatar(id);
    }


    private void OnSelect(int id)
    {
        if(_currentAvatarId != id)
        {
            OnDeselectAvatar?.Invoke(_currentAvatarId);
        }

        _currentAvatarId = id;
        OnSelectAvatar?.Invoke(_currentAvatarId);
    }


    #region Output

    public event Action OnClickToLeft;
    public event Action OnClickToRight;

    public event Action<int> OnSelectAvatar;
    public event Action<int> OnDeselectAvatar;

    #endregion
}
