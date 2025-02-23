using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public Stack<TypeWater> typeWaters;
    public TypeWater[] waters;
    public BottleSO data;

    [Header("REFERENCE")]
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] Transform _bottleOut;
    [SerializeField] Transform _mask;
    [SerializeField] Transform _posTarget;
    [SerializeField] SpriteRenderer _bottleIn;
    [SerializeField] AnimationCurve _animationCurve;

    [Header("PRIVATE")]
    private Material _waterMat;

    [Header("PROPERTIES")]
    public int idCurFill;
    private Vector3 _initPos;
    private bool _canClick = true;
    float _scaleOffset = 1f;


    private void Start()
    {
        Init();
    }

    private void Update()
    {
        UpdateMat();
    }
    //void DoNuoc()
    //{
    //    transform.DOKill();
    //    transform.eulerAngles = Vector3.zero;
    //    Vector3 angle = new Vector3(0, 0, 90);
    //    transform.DORotate(angle, time, RotateMode.FastBeyond360).OnUpdate(() =>
    //    {
    //        if (transform.eulerAngles.z >= 30f && transform.eulerAngles.z < 31f) // Kiểm tra khoảng nhỏ để tránh gọi nhiều lần
    //        {
    //            Debug.Log("Góc đạt 30 độ!");
    //            // Thực hiện hành động mong muốn
    //            DOTween.To(() => 0.34f, x => _waterMat.SetFloat("_FillAmount", x), -0.5f, time);
    //        }
    //    });
    //    DOTween.To(() => 1f, x => _scaleOffset = x, 0.5f, time);
    //    DOTween.To(() => 1f, x => _waterMat.SetFloat("_ScaleOffset", x), 0.5f, time);

    //}
    //Nếu b1 chưa có => b1 = this
    //Nếu b1 có : TH1: b2 chưa có -> b2 = this -> kiểm tra
    //TH2: b1 = this
    private void OnMouseDown()
    {
        if (!_canClick) return;

        if (BottleCtrl.I.b1 == null)
        {
            if (this.IsNull())
            {
                return;
            }
            BottleCtrl.I.b1 = this;
        }
        else if (BottleCtrl.I.b2 == null)
        {
            if (BottleCtrl.I.b1 == this)
            {
                BottleCtrl.I.b1 = null;
            }
            else
            {
                if (this.IsFull() == true)
                {
                    return;
                }

                BottleCtrl.I.b2 = this;

                BottleCtrl.I.B1ToB2();
            }
        }
        //DoNuoc();
    }

    void Init()
    {
        if (_waterMat == null)
        {
            _waterMat = _bottleIn.material;
        }
        _initPos = transform.position;
        SetCurFillWater();
        UpdateColorWaterByThreshold(idCurFill);
    }

    public int CountTopSameType()
    {
        int count = 0;
        int topID = GetIDHasWater();
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
        if (IsNull()) return waters.Length;

        int idTop = GetIDNoWater();
        return waters.Length - idTop;
    }

    void UpdateMat()
    {
        _waterMat.SetVector("_PosWorld", _mask.position);
        _waterMat.SetVector("_ObjectScale", transform.localScale);
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

    //IEnumerator DoNuoc()
    //{
    //    float t = 0;
    //    float lerpValue;
    //    float angleValue;

    //    while(t < time)
    //    {
    //        lerpValue = t / time;
    //        angleValue = Mathf.Lerp(0f, 90f, lerpValue);
    //        _scaleOffset = Mathf.Lerp(1f, 0.5f, lerpValue);
    //        transform.eulerAngles = new Vector3(0, 0, angleValue);
    //        _waterMat.SetFloat("_ScaleOffset", _scaleOffset);
    //        _waterMat.SetFloat("_FillAmount", _animationCurve.Evaluate(angleValue));
    //        t += Time.deltaTime;

    //        yield return new WaitForEndOfFrame();
    //    }
    //    angleValue = 90f;
    //    transform.eulerAngles = new Vector3(0, 0, angleValue);
    //    _waterMat.SetFloat("_ScaleOffset", _scaleOffset);
    //    _waterMat.SetFloat("_FillAmount", _animationCurve.Evaluate(angleValue));

    //}

    void UpdateColorWaterByThreshold(int threshold)
    {
        for (int i = 0; i < threshold; i++)
        {
            switch (waters[i])
            {
                case TypeWater.NONE:
                    break;
                case TypeWater.RED:
                    SetColorWaterById(i, data.colors[0]);
                    break;
                case TypeWater.GREEN:
                    SetColorWaterById(i, data.colors[1]);
                    break;
                case TypeWater.BLUE:
                    SetColorWaterById(i, data.colors[2]);
                    break;
                case TypeWater.YELLOW:
                    SetColorWaterById(i, data.colors[3]);
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
        idCurFill = GetIDNoWater();
        _waterMat.SetFloat("_FillAmount", data.thresholdFills[idCurFill]);
    }

    public void ReFill(int numReFill, float time, TypeWater type, Action actionDone)
    {
        if (IsNull())
        {
            _bottleIn.gameObject.SetActive(true);
        }
        int sumFill = idCurFill + numReFill;
        for (int i = idCurFill; i < sumFill; i++)
        {
            waters[i] = type;
        }
        UpdateColorWaterByThreshold(sumFill);

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
                if (IsNull())
                {
                    _bottleIn.gameObject.SetActive(false);
                }
            });
    }
    float curScale;

    public void TweenBottle(Bottle target, int idRotate, float timeFill, Action action1, Action action2)
    {
        Sequence s = DOTween.Sequence();

        //Append(Tween t) : Thêm 1 tween vào sequence và chạy theo thứ tự lần lượt
        //Join(Tween t): Chạy một tween mới cùng với tween trước đó 
        //Prepend(Tween t): Chạy tween trước tween đầu tiên
        //Insert(float time, Tween t): Chèn tween vào một thời điểm cụ thể trong sequence.
        //SetLoops(int count): Lặp lại sequence (số âm để lặp vô hạn).
        //SetEase(Ease.Linear): Thiết lập kiểu easing.
        _initPos = transform.position;
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

        //Xoay + Di chuyển b1 -> b2
        s.Append(transform.DOMove(target.GetPosTarget(), timeMove));
        s.Join(DOTween.To(() => 1f, x => _scaleOffset = x, scale1, timeMove).OnUpdate(() =>
        {
            _waterMat.SetFloat("_ScaleOffset", _scaleOffset);
        }).OnComplete(() => { curScale = _scaleOffset; }));
        s.Join(transform.DORotate(angleTarget, timeMove).OnComplete(() => action1?.Invoke()));

        //Bắt đầu đổ
        s.Append(transform.DORotate(angleFill, timeFill));
        s.Join(DOTween.To(() => curScale, x => _scaleOffset = x, scale2, timeFill).OnUpdate(() =>
        {
            _waterMat.SetFloat("_ScaleOffset", _scaleOffset);
        }));


        s.Append(transform.DOMove(_initPos, timeMove).OnComplete(() => action2?.Invoke()));
        s.Join(DOTween.To(() => _scaleOffset, x => _scaleOffset = x, 1f, timeMove).OnUpdate(() =>
        {
            _waterMat.SetFloat("_ScaleOffset", _scaleOffset);
        }));
        s.Join(transform.DORotate(Vector3.zero, timeMove));
        s.SetEase(Ease.Linear);
    }

    public int GetIDHasWater()
    {
        for (int i = 0; i < waters.Length; i++)
        {
            if (waters[i] == TypeWater.NONE)
            {
                if (i == 0) return -1;
                else return i - 1;
            }
        }
        return waters.Length - 1;
    }

    public int GetIDNoWater()
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
        if (GetIDHasWater() < waters.Length - 1)
        {
            return false;
        }
        return true;
    }

    //Kiểm tra lọ có phải là lọ rỗng
    public bool IsNull()
    {
        if (GetIDNoWater() == 0)
        {
            return true;
        }
        return false;
    }

    public Vector2 GetPos() => transform.position;

    public Vector2 GetPosTarget() => _posTarget.position;

    public float GetScaleX() => transform.localScale.x;
}

public enum TypeWater { NONE, RED, GREEN, BLUE, YELLOW }
