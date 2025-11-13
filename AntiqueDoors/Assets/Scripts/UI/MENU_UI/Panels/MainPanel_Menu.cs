using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonLeaderboard;
    [SerializeField] private Button buttonWardrobe;
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonExit;

    [SerializeField] private List<UIEffectCombination> uIEffectCombinations = new List<UIEffectCombination>();

    public override void Initialize()
    {
        base.Initialize();

        buttonLeaderboard.onClick.AddListener(() => OnClickToLeaderboard?.Invoke());
        buttonWardrobe.onClick.AddListener(() => OnClickToWardrobe?.Invoke());
        buttonPlay.onClick.AddListener(() => OnClickToPlay?.Invoke());
        buttonExit.onClick.AddListener(() => OnClickToExit?.Invoke());

        uIEffectCombinations.ForEach(data => data.Initialize());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonLeaderboard.onClick.RemoveListener(() => OnClickToLeaderboard?.Invoke());
        buttonWardrobe.onClick.RemoveListener(() => OnClickToWardrobe?.Invoke());
        buttonPlay.onClick.RemoveListener(() => OnClickToPlay?.Invoke());
        buttonExit.onClick.RemoveListener(() => OnClickToExit?.Invoke());

        uIEffectCombinations.ForEach(data => data.Dispose());
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        uIEffectCombinations.ForEach(data => data.ActivateEffect());
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        uIEffectCombinations.ForEach(data => data.DeactivateEffect());
    }

    #region Output

    public event Action OnClickToLeaderboard;
    public event Action OnClickToWardrobe;
    public event Action OnClickToPlay;
    public event Action OnClickToExit;

    #endregion
}
