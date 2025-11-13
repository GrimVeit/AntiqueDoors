using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AvatarView : View
{
    [SerializeField] private List<AvatarVisual> avatarVisuals = new();
    [SerializeField] private List<Image> imageAvatars = new();
    [SerializeField] private SpriteAvatars spriteAvatars;
    [SerializeField] private Transform transformFrame;

    public void Initialize()
    {
        avatarVisuals.ForEach(x =>
        {
            x.OnChooseAvatar += HandleChooseAvatar;
            x.Initialize();
        });
    }

    public void Dispose()
    {
        avatarVisuals.ForEach(x =>
        {
            x.OnChooseAvatar -= HandleChooseAvatar;
            x.Dispose();
        });
    }

    #region Input

    public void Select(int id)
    {
        if(transformFrame != null)
           transformFrame.DOMove(avatarVisuals[id].TransformAvatar.position, 0.2f);

        var avatar = spriteAvatars.GetSpriteById(id);

        for (int i = 0; i < imageAvatars.Count; i++)
        {
            imageAvatars[i].sprite = avatar;
        }
    }

    public void Deselect(int id)
    {

    }

    #endregion

    #region Output

    public event Action<int> OnChooseAvatar;

    private void HandleChooseAvatar(int id)
    {
        OnChooseAvatar?.Invoke(id);
    }

    #endregion

    private AvatarVisual GetAvatarVisualByid(int id)
    {
        return avatarVisuals.FirstOrDefault(data => data.Id == id);
    }
}
