using UnityEngine;

public class UICtrl : MonoBehaviour
{
    public static UICtrl I;
    public UIHome uiHome;
    public UIGame uiGame;

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        ShowUIHome(true);
        ShowUIGame(false);
    }

    public void ShowUIHome(bool isShow)
    {
        if (isShow)
        {
            uiHome.Show();
        }
        else
        {
            uiHome.Hide();
        }
    }

    public void ShowUIGame(bool isShow)
    {
        if (isShow)
        {
            uiGame.Show();
        }
        else
        {
            uiGame.Hide();
        }
    }

}
