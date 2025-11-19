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
        _doorVisualEventsProvider.OnChooseDoor_Value += ChooseDoor;

        _doorVisualActivatorProvider.ActivateInteraction();
    }

    public void ExitState()
    {
        _doorVisualActivatorProvider.DeactivateInteraction();

        _sceneRoot.CloseMainPanel();
    }

    private void ChooseDoor(Door door)
    {
        if (door.HasDanger)
        {
            ChangeStateToBad();
        }
        else if (door.HasBonus)
        {
            ChangeStateToGood();
        }
        else if(!door.HasDanger && !door.HasBonus)
        {
            ChangeStateToNothing();
        }
    }

    private void ChangeStateToNothing()
    {
        _machineProvider.SetState(_machineProvider.GetState<NothingDoorResultState_Game>());
    }

    private void ChangeStateToGood()
    {
        _machineProvider.SetState(_machineProvider.GetState<NothingDoorResultState_Game>());
    }

    private void ChangeStateToBad()
    {
        _machineProvider.SetState(_machineProvider.GetState<NothingDoorResultState_Game>());
    } 
}
