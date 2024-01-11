using UnityEngine;
using UnityEngine.UI;

public class CircleButton : MonoBehaviour
{
    public CircleManager circleManager;
    public Timer timer;
    private void Start()
    {
        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    public void OnButtonClick()
    {

        if (timer != null && timer.canInteract)
        {
            GameObject circle = transform.parent.gameObject;

            if (circleManager != null)
            {
                circleManager.RemoveCircle(circle);
            }
        }
    }
}