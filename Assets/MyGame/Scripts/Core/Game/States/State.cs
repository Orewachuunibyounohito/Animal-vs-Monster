using UnityEngine;

public abstract class State : MonoBehaviour
{
    #region Field
    [SerializeField] protected AnimationClip _motion;
    protected float startTime;
    public    bool  isComplete{ get; protected set; }
    #endregion

    #region Property
    protected Animator      animator   => GetComponent<Animator>(); 
    protected AnimationClip motion     => _motion;
    public    string        motionName => _motion.name;
    protected float         time       => Time.time-startTime;
    #endregion

    public virtual void Enter(){
        startTime = Time.time;
    }
    public virtual void Do(){}
    public virtual void FixDo(){}
    public virtual void Exit(){}

    // public void Setup( Animator animator ){
    //     _animator = animator;
    // }

}
