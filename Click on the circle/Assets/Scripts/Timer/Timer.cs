using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
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
                canInteract = false;
                DisableCircles();
                GameOver();
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
    public void GameOver()
    {
        int finalScore = Score.scoreValue;
        SceneManager.LoadScene("GameOverScreen");
        PlayerPrefs.SetInt("FinalScore", finalScore);
    }
}