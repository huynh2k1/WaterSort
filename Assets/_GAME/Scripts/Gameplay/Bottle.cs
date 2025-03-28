﻿using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bottle : MonoBehaviour, IBottle
{
    public Stack<TypeWater> typeWaters;
    public TypeWater[] waters;
    public BottleSO data;

    [Header("REFERENCE")]
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] Transform _bottleOut;
    [SerializeField] Transform _mask;
    [SerializeField] Transform _posTarget;
    [SerializeField] Transform _posBottom;
    [SerializeField] SpriteRenderer _bottleIn;
    [SerializeField] AnimationCurve _animationCurve;

    [Header("PRIVATE")]
    private Material _waterMat;

    [Header("PROPERTIES")]
    public int idCurFill;
    public Vector3 initPos { get; set; }
    float _scaleOffset = 1f;
    float _curScale;
    bool _canClick = true;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        UpdateMat();
    }

    private void OnMouseDown()
    {
        if (GameCtrl.I != null && GameCtrl.I.CurState() != StateGame.PLAYING)
            return;

        if (!_canClick) return;
        InputCtrl.I.SelectBottle(this);
    }

    public void Selected(bool isSelected)
    {
        if (isSelected)
        {
            float targetY = transform.position.y + 0.5f;
            TweenUtils.MoveY(transform, targetY, 0.2f, Ease.Linear);
        }
        else
        {
            TweenUtils.Move(transform, initPos, 0.2f, Ease.Linear);
        }
    }

    public int GetIDTopWater()
    {
        for (int i = waters.Length - 1; i >= 0; i--)
        {
            if (waters[i] != TypeWater.NONE)
            {
                return i;
            }
        }
        return -1;
    }

    public int GetIDTopEmpty()
    {
        for (int i = 0; i < waters.Length; i++)
        {
            if (waters[i] == TypeWater.NONE)
            {
                return i;
            }
        }
        return waters.Length;
    }

    //Kiểm tra lọ đã đầy hay chưa
    public bool IsFull()
    {
        if (GetIDTopWater() < waters.Length - 1)
        {
            return false;
        }
        return true;
    }

    //Kiểm tra lọ có phải là lọ rỗng
    public bool IsEmpty()
    {
        if (GetIDTopEmpty() == 0)
        {
            return true;
        }
        return false;
    }

    public Vector2 GetPos() => transform.position;

    public Vector2 GetPosTarget() => _posTarget.position;

    public float GetScaleX() => transform.localScale.x;

    public Vector2 GetPosBottom() => _posBottom.position;

    public TypeWater GetTopType()
    {
        throw new NotImplementedException();
    }

    void Init()
    {
        if (_waterMat == null)
        {
            _waterMat = _bottleIn.material;
        }
    }

    public void LoadData(TypeWater[] data)
    {
        UpdateTypeAllWaters(data);
        _bottleIn.gameObject.SetActive(true);
        UpdateTypeAllWaters(data);
        SetCurFillWater();
        UpdateColorWaterByThreshold();
    }

    public void UpdateTypeAllWaters(TypeWater[] data)
    {
        for(int i = 0; i < data.Length; i++)
        {
            UpdateTypeWaterByID(i, data[i]);
        }
    }

    public void UpdateTypeWaterByID(int id, TypeWater type)
    {
        waters[id] = type;
    }

    public int CountTopSameType()
    {
        int count = 0;
        int topID = GetIDTopWater();
        TypeWater typeTop = waters[topID];

        for (int i = topID; i >= 0; i--)
        {
            if (waters[i] == typeTop)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        return count;
    }

    public int ReceivableAmount()
    {
        if (IsEmpty()) return waters.Length;

        int idTop = GetIDTopEmpty();
        return waters.Length - idTop;
    }

    //Cập nhật pos và scale của mask 
    void UpdateMat()
    {
        _waterMat.SetVector("_PosWorld", _mask.position);
        _waterMat.SetVector("_ObjectScale", transform.localScale);
    }

    public bool IsBottleComplete()
    {
        TypeWater typeTop = waters[waters.Length - 1];

        for (int i = waters.Length - 2; i >= 0; i--)
        {
            if (waters[i] != typeTop)
            {
                return false;
            }
        }
        return true;
    }

    public void UpdateColorWaterByThreshold()
    {
        for (int i = 0; i < idCurFill; i++)
        {
            switch (waters[i])
            {
                case TypeWater.NONE:
                    break;
                case TypeWater.C0:
                    SetColorWaterById(i, data.colors[0]);
                    break;
                case TypeWater.C1:
                    SetColorWaterById(i, data.colors[1]);
                    break;
                case TypeWater.C2:
                    SetColorWaterById(i, data.colors[2]);
                    break;
                case TypeWater.C3:
                    SetColorWaterById(i, data.colors[3]);
                    break;
                case TypeWater.C4:
                    SetColorWaterById(i, data.colors[4]);
                    break;
                case TypeWater.C5:
                    SetColorWaterById(i, data.colors[5]);
                    break;
                case TypeWater.C6:
                    SetColorWaterById(i, data.colors[6]);
                    break;
                case TypeWater.C7:
                    SetColorWaterById(i, data.colors[7]);
                    break;
                case TypeWater.C8:
                    SetColorWaterById(i, data.colors[8]);
                    break;
                case TypeWater.C9:
                    SetColorWaterById(i, data.colors[9]);
                    break;
            }
        }
    }

    void SetColorWaterById(int id, Color color)
    {
        _waterMat.SetColor($"_Color{id + 1}", color);
    }

    public void SetCurFillWater()
    {
        idCurFill = GetIDTopEmpty();
        _waterMat.SetFloat("_FillAmount", data.thresholdFills[idCurFill]);
    }

    public void ReFill(int numReFill, float time, TypeWater type, Action actionDone)
    {
        if (IsEmpty())
        {
            _bottleIn.gameObject.SetActive(true);
        }
        int sumFill = idCurFill + numReFill;
        for (int i = idCurFill; i < sumFill; i++)
        {
            waters[i] = type;
        }
        idCurFill = sumFill;
        UpdateColorWaterByThreshold();

        float prevFill = data.thresholdFills[idCurFill];
        float curFill = data.thresholdFills[sumFill];
        DOTween.To(() => prevFill, x => _waterMat.SetFloat("_FillAmount", x), curFill, time)
            .OnComplete(() =>
            {
                idCurFill = sumFill;
                _waterMat.SetFloat("_FillAmount", data.thresholdFills[idCurFill]);
                actionDone?.Invoke();
            });
    }

    public void ReduceWater(int numReduce, float time)
    {
        int curFill = idCurFill - numReduce;
        for (int i = idCurFill - 1; i >= curFill; i--)
        {
            waters[i] = TypeWater.NONE;
        }

        float prevFill = data.thresholdFills[idCurFill];
        float nextFill = data.thresholdFills[curFill];
        DOTween.To(() => prevFill, x => _waterMat.SetFloat("_FillAmount", x), nextFill, time)
            .OnComplete(() =>
            {
                idCurFill = curFill;
                _waterMat.SetFloat("_FillAmount", data.thresholdFills[idCurFill]);
                if (IsEmpty())
                {
                    _bottleIn.gameObject.SetActive(false);
                }
            });
    }

    public void TweenBottle(Bottle target, int idRotate, float timeFill, Action action1, Action action2)
    {
        Sequence s = DOTween.Sequence();

        //Append(Tween t) : Thêm 1 tween vào sequence và chạy theo thứ tự lần lượt
        //Join(Tween t): Chạy một tween mới cùng với tween trước đó 
        //Prepend(Tween t): Chạy tween trước tween đầu tiên
        //Insert(float time, Tween t): Chèn tween vào một thời điểm cụ thể trong sequence.
        //SetLoops(int count): Lặp lại sequence (số âm để lặp vô hạn).
        //SetEase(Ease.Linear): Thiết lập kiểu easing.
        //_initPos = transform.position;
        float timeMove = 0.5f;
        Vector3 angleTarget = data.rotateFills[idCurFill];
        Vector3 angleFill = data.rotateFills[idRotate];
        float scale1 = data.scaleOffsets[idCurFill];
        float scale2 = data.scaleOffsets[idRotate];
        
        if (GetPos().x < target.GetPos().x)
        {
            angleTarget = -angleTarget;
            angleFill = -angleFill;
        }

        if (GetPos().x < target.GetPos().x)
        {
            //b1 nằm bên trái
            FlipPivot(true);
        }
        else
        {
            //b1 nằm bên phải
            FlipPivot(false);
        }

        //Xoay + Di chuyển b1 -> b2
        s.Append(transform.DOMove(target.GetPosTarget(), timeMove));
        s.Join(DOTween.To(() => 1f, x => _scaleOffset = x, scale1, timeMove).OnUpdate(() =>
        {
            _waterMat.SetFloat("_ScaleOffset", _scaleOffset);
        }).OnComplete(() => { _curScale = _scaleOffset; }));
        s.Join(transform.DORotate(angleTarget, timeMove).OnComplete(() => action1?.Invoke()));

        //Bắt đầu đổ
        SoundCtrl.I.PlaySoundByTime(TypeSound.POURBOTTLE, 0.7f,timeFill);
        s.Append(transform.DORotate(angleFill, timeFill).OnComplete(() =>
        {
            if (GetPos().x < target.GetPos().x)
            {
                //b1 nằm bên trái
                ResetFlip(true);
            }
            else
            {
                //b1 nằm bên phải
                ResetFlip(false);
            }
        }));
        s.Join(DOTween.To(() => _curScale, x => _scaleOffset = x, scale2, timeFill).OnUpdate(() =>
        {
            _waterMat.SetFloat("_ScaleOffset", _scaleOffset);
        }));


        s.Append(transform.DOMove(initPos, timeMove).OnComplete(() => action2?.Invoke()));
        s.Join(DOTween.To(() => _scaleOffset, x => _scaleOffset = x, 1f, timeMove).OnUpdate(() =>
        {
            _waterMat.SetFloat("_ScaleOffset", _scaleOffset);
        }));
        s.Join(transform.DORotate(Vector3.zero, timeMove));
        s.SetEase(Ease.Linear);
    }

    public void FlipPivot(bool isPlaceLeft)
    {
        if (isPlaceLeft)
        {
            if (GetScaleX() > 0)
            {
                float newPosX = transform.position.x + 0.5f;
                transform.position = new Vector2(newPosX, transform.localPosition.y);
                transform.localScale = new Vector3(-1, 1, 1);
                _bottleOut.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (GetScaleX() < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                _bottleOut.localScale = new Vector3(1, 1, 1);
                transform.localPosition = new Vector2(transform.localPosition.x - 0.5f, transform.localPosition.y);
            }
        }

    }

    public void ResetFlip(bool isPlaceLeft)
    {
        transform.localScale = Vector3.one;
        _bottleOut.localScale = new Vector3(1, 1, 1);

        if (isPlaceLeft)
        {
            if (GetScaleX() > 0)
            {
                transform.localPosition = new Vector2(transform.localPosition.x - 0.5f, transform.localPosition.y);
            }
        }
        else
        {
            if (GetScaleX() < 0)
            {
                transform.localPosition = new Vector2(transform.localPosition.x + 0.5f, transform.localPosition.y);
            }
        }
    }
}

public enum TypeWater {
    NONE,
    C0,
    C1,
    C2,
    C3,
    C4,
    C5,
    C6,
    C7,
    C8,
    C9
}
