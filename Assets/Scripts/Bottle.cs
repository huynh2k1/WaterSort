using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] Transform _mask;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Material _waterMat;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] AnimationCurve _animationCurve;
    [Header("Properties")]
    public float[] thresholdFills = { -0.5f, -0.29f, -0.07f, };
    public Color[] bottleColors;

    [SerializeField] float time = 2f;
    [SerializeField] float _scaleOffset = 1f;

    private void Start()
    {
        Init();
    }

    private void OnMouseDown()
    {
        DoNuoc();
    }

    void Init()
    {
        if (_waterMat == null)
        {
            _waterMat = _spriteRenderer.material;
        }

        UpdateColorsOnBottle();
    }

    private void Update()
    {
        if (_waterMat)
        {
            _waterMat.SetVector("_PosWorld", _mask.position);
            _waterMat.SetVector("_ObjectScale", transform.localScale);
        }
    }

    //Cập nhật màu
    void UpdateColorsOnBottle()
    {
        _waterMat.SetColor("_Color1", bottleColors[0]);
        _waterMat.SetColor("_Color2", bottleColors[1]);
        _waterMat.SetColor("_Color3", bottleColors[2]);
        _waterMat.SetColor("_Color4", bottleColors[3]);
    }

    void DoNuoc()
    {
        transform.DOKill();
        transform.eulerAngles = Vector3.zero;
        Vector3 angle = new Vector3(0, 0, 90);
        transform.DORotate(angle, time, RotateMode.FastBeyond360).OnUpdate(() =>
        {
            if (transform.eulerAngles.z >= 30f && transform.eulerAngles.z < 31f) // Kiểm tra khoảng nhỏ để tránh gọi nhiều lần
            {
                Debug.Log("Góc đạt 30 độ!");
                // Thực hiện hành động mong muốn
                DOTween.To(() => 0.34f, x => _waterMat.SetFloat("_FillAmount", x), -0.5f, time);
            }
        });
        DOTween.To(() => 1f, x => _scaleOffset = x, 0.5f, time);
        DOTween.To(() => 1f, x => _waterMat.SetFloat("_ScaleOffset", x), 0.5f, time);

    }

    //0 : 0.34
    //30 : 0.34
    //54 : 0.13
    //71 : -0.08
    //83 : -0.29
    //90 : -0.5

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

}
