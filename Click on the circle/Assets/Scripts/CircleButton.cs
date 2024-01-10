using UnityEngine;
using UnityEngine.UI;

public class CircleButton : MonoBehaviour
{
    public CircleManager circleManager; 

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
        

        GameObject circle = transform.parent.gameObject;

        if (circleManager != null)
        {

            
            circleManager.RemoveCircle(circle);
           
        }
    }
}
