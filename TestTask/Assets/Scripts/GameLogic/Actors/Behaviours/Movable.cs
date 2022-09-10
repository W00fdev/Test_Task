using System.Collections;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine;


[RequireComponent(typeof(NavMeshAgent))]
public class Movable : MonoBehaviour
{
    [SerializeField] private float _angularSpeedRotation = 5f;

    private readonly float _stoppingDistanceError = 0.1f;
    private NavMeshAgent _navMeshAgent;
    private bool _isMoving;

    public UnityAction OnDestinationReached;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _isMoving = false;
    }

    private void Update()
    {
        if (_isMoving)
            CheckReachedDestination();
    } 

    private void CheckReachedDestination()
    {
        if (_navMeshAgent.pathPending == false)
        {
            if (!_navMeshAgent.hasPath == false)
            {
                if (_navMeshAgent.remainingDistance <= 
                    _navMeshAgent.stoppingDistance + _stoppingDistanceError)
                {
                    _isMoving = false;
                    OnDestinationReached?.Invoke();
                }
            }
        }
    }

    public void SetNewDestination(Vector3 destination)
    {
        _navMeshAgent.SetDestination(destination);
        _isMoving = true;
    }

    public void ForceNewPositionRotation(Vector3 position, Vector3 rotation) 
        => transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));

    public void SetNewRotation(Vector3 eulerRotation)
        => StartCoroutine(SmoothLookAt(Quaternion.Euler(eulerRotation)));

    private IEnumerator SmoothLookAt(Quaternion targetRotation)
    {
        float t = 0f;
        while (t < 1f)
        {
            yield return null;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);
            
            t += Time.deltaTime * _angularSpeedRotation;
        }
    }

}
