using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Animatable : MonoBehaviour
{
    [SerializeField] private string _runTriggerName;
    [SerializeField] private string _idleTriggerName;

    private int _runStateHash;
    private int _idleStateHash;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _runStateHash = Animator.StringToHash(_runTriggerName);
        _idleStateHash = Animator.StringToHash(_idleTriggerName);
    }

    private void PlayAnimation(int animationStateHash)
    {
        // Prevent multiple triggering
        if (_animator.GetCurrentAnimatorStateInfo(0).shortNameHash != animationStateHash)
            _animator.SetTrigger(animationStateHash);
    }

    public void PlayAnimation(AnimatorState newState)
    {
        switch(newState)
        {
            case AnimatorState.UNKNOWN:
                throw new System.ArgumentNullException("Unknown animator state catched");
            case AnimatorState.IDLE:
                PlayAnimation(_idleStateHash);
                break;
            case AnimatorState.RUN:
                PlayAnimation(_runStateHash);
                break;
        }
    }

    public void DisableAnimations() => _animator.enabled = false;
}
