using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


/// <summary>
/// Enemies' (and other actors) spots controller.
/// </summary>
public class TargetsSpotter : MonoBehaviour
{
    [Tooltip("Fill only spots with enemies, don't initialize empty spots")]
    [SerializeField] private List<TargetsSpot> _targetsSpots;

    public UnityAction OnTargetsCleared;

    private void Start()
    {
        foreach (var spot in _targetsSpots)
        {
            spot.Initialize();
            spot.OnSpotCleared = SpotCleared;
        }
    }

    private void SpotCleared() => OnTargetsCleared?.Invoke();
}


/// <summary>
/// Spot's handler which is set in the inspector.
/// </summary>
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
