using UnityEngine;

public class PopupController : MonoBehaviour
{
    public static PopupController I;
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
