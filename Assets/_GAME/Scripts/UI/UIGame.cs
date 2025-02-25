using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIGame : BaseUI
{
    public Button btnHome, btnSetting;

    private void Awake()
    {
        AssignOnClick(btnHome, OnClickHome);
        AssignOnClick(btnSetting, OnClickSetting);
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
    }
}
