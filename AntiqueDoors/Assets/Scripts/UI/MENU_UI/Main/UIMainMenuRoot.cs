using System;
using UnityEngine;

public class UIMainMenuRoot : UIRoot
{
    [SerializeField] private IntroPanel_Menu introPanel;
    [SerializeField] private MainPanel_Menu mainPanel;
    [SerializeField] private CoinsPanel_Menu coinsPanel;
    [SerializeField] private LeaderboardPanel_Menu leaderboardPanel;

    [Header("Registration")]
    [SerializeField] private NicknamePanel_Menu nicknamePanel;
    [SerializeField] private RegistrationPanel_Menu registrationPanel;
    [SerializeField] private LoadingPanel_Menu loadRegistrationPanel;

    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        introPanel.Initialize();
        mainPanel.Initialize();
        coinsPanel.Initialize();
        leaderboardPanel.Initialize();

        nicknamePanel.Initialize();
        registrationPanel.Initialize();
        loadRegistrationPanel.Initialize();
    }

    public void Activate()
    {
        registrationPanel.OnClickToRegistrate += HandleClickToRegistrate_Registration;

        leaderboardPanel.OnClickToBack += HandleClickToBack_Leaderboard;

        mainPanel.OnClickToLeaderboard += HandleClickToLeaderboard_Main;
        mainPanel.OnClickToWardrobe += HandleClickToWardrobe_Main;
        mainPanel.OnClickToPlay += HandleClickToPlay_Main;
        mainPanel.OnClickToExit += HandleClickToExit_Main;
    }


    public void Deactivate()
    {
        registrationPanel.OnClickToRegistrate -= HandleClickToRegistrate_Registration;

        leaderboardPanel.OnClickToBack -= HandleClickToBack_Leaderboard;

        mainPanel.OnClickToLeaderboard -= HandleClickToLeaderboard_Main;
        mainPanel.OnClickToWardrobe -= HandleClickToWardrobe_Main;
        mainPanel.OnClickToPlay -= HandleClickToPlay_Main;
        mainPanel.OnClickToExit -= HandleClickToExit_Main;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);

        CloseCoinsPanel();
        CloseIntroPanel();
        CloseLeaderboardPanel();
        CloseLoadRegistrationPanel();
        CloseMainPanel();
        CloseNicknamePanel();
        CloseRegistrationPanel();
    }

    public void Dispose()
    {
        introPanel.Dispose();
        mainPanel.Dispose();
        coinsPanel.Dispose();
        leaderboardPanel.Dispose();

        nicknamePanel.Dispose();
        registrationPanel.Dispose();
        loadRegistrationPanel.Dispose();
    }

    #region OTHERS

    public void OpenMainPanel()
    {
        if (mainPanel.IsActive) return;

        OpenOtherPanel(mainPanel);
    }

    public void CloseMainPanel()
    {
        if (!mainPanel.IsActive) return;

        CloseOtherPanel(mainPanel);
    }


    public void OpenCoinsPanel()
    {
        if(coinsPanel.IsActive) return;

        OpenOtherPanel(coinsPanel);
    }

    public void CloseCoinsPanel()
    {
        if (!coinsPanel.IsActive) return;

        CloseOtherPanel(coinsPanel);
    }




    public void OpenLeaderboardPanel()
    {
        if (leaderboardPanel.IsActive) return;

        OpenOtherPanel(leaderboardPanel);
    }

    public void CloseLeaderboardPanel()
    {
        if (!leaderboardPanel.IsActive) return;

        CloseOtherPanel(leaderboardPanel);
    }




    public void OpenNicknamePanel()
    {
        if (nicknamePanel.IsActive) return;

        OpenOtherPanel(nicknamePanel);
    }

    public void CloseNicknamePanel()
    {
        if (!nicknamePanel.IsActive) return;

        CloseOtherPanel(nicknamePanel);
    }



    public void OpenRegistrationPanel()
    {
        if (registrationPanel.IsActive) return;

        OpenOtherPanel(registrationPanel);
    }

    public void CloseRegistrationPanel()
    {
        if (!registrationPanel.IsActive) return;

        CloseOtherPanel(registrationPanel);
    }


    public void OpenLoadRegistrationPanel()
    {
        if (loadRegistrationPanel.IsActive) return;

        OpenOtherPanel(loadRegistrationPanel);
    }

    public void CloseLoadRegistrationPanel()
    {
        if (!loadRegistrationPanel.IsActive) return;

        CloseOtherPanel(loadRegistrationPanel);
    }



    public void OpenIntroPanel()
    {
        if (introPanel.IsActive) return;

        OpenOtherPanel(introPanel);
    }

    public void CloseIntroPanel()
    {
        if (!introPanel.IsActive) return;

        CloseOtherPanel(introPanel);
    }

    #endregion


    #region Output

    #region OTHER

    public event Action OnClickToRegistrate_Registration;

    private void HandleClickToRegistrate_Registration()
    {
        OnClickToRegistrate_Registration?.Invoke();
    }

    #endregion

    #region MainPanel

    public event Action OnClickToLeaderboard_Main;
    public event Action OnClickToWardrobe_Main;
    public event Action OnClickToPlay_Main;
    public event Action OnClickToExit_Main;

    private void HandleClickToLeaderboard_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToLeaderboard_Main?.Invoke();
    }

    private void HandleClickToWardrobe_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToWardrobe_Main?.Invoke();
    }

    private void HandleClickToPlay_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToPlay_Main?.Invoke();
    }

    private void HandleClickToExit_Main()
    {
        OnClickToExit_Main?.Invoke();
    }

    #endregion

    #region LeaderboardPanel

    public event Action OnClickToBack_Leaderboard;

    private void HandleClickToBack_Leaderboard()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBack_Leaderboard?.Invoke();
    }

    #endregion

    #endregion

}
