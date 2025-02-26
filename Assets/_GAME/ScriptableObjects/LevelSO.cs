using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "SO/LevelSO", fileName = "LevelSO")]
public class LevelSO : ScriptableObject
{
    public float time;
    public List<BottleData> listBottle;
}

[System.Serializable]
public class BottleData
{
    public TypeWater[] waters = new TypeWater[4];
}
