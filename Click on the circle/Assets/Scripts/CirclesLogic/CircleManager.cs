using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleManager : MonoBehaviour
{
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float autoDestroyInterval;
    public Canvas canvasPrefab;

    private List<GameObject> activeCircles = new List<GameObject>();
    public CountdownTimer countdownTimer;
    [SerializeField] private Color[] circleColors = { Color.red, Color.blue, Color.green, Color.yellow };
    [SerializeField] private Vector2[] circleSizeRange = { new Vector2(50f, 100f) };

    public CircleManagerData circleManagerData;

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
            Color randomColor = circleColors[Random.Range(0, circleColors.Length)];
            float randomSize = Random.Range(circleSizeRange[0].x, circleSizeRange[0].y);
            float circleRadius = randomSize / 2f;

            Vector2 spawnPosition = new Vector2(
                Random.Range(circleRadius, canvasRect.sizeDelta.x - circleRadius),
                Random.Range(circleRadius, canvasRect.sizeDelta.y - circleRadius)
            );

            GameObject circle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity, canvasPrefab.transform);
            activeCircles.Add(circle);

            CircleButton circleButton = circle.AddComponent<CircleButton>();
            circleButton.circleManager = this;

            Image circleImage = circle.GetComponent<Image>();
            if (circleImage != null)
            {                
                yield return StartCoroutine(FadeInCircle(circleImage, randomColor, randomSize));
            }

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

    private IEnumerator FadeInCircle(Image image, Color targetColor, float targetSize)
    {
        float duration = 0.3f; 
        float timer = 0f;
        Color startColor = image.color;
        float startSize = image.rectTransform.sizeDelta.x;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            image.color = Color.Lerp(Color.clear, targetColor, t);
            image.rectTransform.sizeDelta = new Vector2(Mathf.Lerp(0f, targetSize, t), Mathf.Lerp(0f, targetSize, t));

            yield return null;
        }

        image.color = targetColor;
        image.rectTransform.sizeDelta = new Vector2(targetSize, targetSize);
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
