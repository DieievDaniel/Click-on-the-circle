using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    public float timer = 60f;
    public TextMeshProUGUI timerText;
    public GameObject[] circles;

    public bool canInteract = true;
    private void Update()
    {
        StartCoroutine(StartTimer());
    }
 
    public IEnumerator StartTimer()
    {
        if (canInteract)
        {
            if (timer > 0)
            {
                yield return new WaitForSeconds(3f);
                timer -= Time.deltaTime;
                timerText.text = Mathf.CeilToInt(timer).ToString("00");
            }
            else
            {
                Time.timeScale = 0f;
                canInteract = false;
                DisableCircles();
            }
        }
    }
    private void DisableCircles()
    {
        foreach (GameObject circle in circles)
        {

            if (canInteract)
            {
                circle.SetActive(false);
            }
        }
    }

}