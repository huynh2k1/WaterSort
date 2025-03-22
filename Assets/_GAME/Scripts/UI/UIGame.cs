using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIGame : BaseUI
{
    private Dictionary<Button, UnityAction> buttonActions;
    public Button btnHome;
    public TMP_Text txtLevel;

    private void Awake()
    {
        buttonActions = new Dictionary<Button, UnityAction>
        {
            { btnHome, OnClickHome},
        };
        foreach(var btn in buttonActions)
        {
            AssignOnClick(btn.Key, btn.Value);
        }
    }

    private void OnEnable()
    {
        UpdateTxtLevel();
    }

    void AssignOnClick(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    void OnClickHome()
    {
        UICtrl.I.ShowUIHome();
    }

    void OnClickSetting()
    {
        PopupCtrl.I.ShowSetting();
        GameCtrl.I.GamePause(true);
    }

    void UpdateTxtLevel()
    {
        txtLevel.text = $"LEVEL {PrefData.CurLevel + 1}";
    }
}
