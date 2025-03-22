
using DG.Tweening;
using DG.Tweening.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TweenUtils
{

    #region TRANSFORM MOVE
    public static void Move(Transform current, Vector3 target, float time, Ease easing, Action actionDone = default)
    {
        current.DOKill();
        current.DOMove(target, time).SetEase(easing)
            .OnComplete(() =>
            {
                actionDone?.Invoke();
            });
    }

    public static void MoveX(Transform current, float targetX, float time, Ease easing, Action actionDone = default)
    {
        current.DOKill();
        current.DOMoveX(targetX, time).SetEase(easing)
            .OnComplete(() =>
            {
                actionDone?.Invoke();
            });
    }

    public static void MoveY(Transform current, float targetY, float time, Ease easing, Action actionDone = default)
    {
        current.DOKill();
        current.DOMoveY(targetY, time).SetEase(easing)
            .OnComplete(() => 
            {
                actionDone?.Invoke();
            });
    }
    public static void MoveZ(Transform current, float targetZ, float time, Ease easing, Action actionDone = default)
    {
        current.DOKill();
        current.DOMoveY(targetZ, time).SetEase(easing)
            .OnComplete(() =>
            {
                actionDone?.Invoke();
            });
    }
    #endregion

    #region RectTranform
    public static void RectMove(RectTransform current, Vector3 target, float time, Ease easing, Action actionDone = default)
    {
        current.DOKill();
        current.DOMove(target, time).SetEase(easing)
            .OnComplete(() =>
            {
                actionDone?.Invoke();
            });
    }
    public static void RectMoveX(RectTransform current, float targetX, float time, Ease easing, Action actionDone = default)
    {
        current.DOKill();
        current.DOMoveX(targetX, time).SetEase(easing)
            .OnComplete(() =>
            {
                actionDone?.Invoke();
            });
    }

    public static void RectMoveY(RectTransform current, float targetY, float time, Ease easing, Action actionDone = default)
    {
        current.DOKill();
        current.DOMoveY(targetY, time).SetEase(easing)
            .OnComplete(() =>
            {
                actionDone?.Invoke();
            });
    }
    public static void RectMoveZ(RectTransform current, float targetZ, float time, Ease easing, Action actionDone = default)
    {
        current.DOKill();
        current.DOMoveY(targetZ, time).SetEase(easing)
            .OnComplete(() =>
            {
                actionDone?.Invoke();
            });
    }
    #endregion

    public static void FadeImage(Image current, float alphaInit, float alphaTarget, float time, Ease easing, Action actionDone = default)
    {
        current.DOKill();

        var color = current.color;
        color.a = alphaInit;
        current.color = color;
        current.gameObject.SetActive(true);

        // Fade từ 0 đến 1 trong khoảng thời gian duration
        current.DOFade(alphaTarget, time).SetEase(easing)
            .OnComplete(() =>
            {
                current.gameObject.SetActive(false);
                actionDone?.Invoke();
            });
    }

    public static void TweenTo(float current, float endValue, float time, Action actionDone = default)
    {
        DOTween.To(() => current, x => current = x, endValue, time)
            .OnComplete(() =>
            {
                actionDone?.Invoke();
            });
    }
}
