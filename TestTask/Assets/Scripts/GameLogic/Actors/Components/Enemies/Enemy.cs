using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Can be as strategy-pattern:
    // private IMovable, IShootable, IExplodable and etc...

    private Healthable _healthable;
    private Animatable _animatable;

    private void Start()
    {
        // Disable animations on dead if exist

        if (TryGetComponent(out _healthable) && TryGetComponent(out _animatable))
        {
            _healthable.OnDied.AddListener(_animatable.DisableAnimations);
        }
    }
}
