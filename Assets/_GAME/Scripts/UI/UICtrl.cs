using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UICtrl : MonoBehaviour
{
    public static UICtrl I;
    [SerializeField] UIHome uiHome;
    [SerializeField] UIGame uiGame;
    [SerializeField] Image _mask;
    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        ShowUIHome(true);
        ShowUIGame(false);
    }

    public void ShowUIHome()
    {
        FadeMask(() =>
        {
            ShowUIHome(true);
            ShowUIGame(false);
        });
    }

    public void ShowUIGame()
    {
        FadeMask(() =>
        {
            ShowUIHome(false);
            ShowUIGame(true);
        });
    }

    void ShowUIHome(bool isShow)
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

    void ShowUIGame(bool isShow)
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

    public void FadeMask(Action action = default)
    {
        _mask.raycastTarget = false;
        TweenUtils.FadeImage(_mask, 0, 1, 0.2f, Ease.Linear, () =>
        {
            action?.Invoke();
            TweenUtils.FadeImage(_mask, 1, 0, 0.2f, Ease.Linear, () =>
            {
                _mask.raycastTarget = true;
            });
        });
    }
}
