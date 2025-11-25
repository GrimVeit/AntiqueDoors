using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IBankGameProvider _bankGameProvider;

    private IEnumerator timer;

    public LoseState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IBankGameProvider bankGameProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _bankGameProvider = bankGameProvider;
    }

    public void EnterState()
    {
        if (timer != null) Coroutines.Stop(timer);

        timer = Timer(4);
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer(float timeWait)
    {
        _sceneRoot.OpenLosePanel();

        yield return new WaitForSeconds(timeWait);

        _bankGameProvider.ApplyLoseBonus();
    }
}
