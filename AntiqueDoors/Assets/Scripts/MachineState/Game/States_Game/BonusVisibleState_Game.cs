using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusVisibleState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly IBonusApplierProvider _bonusApplierProvider;
    private readonly UIGameRoot _sceneRoot;

    private IEnumerator timer;

    public BonusVisibleState_Game(IGlobalStateMachineProvider machineProvider, IBonusApplierProvider bonusApplierProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _bonusApplierProvider = bonusApplierProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE BONUS VISIBLE STATE</color>");

        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);

        _bonusApplierProvider.ApplyBonus();
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _sceneRoot.OpenFooterPanel();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1.2f);

        ChangeStateToMainMenu();
    }

    private void ChangeStateToMainMenu()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Game>());
    }
}
