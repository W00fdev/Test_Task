using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Healthable : MonoBehaviour
{
    [SerializeField] private float _maxHp;
    [SerializeField] private float _hp;
    
    private Ragdoller _ragdoller;
    private HealthbarUI _healthbarUI;

    public UnityEvent OnDied;

    private void Start()
    {
        TryGetComponent(out _ragdoller);
        TryGetComponent(out _healthbarUI);

        if (_maxHp < _hp)
            throw new System.Exception("Max hp isn't initialized");
    }

    private void Die()
    {
        if (_ragdoller != null)
            _ragdoller.EnableRagdoll();

        OnDied?.Invoke();
    }

    public void Damage(float damage)
    {
        _hp = Mathf.Clamp(_hp - damage, 0f, _hp);
        _healthbarUI.HealthChanged(_hp / _maxHp);
        
        if (Mathf.Approximately(_hp, 0f) == true)
            Die();
    }    
}
