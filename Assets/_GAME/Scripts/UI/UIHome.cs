using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHome : BaseUI
{
    public Button btnPlay, btnSetting;

    private void Awake()
    {
        AssignOnClick(btnPlay, OnClickPlay);
        AssignOnClick(btnSetting, OnClickSetting);
    }

    void AssignOnClick(Button btn, UnityAction action)
    {
        btn.onClick.AddListener(action);
    }

    void OnClickPlay()
    {
        Hide();
        UICtrl.I.ShowUIGame(true);
    }

    void OnClickSetting()
    {
        PopupCtrl.I.ShowSetting();
    }

}
