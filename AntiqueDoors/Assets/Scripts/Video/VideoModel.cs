using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoModel
{
    public void Play(string id)
    {
        OnPlay?.Invoke(id);
    }

    #region Output

    public event Action<string> OnPlay;

    #endregion
}
