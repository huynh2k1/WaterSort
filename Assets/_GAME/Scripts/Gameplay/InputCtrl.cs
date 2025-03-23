using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using static UnityEditor.PlayerSettings;
using DG.Tweening;
using System;

public class InputCtrl : MonoBehaviour
{
    public static InputCtrl I;
    [SerializeField] Spawner _spawner;
    [SerializeField] PourCtrl _pourCtrl;
    public BottleSO bottleSO;

    public Bottle b1;
    public Bottle b2;

    public static Action<Bottle, Bottle> OnPourWater;

    private void Awake()
    {
        I = this;
    }

    public void SelectBottle(Bottle bottle)
    {
        if(b1 == null)
        {
            if (bottle.IsEmpty())
                return;
            b1 = bottle;
            b1.Selected(true);
        }
        else if(b2 == null)
        {
            if(b1 == bottle)
            {
                b1.Selected(false);
                b1 = null;
                return;
            }
            if (!bottle.IsFull())
            {
                b2 = bottle;
                OnPourWater?.Invoke(b1, b2);
            }
            else
            {
                b1.Selected(false);
                b1 = bottle;
                b1.Selected(true);
            }
        }
    }

    public void ResetBottlesSelected()
    {
        b1 = null;
        b2 = null;
    }
}
public class ConvertToColor
{
    public static Color ConvertTypeToColor(TypeWater type, BottleSO bottleSO)
    {
        Color color = new Color();
        switch (type)
        {
            case TypeWater.NONE:
                color = bottleSO.colors[0];
                break;
            case TypeWater.C0:
                color = bottleSO.colors[0];
                break;
            case TypeWater.C1:
                color = bottleSO.colors[1];
                break;
            case TypeWater.C2:
                color = bottleSO.colors[2];
                break;
            case TypeWater.C3:
                color = bottleSO.colors[3];
                break;
            case TypeWater.C4:
                color = bottleSO.colors[4];
                break;
            case TypeWater.C5:
                color = bottleSO.colors[5];
                break;
            case TypeWater.C6:
                color = bottleSO.colors[6];
                break;
            case TypeWater.C7:
                color = bottleSO.colors[7];
                break;
            case TypeWater.C8:
                color = bottleSO.colors[8];
                break;
            case TypeWater.C9:
                color = bottleSO.colors[9];
                break;
        }
        return color;
    }
}