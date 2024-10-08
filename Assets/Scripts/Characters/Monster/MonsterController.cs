using System.Collections;
using UnityEngine;
using Constants;

public class MonsterController : MonoBehaviour, IDamagable
{
    [HideInInspector] public MonsterStatus monsterStatus; //몬스터 스텟
    [SerializeField] private MonsterState _state; //몬스터 상태
    private GameObject _targetObj; //공격대상 (플레이어)
    private MonsterAnimationData _animationData; //몬스터 애니메이션 데이터
    private Animator _animator; //몬스터 애니메이터
    private MonsterUI _monsterUI;

    //변수 : 몬스터 이동 관련
    [SerializeField] private float attackRange; //공격 사정거리
    private float _distanceFromPlayer; //플레이어와의 거리
    
    //변수 : 몬스터 사망 관련
    private WaitForSeconds _monsterFadeTime; //몬스터 시체 사라지는 시간
    private bool _isDead = false; //몬스터 사망 Flag
    
    //변수 : 몬스터 리셋 관련
    private Vector3 _monsterSpawnPos;

    private void Awake()
    {
        _animationData = new MonsterAnimationData();
        _animationData.InitializeParameters();
        monsterStatus = GetComponent<MonsterStatus>();
        _animator = GetComponentInChildren<Animator>();
        _monsterUI = GetComponent<MonsterUI>();
    }

    private void Start()
    {
        _monsterSpawnPos = transform.position; //몬스터 초기 스폰위치 저장
        _monsterFadeTime = new WaitForSeconds(1.5f); //몬스터 시체 사라지는 시간 (임시)
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
        //체력이 0 이하라면 사망
        if (monsterStatus.Health <= 0)
        {
            ChangeState(MonsterState.Dead);
            return;
        }
        
        //WalkState 변경 조건
        if (_distanceFromPlayer > attackRange)
        {
            ChangeState(MonsterState.Walk); //몬스터 이동상태 전환
        }
    }

    //이동 상태 로직
    private void WalkUpdate()
    {
        //체력이 0 이하라면 사망
        if (monsterStatus.Health <= 0)
        {
            ChangeState(MonsterState.Dead);
            return;
        }
        
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
        //체력이 0 이하라면 사망
        if (monsterStatus.Health <= 0)
        {
            ChangeState(MonsterState.Dead);
            return;
        }
    }

    //피격 상태 로직
    private void HitUpdate()
    {
    }

    //사망 상태 로직
    private void DeadUpdate()
    {
        //중복 실행 방지
        if (!_isDead)
            StartCoroutine(MonsterDead()); //몬스터 사망처리
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
        transform.position += Vector3.left * (monsterStatus.Speed * Time.deltaTime);
    }

    //데미지 받기
    public void TakeDamage(float damage_)
    {
        monsterStatus.Health -= damage_; //데미지 처리
        //최소 체력 적용
        if (monsterStatus.Health < 0) 
            monsterStatus.Health = 0;
        
        // Logger.Log($"몬스터가 {damage_}의 데미지 받음, 남은 체력 : {monsterStatus.Health}");
        _monsterUI.UpdateMonsterHPUI(); //체력 UI 업데이트
    }
    
    //몬스터 사망시
    private IEnumerator MonsterDead()
    {
        _isDead = true;
        yield return _monsterFadeTime; //사망시간 딜레이 
        gameObject.SetActive(false); //오브젝트 비활성화
        _isDead = false;
        ResetMonster(); //몬스터 리셋
        GameManager.Instance.CallMonsterSpawnEvent(); //몬스터 소환
    }
    
    //몬스터 리셋 (재사용)
    private void ResetMonster()
    {
        transform.position = _monsterSpawnPos; //초기 위치로 되돌리기
        ChangeState(MonsterState.Idle); //기본상태 전환
        monsterStatus.Initialize_MonsterData(); //몬스터 스텟 초기화
        _monsterUI.UpdateMonsterHPUI(); //체력 UI 업데이트
    }
}