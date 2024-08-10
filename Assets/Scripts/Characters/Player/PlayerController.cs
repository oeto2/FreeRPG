using System.Collections;
using UnityEngine;
using Constants;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerStatus playerStatus; //플레이어 스텟
    private PlayerState _state; //플레이어 상태
    private PlayerAniamtionData _animationData; //플레이어 애니메이션 데이터
    private Animator _animator; //플레이어 애니메이터

    //변수 : 플레이어 공격 관련
    [SerializeField] private float attackDistance; //공격 사정거리
    private float _distanceFromMonster; //몬스터와의 거리
    private GameObject _targetObj; //공격 대상
    public MonsterStatus targetStatus; //공격 대상 스텟
    private bool _attackChance; //공격 찬스
    private WaitForSeconds _attackCoolTime; //공격 대기시간
    
    private void Awake()
    {
        _animationData = new PlayerAniamtionData();
        _animationData.InitializeParameters();
        playerStatus = GetComponent<PlayerStatus>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _attackChance = true; //공격 찬스 부여
        _targetObj = GameManager.Instance.monsterObj;
        _attackCoolTime = new WaitForSeconds(playerStatus.AttackCoolTime); //공격 속도 적용
        targetStatus = _targetObj.GetComponent<MonsterStatus>();
    }

    private void Update()
    {
        //몬스터와의 거리 계산
        _distanceFromMonster = Vector2.Distance(transform.position,_targetObj.transform.position);
        
        //상태에 따른 동작구현
        switch (_state)
        {
            //기본 상태
            case PlayerState.Idle:
                IdleUpdate();
                break;

            //공격 상태
            case PlayerState.Attack:
                AttackUpdate();
                break;
        }
    }

    //기본 상태 로직
    private void IdleUpdate()
    {
        //몬스터가 공격 사정거리안에 들어올 경우 + 공격 가능 + 대상체력이 0이상
        if (_distanceFromMonster < playerStatus.AttackRange && _attackChance && targetStatus.Health > 0)
        { 
            ChangeState(PlayerState.Attack); //공격상태 변경
        }
    }

    //공격 상태 로직
    private void AttackUpdate()
    {
        //공격 중지 : 공격 대상의 체력이 0이하일경우
        if (targetStatus.Health <= 0)
        {
            ChangeState(PlayerState.Idle);
            return;
        }
        
        //공격 가능시
        if (_attackChance)
        {
            StartCoroutine(ApplyAttackCoolTime()); //공격 쿨타임 적용
        }
        
        //공격 쿨타임일 경우
        if (!_attackChance)
        {
            ChangeState(PlayerState.Idle); //기본상태 변경
        }
    }
    
    //상태 변경
    private void ChangeState(PlayerState newState)
    {
        _state = newState;

        switch (_state)
        {
            //애니메이션 변경
            case PlayerState.Idle:
                _animator.SetBool(_animationData.IdleParameterName, true);
                _animator.SetBool(_animationData.AttackParameterName, false);
                break;

            case PlayerState.Attack:
                _animator.SetBool(_animationData.IdleParameterName, false);
                _animator.SetBool(_animationData.AttackParameterName, true);
                
                //공격 애니메이션 속도 조정 (공속 비례)
                _animator.SetFloat(_animationData.AttackSpeedParameterName, playerStatus.AttackSpeed);
                break;
        }
    }

    //공격 쿨타임 적용
    private IEnumerator ApplyAttackCoolTime()
    {
        _attackChance = false; //공격 찬스 초기화
        yield return _attackCoolTime; //다음 공격 대기 시간 적용
        _attackChance = true;
    }
}
