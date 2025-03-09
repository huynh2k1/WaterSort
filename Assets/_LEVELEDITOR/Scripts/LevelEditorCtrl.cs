using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class LevelEditorCtrl : MonoBehaviour
{
    public int numBottle;
    public List<Bottle> listBottle;
    public Bottle btlPrefab;
    public BottleSO data;
    string _filePath;

    [Header("Bottle To Shuffle")]
    public Bottle b1;
    public Bottle b2;

    [Header("PROPERTIES")]
    public float time;
    float numRow;
    float colPerRow;
    float startX;
    float startY;
    float totalWidth;
    float totalHeight;

    [Header("CONST")]
    public Vector2 offSet = new Vector2(-0.25f, 0.5f);
    public const int maxPerRow = 4;
    public const int maxPerCol = 4;
    public const float spaceX = 1f;
    public const float spaceY = 1.5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InitLevel();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            ShuffleBottles();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveToJson(ConvertToLevelData());
        }
    }

    public void InitLevel()
    {
        DisableBottles();
        SpawnBottles(numBottle);
        InitColor();
    }


    void DisableBottles()
    {
        if (listBottle.Count <= 0)
            return;
        for (int i = 0; i < listBottle.Count; i++)
        {
            listBottle[i].gameObject.SetActive(false);
        }
        listBottle.Clear();
    }

    public void ShuffleBottles()
    {
        b1 = GetRandomBottleNotNull();
        b2 = GetRandomBottleNotFull();

        int num1 = b1.CountTopSameType(); //Lấy ra số phần tử cùng kiểu của bottle 1
        int num2 = b2.ReceivableAmount(); //Số phần tử mà bottle 2 có thể chứa

        int numWaterShuffle = Random.Range(1, Mathf.Min(num1, num2));
        TypeWater type = b1.waters[b1.GetIDTopWater()];

        b1.ReduceWater(numWaterShuffle, 0);
        b2.ReFill(numWaterShuffle, 0, type, () => { });
    }

    //Tạo ra các bottle
    void SpawnBottles(int numBtl)
    {
        //Số lượng hàng = số bottle / số bottle tối đa trên 1 hàng
        numRow = (numBtl + maxPerRow - 1) / maxPerRow;

        //tổng chiều cao = số khoảng trống giữa các hàng * khoảng cách giữa các hàng
        totalHeight = (numRow - 1) * spaceY;

        //vị trí y bắt đầu = tổng chiều cao chia 2 lấy nửa dưới 
        startY = -totalHeight / 2;

        for(int i = 0; i < numRow; i++)
        {
            //Kiểm tra mỗi hàng có bao nhiêu cột
            if(numBtl - i * maxPerRow >= 4)
            {
                colPerRow = 4;
                //tổng chiều rộng = số khoảng trống giữa các bottle * khoảng cách giữa các bottle
                totalWidth = (maxPerRow - 1) * spaceX;
            }
            else
            {
                colPerRow = numBtl - i * maxPerRow;
                totalWidth = ((numBtl - i * maxPerRow) - 1) * spaceX;
            }

            startX = -totalWidth / 2;
            for(int j = 0; j < colPerRow; j++)
            {
                //Vị trí x của bottle = vị trí bắt đầu trục x + số khoảng trống giữa các cột
                float posX = startX + j * spaceX;
                //Vị trí y của bottle = vị trí bắt đầu theo trục y + số khoảng trống giữa các hàng
                float posY = startY + i * spaceY;

                Vector2 pos = new Vector2(posX + offSet.x, posY + offSet.y);
                GameObject bottle = MyPoolManager.I.GetFromPool(btlPrefab.gameObject);
                bottle.transform.position = pos;
                bottle.name = $"Bottle {i + j}";
                listBottle.Add(bottle.GetComponent<Bottle>());
            }
        }
    }

    //Khởi tạo color cho các bottle
    void InitColor()
    {
        int sumBotlle = listBottle.Count;
        int sumColor = data.colors.Length;
        int idBottleNull = Random.Range(0, listBottle.Count);
        for(int i = 0; i < sumBotlle; i++)
        {
            Bottle bottle = listBottle[i];
            int idColor = (i + 1 + sumColor) % sumColor;
            TypeWater type = (TypeWater)idColor;

            if(i == idBottleNull)
            {
                type = TypeWater.NONE;
            }

            for(int j = 0; j < listBottle[i].waters.Length; j++)
            {
                bottle.UpdateTypeWaterByID(j, type);
            }
            bottle.SetCurFillWater();
            bottle.UpdateColorWaterByThreshold();
        }
    }

    //Chọn ngẫu nhiên 1 bottle không rỗng
    Bottle GetRandomBottleNotNull()
    {
        List<Bottle> temp = new List<Bottle>();
        foreach(Bottle b in listBottle)
        {
            if(b.IsEmpty() == false)
            {
                temp.Add(b);
            }
        }

        int id = Random.Range(0, temp.Count);

        return temp[id];
    }

    //Chọn ngẫu nhiên 1 bottle không full
    Bottle GetRandomBottleNotFull()
    {
        List<Bottle> temp = new List<Bottle>();
        foreach (Bottle b in listBottle)
        {
            if (b.IsFull() == false && b != b1)
            {
                temp.Add(b);
            }
        }

        int id = Random.Range(0, temp.Count);

        return temp[id];
    }

    public LevelData ConvertToLevelData()
    {
        LevelData levelData = new LevelData();
        levelData.time = time;
        levelData.listBottle = new List<BottleData>();

        foreach (var bottle in listBottle)
        {
            levelData.listBottle.Add(new BottleData { waters = bottle.waters });
        }

        for (int i = 0; i < listBottle.Count; i++)
        {
            for(int j = 0; j  < listBottle[i].waters.Length; j++)
            {
                levelData.listBottle[i].waters[j] = listBottle[i].waters[j];
            }

        }
        return levelData;
    }

    void SaveToJson(LevelData level)
    {
        string pathSave = Path.Combine(Application.streamingAssetsPath, "Levels");

        if (!Directory.Exists(pathSave))
        {
            Directory.CreateDirectory(pathSave);
        }
        _filePath = Path.Combine(pathSave, $"Level {1}.json");
        string json = JsonUtility.ToJson(level, true);
        File.WriteAllText(_filePath, json);
    }
}
