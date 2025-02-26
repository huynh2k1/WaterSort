using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ButtonEffect : Button
{
    public Ease easing = Ease.OutBounce;
    public bool _hasFX = true;
    public bool _clickSound = true;
    public float _delayBtn = 0.5f;
    public bool _canClick = true;

    public Vector3 initScale;

    protected override void Awake()
    {
        initScale = transform.localScale;   
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!_canClick)
            return;
        if(SoundCtrl.I != null)
        {
            SoundCtrl.I.PlaySound(TypeSound.CLICK);
        }
        _canClick = false;
        ScaleDown();
        DOVirtual.DelayedCall(_delayBtn, () =>
        {
            _canClick = true;
        });
        base.OnPointerClick(eventData);
    }
    void ScaleDown()
    {
        if (_hasFX)
        {
            transform.DOKill();
            transform.localScale = initScale;
            transform.DOScale(initScale * 0.8f, 0.1f).SetEase(Ease.Linear).OnComplete(() => {
                transform.DOScale(initScale, 0.1f).SetEase(Ease.Linear);
            });
        }
    }

    //void ScaleUp(Action action = default)
    //{
    //    if (_hasFX)
    //    {
    //        transform.DOKill();
    //        transform.localScale = initScale;
    //        transform.DOScale(initScale * 1.1f, 0.1f).SetEase(easing).OnComplete(() =>
    //        {
    //            transform.localScale = initScale;
    //        });
    //    }
    //}

    public void Show()
    {
        transform.localScale = Vector3.one * 0.67f;
        gameObject.SetActive(true);

        transform.DOScale(1, 0.2f).SetEase(Ease.OutBounce);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(ButtonEffect))]
public class ButtonEffectEditor : Editor
{
    ButtonEffect mtarget;

    private void OnEnable()
    {
        mtarget = target as ButtonEffect;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
#endif
