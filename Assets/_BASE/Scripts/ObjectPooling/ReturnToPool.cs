using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    public MyPool pool;
    private void OnDisable()
    {
        pool.AddToPool(gameObject);
    }
}
