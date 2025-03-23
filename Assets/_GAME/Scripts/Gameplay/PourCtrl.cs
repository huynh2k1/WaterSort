using UnityEngine;

public class PourCtrl : MonoBehaviour
{
    [SerializeField] Spawner _spawner;
    [SerializeField] LineCtrl _lineCtrl;
    private void OnEnable()
    {
        InputCtrl.OnPourWater += StartPourWater;
    }

    private void OnDisable()
    {
        InputCtrl.OnPourWater -= StartPourWater;
    }

    public void StartPourWater(Bottle b1, Bottle b2) 
    {
        //Check xem có cùng màu nước trên cùng
        TypeWater t1 = b1.waters[b1.GetIDTopWater()];
        TypeWater t2 = TypeWater.NONE;

        if (b2.IsEmpty() == false)
            t2 = b2.waters[b2.GetIDTopWater()];

        if (t2 != TypeWater.NONE && t1 != t2)
        {
            b1.Selected(false);
            b1 = null;
            b1 = b2;
            b1.Selected(true);
            b2 = null;
            return;
            //ResetBothBottle();
            //return;
        }

        if (t1 == t2 || t2 == TypeWater.NONE)
        {
            //Kiểm tra b2 còn bao nhiêu slot trống - lấy từ b1 -> b2
            int num1 = b1.CountTopSameType();
            int num2 = b2.ReceivableAmount();

            int sum = Mathf.Min(num1, num2);
            float timeFill = b1.data.time / sum;

            b1.TweenBottle(b2, b1.idCurFill - sum, timeFill, () =>
            {
                b1.ReduceWater(sum, timeFill);
                _lineCtrl.SpawnLine(t1, b1, b2, timeFill);
                b2.ReFill(sum, timeFill, t1, () =>
                {
                    if (_spawner.IsAllBottleComplete())
                    {
                        GameCtrl.I.GameWin();
                    }
                });
            },
            () =>
            {
                InputCtrl.I.ResetBottlesSelected();
            });

        }
    }
}
