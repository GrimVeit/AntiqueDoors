using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerDoorResultState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IDoorCounterProvider _doorCounterProvider;
    private readonly IDoorStateProvider _doorStateProvider;
    private readonly IVideoProvider _videoProvider;
    private readonly IDoorVisualInfoProvider _doorVisualInfoProvider;

    private readonly IPlayerHealthProvider _playerHealthProvider;
    private Door _currentDoor;

    public DangerDoorResultState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IDoorCounterProvider doorCounterProvider, IDoorStateProvider doorStateProvider, IVideoProvider videoProvider, IDoorVisualInfoProvider doorVisualInfoProvider, IPlayerHealthProvider playerHealthProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _doorCounterProvider = doorCounterProvider;
        _doorStateProvider = doorStateProvider;
        _videoProvider = videoProvider;
        _doorVisualInfoProvider = doorVisualInfoProvider;
        _playerHealthProvider = playerHealthProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE DANGER RESULT STATE</color>");

        _sceneRoot.OpenDoorDangerPanel();

        _currentDoor = _doorVisualInfoProvider.GetCurrentDoor();
        _videoProvider.Play($"DoorDanger_{(int)_currentDoor.DangerLevel}", ChangeStateToStartMenu);

        _playerHealthProvider.TakeDamage((int)_currentDoor.DangerLevel);
    }

    public void ExitState()
    {
        _doorCounterProvider.AddCount();
        _doorStateProvider.Hide();
        _sceneRoot.CloseDoorDangerPanel();

        if (_currentDoor.Type == DoorType.Spikes)
            _playerHealthProvider.TakeDamage(1);
    }

    private void ChangeStateToStartMenu()
    {
        _machineProvider.SetState(_machineProvider.GetState<StartMainState_Game>());
    }
}
