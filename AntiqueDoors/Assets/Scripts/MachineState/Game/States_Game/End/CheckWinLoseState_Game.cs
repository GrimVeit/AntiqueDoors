using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinLoseState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IDoorCounterInfoProvider _doorCounterInfoProvider;
    private readonly IPlayerHealthInfoProvider _playerHealthInfoProvider;

    public CheckWinLoseState_Game(IGlobalStateMachineProvider machineProvider, IDoorCounterInfoProvider doorCounterInfoProvider, IPlayerHealthInfoProvider playerHealthInfoProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _doorCounterInfoProvider = doorCounterInfoProvider;
        _playerHealthInfoProvider = playerHealthInfoProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE CHECK WIN LOSE STATE</color>");

        if (_doorCounterInfoProvider.GetCountDoor() == 99)
        {
            ChangeStateToWin();
            return;
        }

        if(_playerHealthInfoProvider.CurrentHealth == 0)
        {
            ChangeStateToLose();
            return;
        }

        ChangeStateToStartMainMenu();
    }

    public void ExitState()
    {

    }

    private void ChangeStateToStartMainMenu()
    {
        _sceneRoot.CloseDoorDangerPanel();
        _machineProvider.SetState(_machineProvider.GetState<StartMainState_Game>());
    }

    private void ChangeStateToWin()
    {
        _sceneRoot.CloseDoorDangerPanel();
        _sceneRoot.CloseMainPanel();
        _sceneRoot.CloseFooterPanel();

        _machineProvider.SetState(_machineProvider.GetState<WinState_Game>());
    }

    private void ChangeStateToLose()
    {
        _sceneRoot.CloseMainPanel();
        _sceneRoot.CloseFooterPanel();

        _machineProvider.SetState(_machineProvider.GetState<LoseState_Game>());
    }
}
