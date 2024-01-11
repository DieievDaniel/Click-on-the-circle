using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleManager : MonoBehaviour
{
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float autoDestroyInterval;
    public Canvas canvasPrefab;

    private List<GameObject> activeCircles = new List<GameObject>();
    public CountdownTimer countdownTimer;

    private void Start()
    {

        countdownTimer.OnCountdownFinished.AddListener(Coroutines);
    }

    public void Coroutines()
    {
        StartCoroutine(SpawnCircles());
        StartCoroutine(AutoDestroyCircle());
    }

    private IEnumerator SpawnCircles()
    {
        RectTransform canvasRect = canvasPrefab.GetComponent<RectTransform>();

        while (true)
        {
            float circleRadius = circlePrefab.GetComponent<RectTransform>().sizeDelta.x * circlePrefab.transform.localScale.x / 2f;

            Vector2 spawnPosition = new Vector2(
                Random.Range(circleRadius, canvasRect.sizeDelta.x - circleRadius),
                Random.Range(circleRadius, canvasRect.sizeDelta.y - circleRadius)
            );

            GameObject circle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity, canvasPrefab.transform);
            activeCircles.Add(circle);

            CircleButton circleButton = circle.AddComponent<CircleButton>();
            circleButton.circleManager = this;

            StartCoroutine(DestroyCircleAfterDelay(circle, autoDestroyInterval));

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator DestroyCircleAfterDelay(GameObject circle, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (activeCircles.Contains(circle))
        {
            activeCircles.Remove(circle);
            Destroy(circle);

        }
    }

    private IEnumerator AutoDestroyCircle()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoDestroyInterval);

            foreach (GameObject circle in activeCircles)
            {
                StartCoroutine(DestroyCircleAfterDelay(circle, autoDestroyInterval));
            }
        }
    }

    public void RemoveCircle(GameObject circle)
    {
        if (activeCircles.Contains(circle))
        {
            activeCircles.Remove(circle);
            Destroy(circle);
            Score.scoreValue += 1;
        }
    }
}