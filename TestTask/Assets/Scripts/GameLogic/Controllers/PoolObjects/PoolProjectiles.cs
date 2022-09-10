using UnityEngine;


public class PoolProjectiles : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private int _projectilePoolCount = 5;

    private PoolObjects _projectilesPool;
    
    private void Start()
    {
        // The Pool is parent of projectiles, so need to set him to (0,0,0)
        transform.position = Vector3.zero;
        _projectilesPool = new PoolObjects(_projectilePrefab, transform, _projectilePoolCount, true);
    }

    public Projectile GetFreeProjectile() => _projectilesPool.GetFreeObject() as Projectile;
}
