using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[AddComponentMenu("Pool/Projectile")]
public class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _force;

    private Vector3 _direction;
    private bool _enabled;

    private Rigidbody _rigidbody;

    private void Start() => _rigidbody = GetComponent<Rigidbody>();

    public void Shoot(Vector3 direction)
    {
        _direction = direction.normalized;
        _enabled = true;
    }    

    private void FixedUpdate()
    {
        if (_enabled)
            MoveBullet();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Healthable healthable))
            healthable.Damage(_damage);

        ReturnToPool();
    }

    private void MoveBullet()
    {
        var position = transform.position + _speed * Time.fixedDeltaTime * _direction;
        _rigidbody.MovePosition(position);
    }

    public void ReturnToPool()
    {
        _enabled = false;
        gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
    }
    public Transform GetTransform() => transform;
}
