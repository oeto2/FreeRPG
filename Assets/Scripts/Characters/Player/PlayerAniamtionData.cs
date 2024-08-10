using UnityEngine;
[System.Serializable]
public class PlayerAniamtionData
{
    //변수 : 애니메이션 파라미터
    private string _idleParameterName = "Idle";
    private string _attackParameterName = "Attack";
    private string _hitParameterName = "Hit";
    private string _attackSpeedParameterName = "AttackSpeed";

    //프로퍼티 : 애니메이션 파라미터
    public int IdleParameterName { get; private set; }
    public int AttackParameterName { get; private set; }
    public int HitParameterName { get; private set; }
    public int AttackSpeedParameterName { get; private set; }


    //파라미터 설정
    public void InitializeParameters()
    {
        IdleParameterName = Animator.StringToHash(_idleParameterName);
        AttackParameterName = Animator.StringToHash(_attackParameterName);
        HitParameterName = Animator.StringToHash(_hitParameterName);
        AttackSpeedParameterName = Animator.StringToHash(_attackSpeedParameterName);
    }
}