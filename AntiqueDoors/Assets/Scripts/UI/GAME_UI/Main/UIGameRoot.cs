using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameRoot : UIRoot
{
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private DoorsPanel_Game doorsPanel;

    [Header("Nothing Door")]
    [SerializeField] private DoorNothingPanel_Game doorNothingPanel;
    [SerializeField] private DoorNothingBackgroundPanel_Game doorNothingBackgroundPanel;

    [Header("Danger Door")]
    [SerializeField] private DoorDangerPanel_Game doorDangerPanel;

    [Header("Bonus Door")]
    [SerializeField] private DoorBonusPanel_Game doorBonusPanel;
    [SerializeField] private DoorBonusBackgroundPanel_Game doorBonusBackgroundPanel;

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
        doorNothingBackgroundPanel.Initialize();

        doorDangerPanel.Initialize();

        doorBonusPanel.Initialize();
        doorBonusBackgroundPanel.Initialize();
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
        CloseDoorNothingBaackgroundPanel();
        CloseDoorDangerPanel();
        CloseDoorBonusPanel();
        CloseDoorBonusBackgroundPanel();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        doorsPanel.Dispose();

        doorNothingPanel.Dispose();
        doorNothingBackgroundPanel.Dispose();

        doorDangerPanel.Dispose();

        doorBonusPanel.Dispose();
        doorBonusBackgroundPanel.Dispose();
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

    public void OpenDoorNothingBackgroundPanel()
    {
        if(doorNothingBackgroundPanel.IsActive) return;

        OpenOtherPanel(doorNothingBackgroundPanel);
    }

    public void CloseDoorNothingBaackgroundPanel()
    {
        if(!doorNothingBackgroundPanel.IsActive) return;

        CloseOtherPanel(doorNothingBackgroundPanel);
    }



    public void OpenDoorDangerPanel()
    {
        if(doorDangerPanel.IsActive) return;

        OpenOtherPanel(doorDangerPanel);
    }

    public void CloseDoorDangerPanel()
    {
        if(!doorDangerPanel.IsActive) return;

        CloseOtherPanel(doorDangerPanel);
    }



    public void OpenDoorBonusPanel()
    {
        if(doorBonusPanel.IsActive) return;

        OpenOtherPanel(doorBonusPanel);
    }

    public void CloseDoorBonusPanel()
    {
        if(!doorBonusPanel.IsActive) return;

        CloseOtherPanel(doorBonusPanel);
    }

    public void OpenDoorBonusBackgroundPanel()
    {
        if(doorBonusBackgroundPanel.IsActive) return;

        OpenOtherPanel(doorBonusBackgroundPanel);
    }

    public void CloseDoorBonusBackgroundPanel()
    {
        if (!doorBonusBackgroundPanel.IsActive) return;

        CloseOtherPanel(doorBonusBackgroundPanel);
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
