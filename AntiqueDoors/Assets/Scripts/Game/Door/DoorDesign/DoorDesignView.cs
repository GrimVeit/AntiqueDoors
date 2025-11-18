using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DoorDesignView : View
{
    [SerializeField] private RawImage imageDoor_1;
    [SerializeField] private RawImage imageDoor_2;
    [SerializeField] private RawImage imageDoor_3;

    [SerializeField] private DoorDesigns doorDesigns;

    public void SetDesigns(List<DoorType> doors)
    {
        imageDoor_1.texture = doorDesigns.GetSpriteByDoorType(doors[0]).texture;
        imageDoor_2.texture = doorDesigns.GetSpriteByDoorType(doors[1]).texture;
        imageDoor_3.texture = doorDesigns.GetSpriteByDoorType(doors[2]).texture;
    }
}

[System.Serializable]
public class DoorDesigns
{
    [SerializeField] private List<DoorDesign> doorDesigns = new();

    public Sprite GetSpriteByDoorType(DoorType type)
    {
        var sprite = doorDesigns.FirstOrDefault(data => data.Type == type).Sprite;

        if(sprite == null)
        {
            Debug.LogWarning($"Not found DoorSprite by DoorType - {type}");
            return null;
        }

        return sprite;
    }
}

[System.Serializable]
public class DoorDesign
{
    [SerializeField] private DoorType type;
    [SerializeField] private Sprite sprite;

    public DoorType Type => type;
    public Sprite Sprite => sprite;
}
