using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class OtherSceneEntryPoint : MonoBehaviour, ISceneEntryPoint
{
    [SerializeField] private UIOtherSceneRoot sceneRootPrefab;

    private ISceneNavigator _sceneNavigator;
    private UIOtherSceneRoot sceneRoot;
    private BankPresenter bankPresenter;
    private ViewContainer viewContainer;
    private FirebaseDatabasePresenter firebaseDatabasePresenter;
    private WebViewPresenter webViewPresenter;

    public void Initialize(ISceneNavigator sceneNavigator, UIRootView uIRootView)
    {

        _sceneNavigator = sceneNavigator;
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
        DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        sceneRoot = Instantiate(sceneRootPrefab);
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankPresenter.Initialize();

        webViewPresenter = new WebViewPresenter(new WebViewModel(), viewContainer.GetView<WebViewView>());
        webViewPresenter.Initialize();

        firebaseDatabasePresenter = new FirebaseDatabasePresenter(new FirebaseDatabaseModel(firebaseAuth, databaseReference, bankPresenter));
        firebaseDatabasePresenter.Initialize();

        ActivateActions();

        firebaseDatabasePresenter.GetLink();
    }

    private void ActivateActions()
    {
        firebaseDatabasePresenter.OnGetLink += GetURLBd;
        firebaseDatabasePresenter.OnErrorGetLink += GoToMainMenu;

        webViewPresenter.OnGetLinkFromTitle += GetUrl;
        webViewPresenter.OnFail += GoToMainMenu;
    }

    private void DeactivateActions()
    {
        firebaseDatabasePresenter.OnGetLink -= GetURLBd;
        firebaseDatabasePresenter.OnErrorGetLink -= GoToMainMenu;

        webViewPresenter.OnGetLinkFromTitle -= GetUrl;
        webViewPresenter.OnFail -= GoToMainMenu;
    }

    private void GetURLBd(string link)
    {
        webViewPresenter.GetLinkInTitleFromURL(link);
    }

    private void GetUrl(string URL)
    {
        if (URL == null)
        {
            GoToMainMenu();
            return;
        }

        webViewPresenter.SetURL(URL);
        webViewPresenter.Load();

        Debug.Log("SUCCESS");
    }

    public void Dispose()
    {
        DeactivateActions();

        webViewPresenter.Dispose();
    }

    private void GoToMainMenu()
    {
        _sceneNavigator.LoadScene(Scenes.MAIN_MENU, true);
    }
}
