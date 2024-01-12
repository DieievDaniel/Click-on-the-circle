using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI pointText;

    private void OnEnable()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        Setup(finalScore);
    }
    public void Setup(int score)
    {
        gameObject.SetActive(true);

        pointText.text = "POINTS: " + score.ToString();
    }

    public void Restart()
    {
        Score.scoreValue = 0;
        SceneManager.LoadScene("GameScene");  
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
