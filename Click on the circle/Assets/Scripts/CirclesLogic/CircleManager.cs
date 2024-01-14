using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleManager : MonoBehaviour
{
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float autoDestroyInterval;
    [SerializeField] private Color[] circleColors = { Color.red, Color.blue, Color.green, Color.yellow };
    [SerializeField] private Vector2[] circleSizeRange = { new Vector2(50f, 100f) };
    [SerializeField] private Canvas canvasPrefab;
    [SerializeField] private CountdownTimer countdownTimer;

    private List<GameObject> activeCircles = new List<GameObject>();

    private void Start()
    {
        countdownTimer.OnCountdownFinished.AddListener(EnterCoroutines);
    }

    public void EnterCoroutines()
    {
        StartCoroutine(SpawnCircles());
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
                yield return StartCoroutine(AnimationCircle.FadeInCircle(circleImage, randomColor, randomSize));
            }
            circleButton.StartAutoDestroy(autoDestroyInterval);
            circleButton.StartDestroyAfterDelay(circle, autoDestroyInterval);

            yield return new WaitForSeconds(spawnInterval);
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
