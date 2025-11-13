using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AvatarVisual
{
    public int Id => id;
    public Transform TransformAvatar => transformAvatar;

    [SerializeField] private int id;
    [SerializeField] private Button button;
    [SerializeField] private Transform transformAvatar;

    public void Initialize()
    {
        button.onClick.AddListener(() => OnChooseAvatar?.Invoke(id));
    }

    public void Dispose()
    {
        button.onClick.RemoveListener(() => OnChooseAvatar?.Invoke(id));
    }

    #region Output

    public event Action<int> OnChooseAvatar;

    #endregion
}
