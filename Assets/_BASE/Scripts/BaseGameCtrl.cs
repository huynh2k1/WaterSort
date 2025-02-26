using UnityEngine;

public abstract class BaseGameCtrl : MonoBehaviour
{
    private StateGame _curState;

    public void ChangeState(StateGame newState)
    {
        _curState = newState;
    }

    public abstract void GameHome();
    public abstract void GameStart();
    public abstract void GamePause(bool isPause);
    public abstract void GameWin();
    public abstract void GameLose();

    public StateGame CurState() => _curState;
}
public enum StateGame { HOME, PLAYING, PAUSE, WIN, LOSE}