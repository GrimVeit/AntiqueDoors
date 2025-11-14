using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameRoot : UIRoot
{
    [SerializeField] private MainPanel_Game mainPanel;

    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
    }

    public void Activate()
    {
        mainPanel.OnClickToExit += HandleClickToExit_Main;
    }

    public void Deactivate()
    {
        mainPanel.OnClickToExit -= HandleClickToExit_Main;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);

        CloseMainPanel();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
    }

    #region Input


    public void OpenMainPanel()
    {
        if(mainPanel.IsActive) return;

        OpenPanel(mainPanel);
    }

    public void CloseMainPanel()
    {
        if(!mainPanel.IsActive) return;

        CloseOtherPanel(mainPanel);
    }


    #endregion





    #region Output


    public event Action OnClickToExit_Main;

    private void HandleClickToExit_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToExit_Main?.Invoke();
    }


    #endregion
}
