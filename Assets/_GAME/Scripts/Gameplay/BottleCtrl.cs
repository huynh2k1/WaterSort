using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using static UnityEditor.PlayerSettings;
using DG.Tweening;

public class BottleCtrl : MonoBehaviour
{
    public static BottleCtrl I;

    public BottleSO bottleSO;
    public Bottle bottlePrefab;
    public List<Bottle> listBottle = new List<Bottle>();
    [Header("LEVEL DATA")]
    public List<LevelSO> levelSOs;

    [Header("GRID CONFIG")]
    public Vector2 offSet = new Vector2(-0.25f, 0.5f);
    public int maxPerRow = 4;
    public float spaceX = 0.5f;
    public float spaceY = 0.5f;

    public Bottle b1;
    public Bottle b2;

    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        SpawnObjects();
        LoadData();
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
                SpawnLine(t1, b1, b2, timeFill);
                b2.ReFill(sum, timeFill,t1, () =>
                {
                    if (IsWin())
                    {
                        GameCtrl.I.GameWin();
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

    #region SPAWN BOTTLE
    void SpawnObjects()
    {
        int numBottle = levelSOs[PrefData.CurLevel].listBottle.Count;

        int numRow = (numBottle + maxPerRow)/ maxPerRow;

        float totalHeight = (numRow - 1) * spaceY;
        float totalWidth;
        float startX;
        float startY = -totalHeight / 2;
        float bottlePerRow;
        for (int i = 0; i < numRow; i++)
        {
            if(numBottle - i * maxPerRow >= 4)
            {
                bottlePerRow = 4;
                totalWidth = (maxPerRow - 1) * spaceX;
                startX = -totalWidth / 2;
            }
            else
            {
                totalWidth = ((numBottle - i * maxPerRow) - 1) * spaceX;
                bottlePerRow = numBottle - i * maxPerRow;
                startX = -totalWidth / 2;
            }
            for(int j = 0; j < bottlePerRow; j++)
            {
                float x = startX + j * spaceX;
                float y = startY + i * spaceY;

                Vector2 pos = new Vector2(x + offSet.x, y + offSet.y);
                GameObject bottle = MyPoolManager.I.GetFromPool(bottlePrefab.gameObject);
                bottle.transform.position = pos;
                listBottle.Add(bottle.GetComponent<Bottle>());
            }
        }
    }

    void LoadData()
    {
        for(int i = 0; i < listBottle.Count; i++)
        {
            TypeWater[] waters = levelSOs[PrefData.CurLevel].listBottle[i].waters;
            listBottle[i].LoadData(waters);
        }
    }

    public void DisableBottles()
    {
        if (listBottle.Count <= 0)
            return;
        for(int i = 0; i < listBottle.Count; i++)
        {
            listBottle[i].gameObject.SetActive(false);
        }
        listBottle.Clear();
    }

    public LineRenderer lineRenderer;

    public void SpawnLine(TypeWater type, Bottle b1, Bottle b2, float time)
    {
        GameObject lineObj = MyPoolManager.I.GetFromPool(lineRenderer.gameObject);
        LineRenderer line = lineObj.GetComponent<LineRenderer>();
        Color color = ConvertTypeToColor(type);
        line.SetColors(color, color);
        line.SetWidth(0.02f, 0.02f);
        while(line.positionCount < 1)
        {
            line.positionCount++;
        }
        line.SetPosition(0, b1.GetPos());
        line.SetPosition(1, b1.GetPos());

        DOTween.To(() => b1.GetPos(), x => line.SetPosition(1, x), b2.GetPosBottom(), 0.02f).OnComplete(() =>
        {
            DOTween.To(() => b1.GetPos(), x => line.SetPosition(0, x), b2.GetPosBottom(), 0f).SetDelay(time).OnComplete(() =>
            {
                lineObj.SetActive(false);
            });
        });
    }

    public Color ConvertTypeToColor(TypeWater type)
    {
        Color color = new Color();
        switch (type)
        {
            case TypeWater.NONE:
                color = bottleSO.colors[0];
                break;
            case TypeWater.RED:
                color = bottleSO.colors[0];
                break;
            case TypeWater.GREEN:
                color = bottleSO.colors[1];
                break;
            case TypeWater.BLUE:
                color = bottleSO.colors[2];
                break;
            case TypeWater.YELLOW:
                color = bottleSO.colors[3];
                break;
            case TypeWater.PURPLE:
                color = bottleSO.colors[4];
                break;
            case TypeWater.ORANGE:
                color = bottleSO.colors[5];
                break;
        }
        return color;
    }
    #endregion
}
