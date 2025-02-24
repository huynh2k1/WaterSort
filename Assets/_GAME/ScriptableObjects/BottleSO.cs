using UnityEngine;

[CreateAssetMenu(menuName = "SO/BottleSO", fileName = "BottleSO")]
public class BottleSO : ScriptableObject
{
    //0 : 0.34
    //30 : 0.34 - 0.95
    //56 : 0.13 - 0.75 
    //71 : -0.08 - 0.6
    //83 : -0.29 - 0.5
    //90 : -0.5
    public float time = 2f;
    public float[] scaleOffsets = 
    {
        0.5f,
        0.6f,
        0.75f,
        0.95f,
    };
    public float[] thresholdFills = new float[5] 
    { 
        -0.5f,
        -0.29f,
        -0.07f,
        0.13f,
        0.34f
    };
    public Vector3[] rotateFills = new Vector3[5]
    {
        new Vector3(0, 0, 90),
        new Vector3(0, 0, 83),
        new Vector3(0, 0, 71),
        new Vector3(0, 0, 54),
        new Vector3(0, 0, 30),
    };
    public Color[] colors;

}
