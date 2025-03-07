using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 1)]
public class GameConfigSO : ScriptableObject
{
    public static GameConfigSO I;
    [Header("Level Settings")]
    [SerializeField] private int maxLevel = 100; //Mức level tối đa
    public int MaxLevel => maxLevel;

    private void OnEnable()
    {
        I = this;
    }

}
