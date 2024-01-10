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

    private void Start()
    {
        StartCoroutine(SpawnCircles());
        StartCoroutine(AutoDestroyCircle());
    }

    private IEnumerator SpawnCircles()
    {
        RectTransform canvasRect = canvasPrefab.GetComponent<RectTransform>();
        float canvasHalfWidth = canvasRect.sizeDelta.x / 2f;
        float canvasHalfHeight = canvasRect.sizeDelta.y / 2f;

        float circleHalfWidth = circlePrefab.GetComponent<RectTransform>().sizeDelta.x / 2f;
        float circleHalfHeight = circlePrefab.GetComponent<RectTransform>().sizeDelta.y / 2f;

        while (true)
        {
            Vector2 spawnPosition = new Vector2(
                canvasPrefab.transform.position.x + Random.Range(-canvasHalfWidth + circleHalfWidth, canvasHalfWidth - circleHalfWidth),
                canvasPrefab.transform.position.y + Random.Range(-canvasHalfHeight + circleHalfHeight, canvasHalfHeight - circleHalfHeight)
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
        activeCircles.Remove(circle);
    }
}
