using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationCircle 
{
    public static IEnumerator FadeInCircle(Image image, Color targetColor, float targetSize)
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
}
