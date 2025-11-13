using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardView : View
{
    [SerializeField] private Transform transformContent;
    [SerializeField] private UserGrid userGridPrefabTop3;
    [SerializeField] private UserGrid userGridPrefabOther;

    public void GetTopPlayers(List<UserData> users)
    {
        for (int i = 0; i < users.Count; i++)
        {
            UserGrid grid = null;

            if (i < 3)
            {
                grid = Instantiate(userGridPrefabTop3, transformContent);
            }
            else
            {
                grid = Instantiate(userGridPrefabOther, transformContent);
            }

            grid.SetData(i + 1, users[i].Nickname, users[i].Record);
        }
    }
}
