using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestLineLoop : MonoBehaviour
{
    public RectTransform line;      // твоя линия
    public float duration = 1f;     // длительность каждой анимации
    public float minWidth = 100f;   // минимальная длина
    public float maxWidth = 1300f;  // максимальная длина

    void Start()
    {
        AnimateLine();
    }

    void AnimateLine()
    {
        float randomWidth = Random.Range(minWidth, maxWidth);

        // Анимируем sizeDelta.x к случайной ширине
        line.DOSizeDelta(new Vector2(randomWidth, line.sizeDelta.y), duration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => AnimateLine());  // рекурсивно запускаем снова
    }
}
