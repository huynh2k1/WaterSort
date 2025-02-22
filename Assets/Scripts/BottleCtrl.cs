using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class BottleCtrl : MonoBehaviour
{
    public static BottleCtrl I;

    public List<Bottle> listBottle;

    public Bottle b1;
    public Bottle b2;

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        listBottle = transform.GetComponentsInChildren<Bottle>().ToList();
    }

    public void B1ToB2()
    {
        //Check xem có cùng màu nước trên cùng
        TypeWater t1 = b1.waters[b1.GetIDHasWater()];
        TypeWater t2 = TypeWater.NONE;

        if(b2.IsNull() == false)
            t2 = b2.waters[b2.GetIDHasWater()];

        if (t2 != TypeWater.NONE && t1 != t2)
        {
            ResetBothBottle();
            return;
        }

        if(t1 == t2 || t2 == TypeWater.NONE)
        {
            if(b1.GetPos().x < b2.GetPos().x)
            {
                //b1 nằm bên trái
                b1.FlipPivot(true);
            }
            else
            {
                //b1 nằm bên phải
                b1.FlipPivot(false);
            }

            //Kiểm tra b2 còn bao nhiêu slot trống - lấy từ b1 -> b2
            int num1 = b1.CountTopSameType();
            int num2 = b2.ReceivableAmount();

            int sum = Mathf.Min(num1, num2);
            float timeFill = b1.data.time / sum;

            b1.TweenBottle(b2, b1.idCurFill - sum, timeFill, () =>
            {
                b1.ReduceWater(sum, timeFill);
                b2.ReFill(sum, timeFill,t1, () =>
                {
                    if (IsWin())
                    {
                        Debug.Log("YOU WIN");
                    }
                });
            },
            () =>
            {
                if (b1.GetPos().x < b2.GetPos().x)
                {
                    //b1 nằm bên trái
                    b1.ResetFlip(true);
                }
                else
                {
                    //b1 nằm bên phải
                    b1.ResetFlip(false);
                }
                ResetBothBottle();
            });

        }

    }

    public bool IsWin()
    {
        for(int i = 0; i < listBottle.Count; i++)
        {
            if (listBottle[i].IsBottleComplete() == false)
            {
                return false;
            }
        }
        return true;
    }

    void ResetBothBottle()
    {
        b1 = null;
        b2 = null;
    }

}
