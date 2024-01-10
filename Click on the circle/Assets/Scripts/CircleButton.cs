using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleButton : MonoBehaviour
{
    public CircleManager circleManager;
    private void OnMouseDown()
    {
        
        Destroy(gameObject);
        circleManager.RemoveCircle(gameObject);
    }
}
