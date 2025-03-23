using DG.Tweening;
using UnityEngine;

public class LineCtrl : MonoBehaviour
{
    public LineRenderer linePrefab;
    public BottleSO bottleSO;

    public void SpawnLine(TypeWater type, Bottle b1, Bottle b2, float time)
    {
        GameObject lineObj = MyPoolManager.I.GetFromPool(linePrefab.gameObject);
        LineRenderer line = lineObj.GetComponent<LineRenderer>();
        Color color = ConvertToColor.ConvertTypeToColor(type, bottleSO);
        line.SetColors(color, color);
        line.SetWidth(0.02f, 0.02f);
        while (line.positionCount < 1)
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
}
