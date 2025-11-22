using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDoorResultState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IDoorCounterProvider _doorCounterProvider;
    private readonly IDoorStateProvider _doorStateProvider;
    private readonly IBonusRewardProvider _bonusRewardProvider;
    private readonly IDoorVisualInfoProvider _doorVisualInfoProvider;

    private Door _currentDoor;
    private IEnumerator timer;

    public BonusDoorResultState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IDoorCounterProvider doorCounterProvider, IDoorStateProvider doorStateProvider, IBonusRewardProvider bonusRewardProvider, IDoorVisualInfoProvider doorVisualInfoProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _doorCounterProvider = doorCounterProvider;
        _doorStateProvider = doorStateProvider;
        _bonusRewardProvider = bonusRewardProvider;
        _doorVisualInfoProvider = doorVisualInfoProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE BONUS RESULT STATE</color>");

        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);

        _currentDoor = _doorVisualInfoProvider.GetCurrentDoor();
        _bonusRewardProvider.CreateBonuses(_currentDoor.BonusCount);
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
        _sceneRoot.OpenBonusRewardPanel();

        yield return new WaitForSeconds(2.2f);

        _sceneRoot.CloseDoorBonusPanel();

        yield return new WaitForSeconds(0.3f);

        ChangeStateToBonusReward();
    }

    private void ChangeStateToBonusReward()
    {
        _machineProvider.SetState(_machineProvider.GetState<BonusRewardState_Game>());
    }
}
