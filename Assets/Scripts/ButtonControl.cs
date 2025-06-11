using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonControl : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    bool isPressed = false;
    public GameObject Player;
    public float Force;

    void Update()
    {
        if (isPressed)
        {
            Player.transform.Translate(Force * Time.deltaTime, 0, 0);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
