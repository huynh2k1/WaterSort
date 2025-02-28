using System.Collections.Generic;
using UnityEngine;

public class MyPoolManager : MonoBehaviour
{
    public static MyPoolManager I;
    private Dictionary<GameObject, MyPool> dicPools = new Dictionary<GameObject, MyPool>();


    private void Awake()
    {
        I = this;
    }

    public GameObject GetFromPool(GameObject obj)
    {
        if (dicPools.ContainsKey(obj) == false)
        {
            dicPools.Add(obj, new MyPool(obj));
        }
        return dicPools[obj].Get();
    }
}
