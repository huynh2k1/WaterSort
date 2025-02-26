using UnityEngine;

public class PopupCtrl : MonoBehaviour
{
    public static PopupCtrl I;
    public PopupSetting puSetting;
    public PopupWin puWin;

    private void Awake()
    {
        I = this;
    }

    public void ShowSetting()
    {
        puSetting.Show();
    }

    public void ShowWin()
    {
        puWin.Show();   
    }
}
