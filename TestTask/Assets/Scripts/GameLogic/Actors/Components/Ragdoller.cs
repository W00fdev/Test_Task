using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoller : MonoBehaviour
{
    [SerializeField] private Transform _rigRoot;
    [SerializeField] private Rigidbody[] _ragdollRigidbodies;

    // For bullet collision checking
    private BoxCollider _bulletCollider;

    private void Start()
    {
        if (_rigRoot == null)
            throw new System.MissingFieldException("Missing field _rigRoot");

        _ragdollRigidbodies = _rigRoot.GetComponentsInChildren<Rigidbody>();
        
        if (TryGetComponent(out _bulletCollider))
        {}

        DisableRagdoll();
    }

    private void SwitchRagdollOn(bool state)
    {
        foreach (var rb in _ragdollRigidbodies)
            rb.isKinematic = !state;

        if (_bulletCollider != null)
            _bulletCollider.enabled = !state;
    }

    public void EnableRagdoll() => SwitchRagdollOn(true);

    public void DisableRagdoll() => SwitchRagdollOn(false);
}
