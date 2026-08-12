using System;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class MenuEntryPoint : MonoBehaviour, ISceneEntryPoint
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private ISceneNavigator _sceneNavigator;
    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private ParticleEffectMaterialPresenter particleEffectMaterialPresenter;
    private SoundPresenter soundPresenter;

    private NicknamePresenter nicknamePresenter;
    private AvatarPresenter avatarPresenter;
    private FirebaseAuthenticationPresenter firebaseAuthenticationPresenter;
    private FirebaseDatabasePresenter firebaseDatabasePresenter;
    private LeaderboardPresenter leaderboardPresenter;

    private AvatarVisualPresenter avatarVisualPresenter_Main;
    private AvatarVisualPresenter avatarVisualPresenter_Update;

    private ScoreLaurelPresenter scoreLaurelPresenter;
    private ScoreLaurelVisualPresenter scoreLaurelVisualPresenter;

    private StoreAdditionallyPresenter storeAdditionallyPresenter;
    private StoreHealthPresenter storeHealthPresenter;
    private StoreShopPresenter storeShopPresenter;
    private ShopVisualPresenter shopVisualPresenter;
    private ShopAnimationVisualPresenter shopAnimationVisualPresenter;

    private StateMachine_Menu stateMachine;

    public void Initialize(ISceneNavigator sceneNavigator, UIRootView uIRootView)
    {
        _sceneNavigator = sceneNavigator;

        sceneRoot = Instantiate(menuRootPrefab);

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
                FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
                DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                soundPresenter = new SoundPresenter
                    (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
                    viewContainer.GetView<SoundView>());

                particleEffectPresenter = new ParticleEffectPresenter
                    (new ParticleEffectModel(),
                    viewContainer.GetView<ParticleEffectView>());

                particleEffectMaterialPresenter = new ParticleEffectMaterialPresenter(new ParticleEffectMaterialModel(), viewContainer.GetView<ParticleEffectMaterialView>());

                bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());

                nicknamePresenter = new NicknamePresenter(new NicknameModel(PlayerPrefsKeys.NICKNAME, soundPresenter), viewContainer.GetView<NicknameView>());
                avatarPresenter = new AvatarPresenter(new AvatarModel(PlayerPrefsKeys.AVATAR), viewContainer.GetView<AvatarView>());
                firebaseAuthenticationPresenter = new FirebaseAuthenticationPresenter(new FirebaseAuthenticationModel(firebaseAuth, soundPresenter), viewContainer.GetView<FirebaseAuthenticationView>());

                firebaseDatabasePresenter = new FirebaseDatabasePresenter(new FirebaseDatabaseModel(firebaseAuth, databaseReference, bankPresenter));
                leaderboardPresenter = new LeaderboardPresenter(new LeaderboardModel(firebaseDatabasePresenter), viewContainer.GetView<LeaderboardView>());

                avatarVisualPresenter_Main = new AvatarVisualPresenter(new AvatarVisualModel(avatarPresenter, avatarPresenter, avatarPresenter, soundPresenter), viewContainer.GetView<AvatarVisualView>("Registration"));
                avatarVisualPresenter_Update = new AvatarVisualPresenter(new AvatarVisualModel(avatarPresenter, avatarPresenter, avatarPresenter, soundPresenter), viewContainer.GetView<AvatarVisualView>("Update"));

                scoreLaurelPresenter = new ScoreLaurelPresenter(new ScoreLaurelModel(PlayerPrefsKeys.SCORE_LAUREL));
                scoreLaurelVisualPresenter = new ScoreLaurelVisualPresenter(new ScoreLaurelVisualModel(scoreLaurelPresenter, scoreLaurelPresenter), viewContainer.GetView<ScoreLaurelVisualView>());

                storeAdditionallyPresenter = new StoreAdditionallyPresenter(new StoreAdditionallyModel(new List<string>
                {
                    PlayerPrefsKeys.SHOP_CONDITION_EVIL_TONGUE_START,
                    PlayerPrefsKeys.SHOP_CONDITION_EVIL_TONGUE_10_DOORS,
                    PlayerPrefsKeys.SHOP_CONDITION_ORACLE_START,
                    PlayerPrefsKeys.SHOP_CONDITION_ORACLE_10_DOORS
                }));

                storeHealthPresenter = new StoreHealthPresenter(new StoreHealthModel(PlayerPrefsKeys.MAX_HEALTH, PlayerPrefsKeys.MAX_SHIELD));
                storeShopPresenter = new StoreShopPresenter(new StoreShopModel(PlayerPrefsKeys.SHOP_LEVEL_SHIELD, PlayerPrefsKeys.SHOP_LEVEL_EVIL, PlayerPrefsKeys.SHOP_LEVEL_ORACLE));
                shopVisualPresenter = new ShopVisualPresenter(new ShopVisualModel(storeShopPresenter, bankPresenter, storeShopPresenter, storeHealthPresenter, storeAdditionallyPresenter, soundPresenter), viewContainer.GetView<ShopVisualView>());
                shopAnimationVisualPresenter = new ShopAnimationVisualPresenter(new ShopAnimationVisualModel(storeShopPresenter), viewContainer.GetView<ShopAnimationVisualView>());


                stateMachine = new StateMachine_Menu
                (sceneRoot,
                nicknamePresenter,
                avatarPresenter,
                firebaseAuthenticationPresenter,
                firebaseDatabasePresenter);

                sceneRoot.SetSoundProvider(soundPresenter);
                sceneRoot.Activate();

                ActivateTransitions();

                soundPresenter.Initialize();
                particleEffectPresenter.Initialize();
                particleEffectMaterialPresenter.Initialize();
                particleEffectMaterialPresenter.Activate();
                sceneRoot.Initialize();
                bankPresenter.Initialize();
                nicknamePresenter.Initialize();
                leaderboardPresenter.Initialize();
                firebaseAuthenticationPresenter.Initialize();
                firebaseDatabasePresenter.Initialize();

                avatarVisualPresenter_Main.Initialize();
                avatarVisualPresenter_Update.Initialize();
                scoreLaurelPresenter.Initialize();
                scoreLaurelVisualPresenter.Initialize();
                avatarPresenter.Initialize();
                shopAnimationVisualPresenter.Initialize();

                storeAdditionallyPresenter.Initialize();
                storeHealthPresenter.Initialize();
                shopVisualPresenter.Initialize();
                storeShopPresenter.Initialize();

                stateMachine.Initialize();
            }
            else
            {
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    private void ActivateTransitions()
    {
        sceneRoot.OnClickToPlay_Main += HandleClickToGame;
    }

    private void DeactivateTransitions()
    {
        sceneRoot.OnClickToPlay_Main -= HandleClickToGame;
    }

    public void Dispose()
    {
        DeactivateTransitions();

        particleEffectMaterialPresenter?.Deactivate();
        soundPresenter?.Dispose();
        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        particleEffectMaterialPresenter?.Dispose();
        bankPresenter?.Dispose();

        nicknamePresenter?.Dispose();
        leaderboardPresenter?.Dispose();
        firebaseAuthenticationPresenter?.Dispose();
        firebaseDatabasePresenter?.Dispose();

        avatarVisualPresenter_Main?.Dispose();
        avatarVisualPresenter_Update?.Dispose();
        scoreLaurelPresenter?.Dispose();
        scoreLaurelVisualPresenter?.Dispose();
        avatarPresenter?.Dispose();

        shopAnimationVisualPresenter?.Dispose();
        storeAdditionallyPresenter?.Dispose();
        storeHealthPresenter?.Dispose();
        shopVisualPresenter?.Dispose();
        storeShopPresenter?.Dispose();

        stateMachine?.Dispose();
    }

    #region Output

    private void HandleClickToGame()
    {
        _sceneNavigator.LoadScene(Scenes.GAME, true);
    }

    #endregion
}
