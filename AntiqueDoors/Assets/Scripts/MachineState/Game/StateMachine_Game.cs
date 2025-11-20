using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Game : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Game(
        UIGameRoot sceneRoot,
        IDoorVisualActivatorProvider doorVisualActivatorProvider,
        IDoorVisualEventsProvider doorVisualEventsProvider,
        IDoorStateProvider doorStateProvider,
        IDoorStateEventsProvider doorStateEventsProvider,
        IStoreDoorProvider storeDoorProvider,
        IDoorCounterProvider doorCounterProvider,
        IDoorVisualInfoProvider doorVisualInfoProvider,
        IVideoProvider videoProvider)
    {
        states[typeof(StartMainState_Game)] = new StartMainState_Game(this, sceneRoot, doorStateProvider, doorStateEventsProvider, storeDoorProvider);
        states[typeof(MainState_Game)] = new MainState_Game(this, sceneRoot, doorVisualActivatorProvider, doorVisualEventsProvider);
        states[typeof(MoveDoorState_Game)] = new MoveDoorState_Game(this, doorVisualInfoProvider, doorStateProvider, doorStateEventsProvider, videoProvider);

        states[typeof(NothingDoorResultState_Game)] = new NothingDoorResultState_Game(this, sceneRoot, doorCounterProvider, doorStateProvider);
        states[typeof(DangerDoorResultState_Game)] = new DangerDoorResultState_Game(this, sceneRoot, doorCounterProvider, doorStateProvider, videoProvider, doorVisualInfoProvider);
        states[typeof(BonusDoorResultState_Game)] = new BonusDoorResultState_Game(this, sceneRoot, doorCounterProvider, doorStateProvider);
    }

    public void Initialize()
    {
        SetState(GetState<StartMainState_Game>());
    }

    public void Dispose()
    {

    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }

    public void SetState(IState state)
    {
        _currentState?.ExitState();

        _currentState = state;
        _currentState.EnterState();
    }
}
