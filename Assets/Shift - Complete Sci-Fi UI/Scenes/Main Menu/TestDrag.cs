using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Iniciar el arrastre
        Debug.Log("pointerDown");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        // Arrastrar el elemento con el mouse
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Restaurar la visibilidad y la interacción
        Debug.Log("EndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Verificar si se soltó en un elemento válido
        if (eventData.pointerEnter != null)
        {
            // Obtener el objeto en el que se soltó
            GameObject droppedOnObject = eventData.pointerEnter;

            // Ejecutar un método en el objeto en el que se soltó
            droppedOnObject.SendMessage("MetodoASerEjecutado", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void MetodoASerEjecutado()
    {
        Debug.Log("Me Apretaron");
    }
}