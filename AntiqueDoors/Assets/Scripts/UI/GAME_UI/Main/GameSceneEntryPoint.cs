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

    private VideoPresenter videoPresenter;
    private DoorStatePresenter doorStatePresenter;
    private DoorCounterPresenter doorCounterPresenter;
    private StoreDoorPresenter storeDoorPresenter;
    private DoorDesignPresenter doorDesignPresenter;
    private DoorVisualPresenter doorVisualPresenter;

    private StoreHealthPresenter storeHealthPresenter;
    private PlayerHealthPresenter playerHealthPresenter;

    private StoreBonusPresenter storeBonusPresenter;
    private BonusVisualPresenter bonusVisualPresenter;
    private StoreAdditionallyPresenter storeAdditionallyPresenter;
    private BonusConditionPresenter bonusConditionPresenter;
    private BonusRewardPresenter bonusRewardPresenter;

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

        videoPresenter = new VideoPresenter(new VideoModel(), viewContainer.GetView<VideoView>());
        doorStatePresenter = new DoorStatePresenter(viewContainer.GetView<DoorStateView>());
        doorCounterPresenter = new DoorCounterPresenter(new DoorCounterModel(), viewContainer.GetView<DoorCounterView>());
        storeDoorPresenter = new StoreDoorPresenter(new StoreDoorModel(doorCounterPresenter));
        doorDesignPresenter = new DoorDesignPresenter(new DoorDesignModel(storeDoorPresenter), viewContainer.GetView<DoorDesignView>());
        doorVisualPresenter = new DoorVisualPresenter(new DoorVisualModel(storeDoorPresenter), viewContainer.GetView<DoorVisualView>());

        storeHealthPresenter = new StoreHealthPresenter(new StoreHealthModel(PlayerPrefsKeys.MAX_HEALTH, PlayerPrefsKeys.MAX_SHIELD));
        playerHealthPresenter = new PlayerHealthPresenter(new PlayerHealthModel(storeHealthPresenter), viewContainer.GetView<PlayerHealthView>());

        storeBonusPresenter = new StoreBonusPresenter(new StoreBonusModel());
        bonusVisualPresenter = new BonusVisualPresenter(new BonusVisualModel(storeBonusPresenter), viewContainer.GetView<BonusVisualView>());
        storeAdditionallyPresenter = new StoreAdditionallyPresenter(new StoreAdditionallyModel(new List<string>
        {
            PlayerPrefsKeys.SHOP_CONDITION_EVIL_TONGUE_START,
            PlayerPrefsKeys.SHOP_CONDITION_EVIL_TONGUE_10_DOORS,
            PlayerPrefsKeys.SHOP_CONDITION_ORACLE_START,
            PlayerPrefsKeys.SHOP_CONDITION_ORACLE_10_DOORS
        }));

        bonusConditionPresenter = new BonusConditionPresenter(new BonusConditionModel(storeAdditionallyPresenter, doorCounterPresenter, storeBonusPresenter));
        bonusRewardPresenter = new BonusRewardPresenter(new BonusRewardModel(storeBonusPresenter, playerHealthPresenter), viewContainer.GetView<BonusRewardView>());

        stateMachine = new StateMachine_Game
            (sceneRoot,
            doorVisualPresenter,
            doorVisualPresenter,
            doorStatePresenter,
            doorStatePresenter,
            storeDoorPresenter,
            doorCounterPresenter,
            doorVisualPresenter,
            videoPresenter,
            playerHealthPresenter,
            bonusRewardPresenter,
            bonusRewardPresenter);

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

        videoPresenter.Initialize();
        doorStatePresenter.Initialize();
        doorCounterPresenter.Initialize();
        storeDoorPresenter.Initialize();
        doorDesignPresenter.Initialize();
        doorVisualPresenter.Initialize();

        storeHealthPresenter.Initialize();
        playerHealthPresenter.Initialize();

        bonusVisualPresenter.Initialize();
        storeBonusPresenter.Initialize();
        storeAdditionallyPresenter.Initialize();
        bonusConditionPresenter.Initialize();
        bonusRewardPresenter.Initialize();

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

        videoPresenter?.Dispose();
        doorStatePresenter?.Dispose();
        doorCounterPresenter?.Dispose();
        storeDoorPresenter?.Dispose();
        doorDesignPresenter?.Dispose();
        doorVisualPresenter?.Dispose();

        storeHealthPresenter?.Dispose();
        playerHealthPresenter?.Dispose();

        bonusVisualPresenter?.Dispose();
        storeBonusPresenter?.Dispose();
        storeAdditionallyPresenter?.Dispose();
        bonusConditionPresenter?.Dispose();
        bonusRewardPresenter?.Dispose();

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
