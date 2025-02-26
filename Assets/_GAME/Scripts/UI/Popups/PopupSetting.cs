using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : Popup
{
    public override void Hide()
    {
        base.Hide();
        GameCtrl.I.GamePause(false);
    }
}
