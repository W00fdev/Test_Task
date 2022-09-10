using UnityEngine;


public interface IPoolable
{
    public void ReturnToPool();

    public Transform GetTransform();
}
