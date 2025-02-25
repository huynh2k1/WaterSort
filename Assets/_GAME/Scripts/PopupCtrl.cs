using UnityEngine;

public class PopupCtrl : MonoBehaviour
{
    public static PopupCtrl I;
    public PopupSetting puSetting;

    private void Awake()
    {
        I = this;
    }

    public void ShowSetting()
    {
        puSetting.Show();
    }
}
