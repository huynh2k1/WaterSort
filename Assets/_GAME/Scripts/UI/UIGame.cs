using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIGame : BaseUI
{
    public Button btnHome, btnSetting;
    public TMP_Text txtLevel;

    private void Awake()
    {
        AssignOnClick(btnHome, OnClickHome);
        AssignOnClick(btnSetting, OnClickSetting);
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
        Hide();
        UICtrl.I.ShowUIHome(true);
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
