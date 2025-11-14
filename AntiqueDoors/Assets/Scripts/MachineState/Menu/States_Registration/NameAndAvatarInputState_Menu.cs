using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameAndAvatarInputState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly NicknamePresenter _nicknamePresenter;
    private readonly FirebaseAuthenticationPresenter _firebaseAuthenticationPresenter;
    private readonly FirebaseDatabasePresenter _firebaseDatabasePresenter;

    public NameAndAvatarInputState_Menu(IGlobalStateMachineProvider globalStateMachineProvider, UIMainMenuRoot sceneRoot, NicknamePresenter nicknamePresenter, FirebaseAuthenticationPresenter firebaseAuthenticationPresenter, FirebaseDatabasePresenter firebaseDatabasePresenter)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _sceneRoot = sceneRoot;
        _nicknamePresenter = nicknamePresenter;
        _firebaseAuthenticationPresenter = firebaseAuthenticationPresenter;
        _firebaseDatabasePresenter = firebaseDatabasePresenter;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - TUTORIAL 02 STATE / MENU</color>");

        _nicknamePresenter.OnChooseNickname += _firebaseAuthenticationPresenter.SetNickname;
        _nicknamePresenter.OnChooseNickname += _firebaseDatabasePresenter.SetNickname;

        _sceneRoot.OnClickToRegistrate_Registration += ChangeStateToRegistration;

        _sceneRoot.OpenNicknamePanel();
        _sceneRoot.OpenRegistrationPanel();
    }

    public void ExitState()
    {
        _nicknamePresenter.OnChooseNickname -= _firebaseAuthenticationPresenter.SetNickname;
        _nicknamePresenter.OnChooseNickname -= _firebaseDatabasePresenter.SetNickname;

        _sceneRoot.OnClickToRegistrate_Registration -= ChangeStateToRegistration;

        _sceneRoot.CloseNicknamePanel();
        _sceneRoot.CloseRegistrationPanel();
    }

    private void ChangeStateToRegistration()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<RegistrationState_Menu>());
    }
}
