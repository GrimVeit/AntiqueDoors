using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardView : View
{
    [SerializeField] private Transform transformContent;
    [SerializeField] private UserGrid userGridPrefab;

    public void GetTopPlayers(List<UserData> users)
    {
        for (int i = 0; i < users.Count; i++)
        {
            UserGrid grid = Instantiate(userGridPrefab, transformContent);

            if (i == 0)
            {
                grid.SetData(users[i].Nickname, users[i].Record, 3);
            }
            else if(i == 1)
            {
                grid.SetData(users[i].Nickname, users[i].Record, 2);
            }
            else if (i == 2)
            {
                grid.SetData(users[i].Nickname, users[i].Record, 1);
            }
            else
            {
                grid.SetData(users[i].Nickname, users[i].Record, 0);
            }
        }
    }
}
