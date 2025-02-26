using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public ButtonEffect btnClose;
    public CanvasGroup mainGroup;
    public virtual void Awake()
    {
        if (btnClose != null)
            btnClose.onClick.AddListener(Hide);
    }

    public virtual void Show()
    {
        if (btnClose)
        {
            btnClose.Hide();
        }
        mainGroup.interactable = false;
        mainGroup.transform.localScale = Vector3.one * 0.67f;
        gameObject.SetActive(true);

        mainGroup.transform.DOScale(1, 0.2f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            mainGroup.interactable = true;
            DOVirtual.DelayedCall(1f, () =>
            {
                if(btnClose)
                    btnClose.Show();
            });
        });
    }

    public virtual void Hide()
    {
        mainGroup.transform.DOKill();
        mainGroup.interactable = false;
        mainGroup.transform.localScale = Vector3.one;
        mainGroup.transform.DOScale(1.1f, 0.1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            mainGroup.transform.DOScale(Vector3.one * 0.67f, 0.1f).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        });
    }

    public virtual void OnClickClose()
    {
        btnClose.Hide();
        Hide();
    }
}
