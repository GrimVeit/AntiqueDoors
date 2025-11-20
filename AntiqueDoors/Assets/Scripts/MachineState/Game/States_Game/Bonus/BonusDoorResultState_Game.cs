using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDoorResultState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IDoorCounterProvider _doorCounterProvider;
    private readonly IDoorStateProvider _doorStateProvider;

    private IEnumerator timer;

    public BonusDoorResultState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IDoorCounterProvider doorCounterProvider, IDoorStateProvider doorStateProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _doorCounterProvider = doorCounterProvider;
        _doorStateProvider = doorStateProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE BONUS RESULT STATE</color>");

        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _doorCounterProvider.AddCount();
        _doorStateProvider.Hide();

        _sceneRoot.CloseDoorBonusBackgroundPanel();
    }

    private IEnumerator Timer()
    {
        _sceneRoot.OpenDoorBonusBackgroundPanel();

        yield return new WaitForSeconds(0.5f);

        _sceneRoot.OpenDoorBonusPanel();

        yield return new WaitForSeconds(2.2f);

        _sceneRoot.CloseDoorBonusPanel();

        yield return new WaitForSeconds(0.3f);

        ChangeStateToStartMenu();
    }

    private void ChangeStateToStartMenu()
    {
        _machineProvider.SetState(_machineProvider.GetState<StartMainState_Game>());
    }
}
