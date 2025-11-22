using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRewardState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IBonusRewardProvider _bonusRewardProvider;
    private readonly IBonusRewardEventsProvider _bonusRewardEventsProvider;

    public BonusRewardState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IBonusRewardProvider bonusRewardProvider, IBonusRewardEventsProvider bonusRewardEventsProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _bonusRewardProvider = bonusRewardProvider;
        _bonusRewardEventsProvider = bonusRewardEventsProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE BONUS REWARD STATE</color>");

        _bonusRewardEventsProvider.OnAllBonusRewarded += ChangeStateToStartMenu;

        _bonusRewardProvider.ActivateMove();
        _sceneRoot.OpenMainPanel();
    }

    public void ExitState()
    {
        _bonusRewardEventsProvider.OnAllBonusRewarded -= ChangeStateToStartMenu;

        _sceneRoot.CloseBonusRewardPanel();
    }

    private void ChangeStateToStartMenu()
    {
        _machineProvider.SetState(_machineProvider.GetState<StartMainState_Game>());
    }
}
