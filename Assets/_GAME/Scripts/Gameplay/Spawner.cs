using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Bottle bottlePrefab;
    public List<Bottle> listBottle = new List<Bottle>();

    [Header("LEVEL DATA")]
    public List<LevelSO> levelSOs;

    [Header("GRID CONFIG")]
    public Vector2 offSet = new Vector2(-0.25f, 0.5f);
    public int maxPerRow = 4;
    public float spaceX = 0.5f;
    public float spaceY = 0.5f;

    private void Start()
    {
        GameCtrl.OnGameStart += Init;
    }

    public void Init()
    {
        ResetAllBottles();
        SpawnObjects();
        LoadData();
    }

    void SpawnObjects()
    {
        int numBottle = levelSOs[PrefData.CurLevel].listBottle.Count;

        int numRow = (numBottle + maxPerRow - 1) / maxPerRow;

        float totalHeight = (numRow - 1) * spaceY;
        float totalWidth;
        float startX;
        float startY = -totalHeight / 2;
        float bottlePerRow;

        for (int i = 0; i < numRow; i++)
        {
            if (numBottle - i * maxPerRow >= 4)
            {
                bottlePerRow = 4;
                totalWidth = (maxPerRow - 1) * spaceX;
            }
            else
            {
                totalWidth = ((numBottle - i * maxPerRow) - 1) * spaceX;
                bottlePerRow = numBottle - i * maxPerRow;
            }
            startX = -totalWidth / 2;
            for (int j = 0; j < bottlePerRow; j++)
            {
                float x = startX + j * spaceX;
                float y = startY + i * spaceY;

                Vector2 pos = new Vector2(x + offSet.x, y + offSet.y);
                GameObject bottleObj = MyPoolManager.I.GetFromPool(bottlePrefab.gameObject);
                bottleObj.transform.position = pos;
                Bottle bottle = bottleObj.GetComponent<Bottle>();
                bottle.initPos = pos;
                listBottle.Add(bottle);
            }
        }
    }

    //Cập nhật dữ liệu & hiển thị của các bottle
    void LoadData()
    {
        for (int i = 0; i < listBottle.Count; i++)
        {
            TypeWater[] waters = levelSOs[PrefData.CurLevel].listBottle[i].waters;
            listBottle[i].LoadData(waters);
        }
    }

    public void ResetAllBottles()
    {
        if (listBottle.Count <= 0)
            return;
        for (int i = 0; i < listBottle.Count; i++)
        {
            listBottle[i].gameObject.SetActive(false);
        }
        listBottle.Clear();
    }

    public bool IsAllBottleComplete()
    {
        for (int i = 0; i < listBottle.Count; i++)
        {
            if (listBottle[i].IsBottleComplete() == false)
            {
                return false;
            }
        }
        return true;
    }
}
