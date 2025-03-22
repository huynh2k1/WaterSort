using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHome : BaseUI
{
    private Dictionary<Button, UnityAction> buttonActions;
    public Button btnPlay;

    private void Awake()
    {
        buttonActions = new Dictionary<Button, UnityAction>
        {
            { btnPlay, OnClickPlay},
        };

        foreach(var obj in buttonActions)
        {
            AssignOnClick(obj.Key, obj.Value);

        }
    }

    void AssignOnClick(Button btn, UnityAction action)
    {
        if (btn != null)
        {
            btn.onClick.AddListener(action);
        }
        else
        {
            Debug.LogError($"Button chưa được gán trong Inspector: {action.Method.Name}");
        }
    }

    void OnClickPlay()
    {
        UICtrl.I.ShowUIGame();
        GameCtrl.I.GameStart();
    }

    void OnClickSetting()
    {
        PopupCtrl.I.ShowSetting();
    }

}
