using DG.Tweening;
using System;
using UnityEngine;

public class GameCtrl : BaseGameCtrl
{
    public static GameCtrl I { get; private set; }

    public static event Action OnGameStart;

    private void Awake()
    {
        if(I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameHome();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameStart();
        }
    }

    public override void GameHome()
    {
        ChangeState(StateGame.HOME);
        UICtrl.I.Init();
        //BottleCtrl.I.Init();
    }

    public override void GameStart()
    {
        ChangeState(StateGame.PLAYING);
        OnGameStart?.Invoke();
    }

    public override void GamePause(bool isPause)
    {
        if (isPause)
            ChangeState(StateGame.PAUSE);
        else
            ChangeState(StateGame.PLAYING);
    }

    public override void GameWin()
    {
        ChangeState(StateGame.WIN);

        CheckLevelUp();
        DOVirtual.DelayedCall(1f, () =>
        {
            PopupCtrl.I.ShowWin();
        });
    }

    public override void GameLose()
    {
        ChangeState(StateGame.LOSE);
    }

    public void CheckLevelUp()
    {
        if(PrefData.CurLevel < 100)
        {
            PrefData.CurLevel++;
        }
        else
        {
            PrefData.CurLevel = 0;
        }
    }
}
