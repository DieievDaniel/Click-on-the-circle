using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePool : MonoBehaviour
{
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private int poolSize;

    private List<GameObject> circlePool = new List<GameObject>();
    private int currentIndex = 0;

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject circle = Instantiate (circlePrefab, Vector2.zero, Quaternion.identity, transform);
            circle.SetActive (false);
            circlePool.Add (circle);
        }
    }

    public GameObject GetCircleFromPool(Vector2 spawnPosition)
    {
        GameObject circle = circlePool[currentIndex];
        currentIndex = (currentIndex + 1) % poolSize;

        if (!circle.activeSelf)
        {
            circle.transform.position = spawnPosition;
            circle.SetActive(true); 
            return circle;
        }

        for (int i = 0; i < poolSize; i++)
        {
            if (!circlePool[i].activeSelf)
            {
                circlePool[i].transform.position = spawnPosition;
                return circlePool[i];
            }
        }

        
        GameObject newCircle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity, transform);
        newCircle.SetActive(false);
        circlePool.Add(newCircle);
        poolSize++;
        return newCircle;
    }
    public void ReturnCircleToPool(GameObject circle)
    {
        if (circlePool.Contains(circle))
        {
            circle.SetActive(false);
        }
    }
}
