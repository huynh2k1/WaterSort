using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHome : MonoBehaviour
{
    public Button btnPlay, btnSetting;

    private void Awake()
    {
        AssignOnClick(btnSetting, OnClickSetting);
    }

    void AssignOnClick(Button btn, UnityAction action)
    {
        btn.onClick.AddListener(action);
    }

    void OnClickPlay()
    {

    }

    void OnClickSetting()
    {
        PopupController.I.ShowSetting();
    }
}
