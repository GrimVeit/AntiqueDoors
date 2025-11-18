using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIGameRoot menuRootPrefab;

    private UIGameRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private ParticleEffectMaterialPresenter particleEffectMaterialPresenter;
    private SoundPresenter soundPresenter;
    private AvatarPresenter avatarPresenter;

    private DoorStatePresenter doorStatePresenter;
    private DoorCounterPresenter doorCounterPresenter;
    private StoreDoorPresenter storeDoorPresenter;
    private DoorDesignPresenter doorDesignPresenter;

    private StateMachine_Game stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = menuRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter
                    (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
                    viewContainer.GetView<SoundView>());

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());

        particleEffectMaterialPresenter = new ParticleEffectMaterialPresenter(new ParticleEffectMaterialModel(), viewContainer.GetView<ParticleEffectMaterialView>());

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        avatarPresenter = new AvatarPresenter(new AvatarModel(PlayerPrefsKeys.AVATAR), viewContainer.GetView<AvatarView>());

        doorStatePresenter = new DoorStatePresenter(new DoorStateModel(), viewContainer.GetView<DoorStateView>());
        doorCounterPresenter = new DoorCounterPresenter(new DoorCounterModel(), viewContainer.GetView<DoorCounterView>());
        storeDoorPresenter = new StoreDoorPresenter(new StoreDoorModel(doorCounterPresenter));
        doorDesignPresenter = new DoorDesignPresenter(new DoorDesignModel(storeDoorPresenter), viewContainer.GetView<DoorDesignView>());

        stateMachine = new StateMachine_Game(sceneRoot);

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();
        particleEffectMaterialPresenter.Initialize();
        particleEffectMaterialPresenter.Activate();
        sceneRoot.Initialize();
        bankPresenter.Initialize();
        avatarPresenter.Initialize();

        doorStatePresenter.Initialize();
        doorCounterPresenter.Initialize();
        storeDoorPresenter.Initialize();
        doorDesignPresenter.Initialize();

        stateMachine.Initialize();
    }

    private void ActivateEvents()
    {
        ActivateTransitions();
    }

    private void DeactivateEvents()
    {
        DeactivateTransitions();
    }

    private void ActivateTransitions()
    {
        sceneRoot.OnClickToExit_Main += HandleClickToMenu;
    }

    private void DeactivateTransitions()
    {
        sceneRoot.OnClickToExit_Main -= HandleClickToMenu;
    }

    private void Deactivate()
    {
        particleEffectMaterialPresenter.Deactivate();

        sceneRoot.Deactivate();
        soundPresenter?.Dispose();
    }

    private void Dispose()
    {
        DeactivateEvents();

        soundPresenter?.Dispose();
        sceneRoot.Dispose();
        particleEffectPresenter?.Dispose();
        particleEffectMaterialPresenter?.Dispose();
        bankPresenter?.Dispose();
        avatarPresenter?.Dispose();

        doorStatePresenter?.Dispose();
        doorCounterPresenter?.Dispose();
        storeDoorPresenter?.Dispose();
        doorDesignPresenter?.Dispose();

        stateMachine?.Dispose();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
            doorStatePresenter.ActivateAll();

        if (Input.GetKeyDown(KeyCode.RightAlt))
            doorStatePresenter.DeactivateAll();


        if (Input.GetKeyDown(KeyCode.Space))
            doorCounterPresenter.AddCount();

        if(Input.GetKeyDown(KeyCode.Escape))
            storeDoorPresenter.GenerateDoors();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Output


    public event Action OnClickToMenu;
    public event Action OnClickToGame;

    private void HandleClickToMenu()
    {
        Deactivate();

        OnClickToMenu?.Invoke();
    }

    private void HandleClickToGame()
    {
        Deactivate();

        OnClickToGame?.Invoke();
    }

    #endregion
}
