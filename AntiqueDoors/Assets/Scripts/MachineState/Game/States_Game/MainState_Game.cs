using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IDoorVisualActivatorProvider _doorVisualActivatorProvider;
    private readonly IDoorVisualEventsProvider _doorVisualEventsProvider;

    public MainState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IDoorVisualActivatorProvider doorVisualActivatorProvider, IDoorVisualEventsProvider doorVisualEventsProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _doorVisualActivatorProvider = doorVisualActivatorProvider;
        _doorVisualEventsProvider = doorVisualEventsProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE MAIN STATE</color>");

        _doorVisualEventsProvider.OnChooseDoor += ChangeStateToDoorMove;

        _doorVisualActivatorProvider.ActivateInteraction();
    }

    public void ExitState()
    {
        _doorVisualEventsProvider.OnChooseDoor -= ChangeStateToDoorMove;

        _doorVisualActivatorProvider.DeactivateInteraction();

        _sceneRoot.CloseMainPanel();
    }
    
    private void ChangeStateToDoorMove()
    {
        _machineProvider.SetState(_machineProvider.GetState<MoveDoorState_Game>());
    }
}
