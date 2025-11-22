using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BonusReward : MonoBehaviour
{
    public BonusType BonusType => bonusType;

    [SerializeField] private BonusType bonusType;
    [SerializeField] private Transform transformMove;

    private Transform _transformTarget;
    private IEnumerator _timer;

    public void SetData(Transform transformTarget)
    {
        _transformTarget = transformTarget;
    }

    public void ActivateMove(float durationWait, float durationMove)
    {
        if (_timer != null) Coroutines.Stop(_timer);

        _timer = Timer(durationWait, durationMove);
        Coroutines.Start(_timer);
    }

    private void OnDestroy()
    {
        if (_timer != null) Coroutines.Stop(_timer);
    }

    private IEnumerator Timer(float durationWait, float durationMove)
    {
        yield return new WaitForSeconds(durationWait);

        StartMoving(durationMove);
    }

    private void StartMoving(float durationMove)
    {
        transform.DOScale(0.42f, durationMove - 0.01f);
        transform.DOLocalMove(_transformTarget.localPosition, durationMove)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                OnAddBonus?.Invoke(bonusType);
                OnDestroyed?.Invoke(this);
                Destroy(gameObject);
            });
    }

    #region Output

    public event Action<BonusType> OnAddBonus;
    public event Action<BonusReward> OnDestroyed;

    #endregion
}
