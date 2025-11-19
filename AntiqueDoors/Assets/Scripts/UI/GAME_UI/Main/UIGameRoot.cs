using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameRoot : UIRoot
{
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private DoorsPanel_Game doorsPanel;
    [SerializeField] private DoorNothingPanel_Game doorNothingPanel;

    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
        doorsPanel.Initialize();
        doorNothingPanel.Initialize();
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
        CloseDoorNothingPanel();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        doorsPanel.Dispose();
        doorNothingPanel.Dispose();
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

    public void OpenDoorsPanel()
    {
        if(doorsPanel.IsActive) return;

        OpenOtherPanel(doorsPanel);
    }

    public void CloseDoorsPanel()
    {
        if (!doorsPanel.IsActive) return;

        CloseOtherPanel(doorsPanel);
    }




    public void OpenDoorNothingPanel()
    {
        if(doorNothingPanel.IsActive) return;

        OpenOtherPanel(doorNothingPanel);
    }

    public void CloseDoorNothingPanel()
    {
        if(!doorNothingPanel.IsActive) return;

        CloseOtherPanel(doorNothingPanel);
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
