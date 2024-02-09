using System.Collections;
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

    public void StartAutoDestroy(float delay)
    {
        StartCoroutine(AutoDestroyCircle(delay));
    }

    private IEnumerator AutoDestroyCircle(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject circle = transform.parent.gameObject;
        if (circleManager != null)
        {
            circleManager.RemoveCircle(circle);
        }
    }

    public void StartDestroyAfterDelay(GameObject circle, float delay)
    {
        StartCoroutine(DestroyCircleAfterDelay(circle, delay));
    }

    private IEnumerator DestroyCircleAfterDelay(GameObject circle, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (circle != null)
        {
            Destroy(circle);
        }
    }
}