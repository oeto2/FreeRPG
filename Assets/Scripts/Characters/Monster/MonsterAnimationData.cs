using UnityEngine;

[System.Serializable]
public class MonsterAnimationData
{
    //변수 : 애니메이션 파라미터
    private string _idleParameterName = "Idle";
    private string _walkParameterName = "Walk";
    private string _attackParameterName = "Attack";
    private string _hitParameterName = "Hit";
    private string _deadParameterName = "Dead";
    
    //프로퍼티 : 애니메이션 파라미터
    public int IdleParameterName { get; private set;}
    public int WalkParameterName { get; private set;}
    public int AttackParameterName { get; private set;}
    public int HitParameterName { get; private set;}
    public int DeadParameterName { get; private set;}
    
    //파라미터 설정
    public void InitializeParameters()
    {
        IdleParameterName = Animator.StringToHash(_idleParameterName);
        WalkParameterName = Animator.StringToHash(_walkParameterName);
        AttackParameterName = Animator.StringToHash(_attackParameterName);
        HitParameterName = Animator.StringToHash(_hitParameterName);
        DeadParameterName = Animator.StringToHash(_deadParameterName);
    }
}
