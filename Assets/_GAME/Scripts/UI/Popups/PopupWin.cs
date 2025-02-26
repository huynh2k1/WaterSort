using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupWin : Popup
{
    public Button btnNext;
    public Button btnReplay;

    public override void Awake()
    {
        base.Awake();
        AssignOnClick(btnNext, OnClickNext);
        AssignOnClick(btnReplay, OnClickReplay);
    }

    void AssignOnClick(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    void OnClickReplay()
    {
        if(PrefData.CurLevel > 0)
        {
            PrefData.CurLevel--;
        }
        Hide();
        GameCtrl.I.GameStart();
    }

    void OnClickNext()
    {
        Hide();
        GameCtrl.I.GameStart();
    }
}
