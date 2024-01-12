using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
   public static int scoreValue = 0;
   private TextMeshProUGUI score;
   public GameOverScreen gameOverScreen;

    private void Start()
    {
        score = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        score.text = "Score: " + scoreValue;
    }
    
}
