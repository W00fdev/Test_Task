using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private PoolProjectiles _poolProjectiles;
    [SerializeField] private Transform _shootingPosition;

    public bool CanShoot;


    public void Shoot(Vector3 pointerPosition)
    {
        if (CanShoot)
        {
            Ray ray = _mainCamera.ScreenPointToRay(pointerPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var projectile = _poolProjectiles.GetFreeProjectile();

                projectile.transform.position = _shootingPosition.position;
                projectile.Shoot(hit.point - projectile.transform.position);
            }
        }
    }
}
