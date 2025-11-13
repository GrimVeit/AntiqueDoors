using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;

    private readonly UIMainMenuRoot _sceneRoot;

    private IEnumerator timer;

    public IntroState_Menu(IGlobalStateMachineProvider globalStateMachineProvider, UIMainMenuRoot sceneRoot)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OpenIntroPanel();

        if (timer != null ) Coroutines.Stop(timer);

        timer = Timer(1);
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _sceneRoot.CloseIntroPanel();
    }

    private IEnumerator Timer(float sec)
    {
        yield return new WaitForSeconds(sec);

        ChangeStateToCheckAuthorization();
    }

    private void ChangeStateToCheckAuthorization()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<CheckAuthorizationState_Menu>());
    }
}
