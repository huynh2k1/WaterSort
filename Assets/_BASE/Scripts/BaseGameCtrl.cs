using UnityEngine;

public abstract class BaseGameCtrl : MonoBehaviour
{
    public StateGame curState;



    public abstract void GameHome();
    public abstract void GameStart();
    public abstract void GamePause();
    public abstract void GameWin();
    public abstract void GameLose();
}
public enum StateGame { HOME, PLAYING, PAUSE, COMPLETE}