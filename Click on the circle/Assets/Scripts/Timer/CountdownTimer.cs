using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CountdownTimer : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI countdownText;

    public UnityEvent OnCountdownFinished;

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }
    public IEnumerator StartCountdown()
    {
        int countdown = 3;

        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);
        Destroy(countdownText);
        OnCountdownFinished?.Invoke();       
    }     
}
