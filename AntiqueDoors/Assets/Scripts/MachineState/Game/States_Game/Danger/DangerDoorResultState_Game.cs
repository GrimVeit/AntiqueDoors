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

    public DangerDoorResultState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IDoorCounterProvider doorCounterProvider, IDoorStateProvider doorStateProvider, IVideoProvider videoProvider, IDoorVisualInfoProvider doorVisualInfoProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _doorCounterProvider = doorCounterProvider;
        _doorStateProvider = doorStateProvider;
        _videoProvider = videoProvider;
        _doorVisualInfoProvider = doorVisualInfoProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE DANGER RESULT STATE</color>");

        _sceneRoot.OpenDoorDangerPanel();

        var currentDoor = _doorVisualInfoProvider.GetCurrentDoor();
        _videoProvider.Play($"DoorDanger_{(int)currentDoor.DangerLevel}", ChangeStateToStartMenu);
    }

    public void ExitState()
    {
        _doorCounterProvider.AddCount();
        _doorStateProvider.Hide();
        _sceneRoot.CloseDoorDangerPanel();
    }

    private void ChangeStateToStartMenu()
    {
        _machineProvider.SetState(_machineProvider.GetState<StartMainState_Game>());
    }
}
