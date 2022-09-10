using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetsSpotter : MonoBehaviour
{
    [Tooltip("Fill only spots with enemies, don't initialize empty spots")]
    [SerializeField] private List<TargetsSpot> _targetsSpots;

    public UnityAction OnTargetsCleared;

    private void Start()
    {
        foreach (var spot in _targetsSpots)
            spot.Initialize();

        foreach (var targets in _targetsSpots)
            targets.OnSpotCleared = SpotCleared;
    }

    private void SpotCleared()
    {
        OnTargetsCleared?.Invoke();
    }
}


[System.Serializable]
public class TargetsSpot
{
    [SerializeField] private List<Healthable> _enemies;
    private int _count;

    public UnityAction OnSpotCleared;

    // Not constructor because of serialization
    public void Initialize()
    {
        foreach (var enemy in _enemies)
            enemy.OnDied.AddListener(CountDeads);
    }

    private void CountDeads()
    {
        _count++;
        if (_count >= _enemies.Count)
            OnSpotCleared?.Invoke();
    }
}
