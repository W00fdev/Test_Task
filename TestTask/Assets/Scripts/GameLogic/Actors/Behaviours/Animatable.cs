using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Animatable : MonoBehaviour
{
    [SerializeField] private string _runTriggerName;
    [SerializeField] private string _idleTriggerName;
    
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    private void PlayAnimation(string animationName)
    {
        // Prevent multiple triggering
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) == false)
            _animator.SetTrigger(animationName);
    }

    public void PlayIdle() => PlayAnimation(_idleTriggerName);
    public void PlayRun() => PlayAnimation(_runTriggerName);
    public void DisableAnimations() => _animator.enabled = false;
}
