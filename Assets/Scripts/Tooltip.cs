using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipBox;

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipBox.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipBox.SetActive(false);
    }
}
