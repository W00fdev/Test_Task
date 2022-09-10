using UnityEngine.SceneManagement;


public class FinishState : BaseState
{
    private readonly Animatable _animatable;


    public FinishState(IStateSwitcher stateSwitcher, Animatable animatable) 
        : base(stateSwitcher)
    {
        _animatable = animatable;
    }

    public override void Start()
    {
        _animatable.PlayIdle();
        Stop();
    }

    public override void Stop()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

        StateSwitcher.SwitchState<StartState>();
    }
}
