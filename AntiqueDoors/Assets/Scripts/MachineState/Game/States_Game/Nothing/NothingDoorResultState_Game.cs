using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NothingDoorResultState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IDoorCounterProvider _doorCounterProvider;

    private IEnumerator timer;

    public NothingDoorResultState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IDoorCounterProvider doorCounterProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _doorCounterProvider = doorCounterProvider;
    }

    public void EnterState()
    {
        _sceneRoot.OpenDoorNothingPanel();

        if(timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _doorCounterProvider.AddCount();
        _sceneRoot.CloseDoorNothingPanel();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);

        ChangeStateToStartMenu();
    }

    private void ChangeStateToStartMenu()
    {
        _machineProvider.SetState(_machineProvider.GetState<StartMainState_Game>());
    }
}
