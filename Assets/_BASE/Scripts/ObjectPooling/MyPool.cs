using UnityEngine;
using System.Collections.Generic;
public class MyPool : MonoBehaviour
{
    Stack<GameObject> stack = new Stack<GameObject>();
    GameObject baseObj;
    GameObject tmp;
    private ReturnToPool returnPool;

    public MyPool(GameObject baseObj)
    {
        this.baseObj = baseObj;
    }

    public GameObject Get()
    {
        if(stack.Count > 0)
        {
            //Nếu có object trên scene thì lấy
            tmp = stack.Pop();
            tmp.SetActive(true);
            return tmp;
        }

        //Nếu chưa có thì sinh đối tượng mới
        tmp = GameObject.Instantiate(baseObj);
        returnPool = tmp.AddComponent<ReturnToPool>();
        returnPool.pool = this;
        return tmp;
    }

    public void AddToPool(GameObject obj)
    {
        stack.Push(obj);
    }
}
