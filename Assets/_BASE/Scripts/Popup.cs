using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public Transform main;
    public ButtonEffect btnClose; 

    private void Awake()
    {
        if (btnClose)
            btnClose.onClick.AddListener(Hide);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        btnClose.gameObject.SetActive(false);

        main.localScale = Vector3.one * 0.67f;

        main.DOScale(1, 0.2f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            DOVirtual.DelayedCall(1f, () =>
            {
                btnClose.gameObject.SetActive(true);
            });
        });
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
