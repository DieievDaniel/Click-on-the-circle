
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CircleData")]
public class CircleManagerData : ScriptableObject
{
    public GameObject circlePrefab;
    public float spawnInterval;
    public float autoDestroyInterval;
    public Canvas canvasPrefab;
    public CountdownTimer countdownTimer;
    public Color[] circleColors = { Color.red, Color.blue, Color.green, Color.yellow };
    public Vector2[] circleSizeRange = { new Vector2(50f, 100f) };
}
