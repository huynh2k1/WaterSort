using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] bool _hasEffect = false;
    [SerializeField] bool _clickSound = true;
    [SerializeField] float _pressing;
    [SerializeField] float _delayBtn = 1f;
    [SerializeField] bool _canClick = true;

    public UnityEvent onClick = new UnityEvent();
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }

}
