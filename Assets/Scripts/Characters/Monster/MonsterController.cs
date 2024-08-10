using System;
using UnityEngine;
using Constants;
using UnityEngine.Serialization;

public class MonsterController : MonoBehaviour, IDamagable
{
    private MonsterStatus _monsterStatus; //몬스터 스텟
    [SerializeField] private MonsterState _state; //몬스터 상태
    private GameObject _targetObj; //공격대상 (플레이어)
    private MonsterAnimationData _animationData; //몬스터 애니메이션 데이터
    private Animator _animator; //플레이어 애니메이터

    //변수 : 몬스터 이동 관련
    [SerializeField] private float attackRange; //공격 사정거리
    private float _distanceFromPlayer; //플레이어와의 거리


    private void Awake()
    {
        _animationData = new MonsterAnimationData();
        _animationData.InitializeParameters();
        _monsterStatus = GetComponent<MonsterStatus>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _targetObj = GameManager.Instance.playerObj;

        if (_targetObj == null)
        {
            Logger.LogError("플레이어 오브젝트가 존재하지 않습니다.");
        }
    }

    private void Update()
    {
        //플레이어와의 거리
        _distanceFromPlayer = Vector2.Distance(_targetObj.transform.position, transform.position);
        
        //상태에 따른 동작구현
        switch (_state)
        {
            //기본 상태
            case MonsterState.Idle:
                IdleUpdate();
                break;

            //이동 상태
            case MonsterState.Walk:
                WalkUpdate();
                break;

            //공격 상태
            case MonsterState.Attack:
                AttackUpdate();
                break;

            //피격 상태
            case MonsterState.Hit:
                HitUpdate();
                break;

            //사망 상태
            case MonsterState.Dead:
                DeadUpdate();
                break;
        }
    }

    //기본 상태 로직
    private void IdleUpdate()
    {
        //WalkState 변경 조건
        if (_distanceFromPlayer > attackRange)
        {
            ChangeState(MonsterState.Walk); //몬스터 이동상태 전환
        }
    }

    //이동 상태 로직
    private void WalkUpdate()
    {
        //공격 사정거리 밖
        if (_distanceFromPlayer > attackRange)
        {
            OnMove(); //이동
        }

        //공격 사정거리 안
        else
        {
            ChangeState(MonsterState.Attack);
        }
    }

    //공격 상태 로직
    private void AttackUpdate()
    {
    }

    //피격 상태 로직
    private void HitUpdate()
    {
    }

    //사망 상태 로직
    private void DeadUpdate()
    {
    }

    //상태 변경
    private void ChangeState(MonsterState newState)
    {
        _state = newState;

        switch (_state)
        {
            //애니메이션 변경
            case MonsterState.Idle:
                _animator.SetBool(_animationData.IdleParameterName, true);
                _animator.SetBool(_animationData.WalkParameterName, false);
                _animator.SetBool(_animationData.AttackParameterName, false);
                break;

            case MonsterState.Walk:
                _animator.SetBool(_animationData.IdleParameterName, false);
                _animator.SetBool(_animationData.WalkParameterName, true);
                _animator.SetBool(_animationData.AttackParameterName, false);
                break;

            case MonsterState.Attack:
                _animator.SetBool(_animationData.IdleParameterName, false);
                _animator.SetBool(_animationData.WalkParameterName, false);
                _animator.SetBool(_animationData.AttackParameterName, true);
                break;

            case MonsterState.Hit:
                _animator.SetTrigger(_animationData.HitParameterName);
                _animator.SetBool(_animationData.IdleParameterName, true);
                _animator.SetBool(_animationData.WalkParameterName, false);
                _animator.SetBool(_animationData.AttackParameterName, false);
                break;

            case MonsterState.Dead:
                _animator.SetTrigger(_animationData.DeadParameterName);
                _animator.SetBool(_animationData.IdleParameterName, true);
                _animator.SetBool(_animationData.WalkParameterName, false);
                _animator.SetBool(_animationData.AttackParameterName, false);
                break;
        }
    }

    //이동 로직
    public void OnMove()
    {
        //몬스터 포지션 변경
        transform.position += Vector3.left * (_monsterStatus.Speed * Time.deltaTime);
    }

    public void TakeDamage(float damage_)
    {
        _monsterStatus.Health -= damage_;
        Logger.Log("몬스터가 데미지 받음");
    }
}