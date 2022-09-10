using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerShooting _playerShooting;
    
    public UnityAction OnClick;


    private void Update()
    {
        // No need to use new InputSystem for task.

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
                ProcessMouseOrTouch(Input.GetTouch(0).position);
#else
        if (Input.GetMouseButtonUp(0))
            ProcessMouseOrTouch(Input.mousePosition);
#endif
    }

    private void ProcessMouseOrTouch(Vector3 pointerPosition)
    {
        _playerShooting.Shoot(pointerPosition);
        OnClick?.Invoke();
    }
}
