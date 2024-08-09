using UnityEngine;
using Constants;

public class MonsterController : MonoBehaviour
{ 
    private MonsterStatus _monsterStatus; //몬스터 스텟
    private MonsterState _state; //몬스터 상태
    private GameObject _playerObj; //플레이어 오브젝트
    private MonsterAnimationData _animationData; //몬스터 애니메이션 데이터
    private Animator _animator; //플레이어 애니메이터
    
    //변수 : 몬스터 이동 관련
    [SerializeField] private float attackDistance; //공격 사정거리
    private float _distanceFromPlayer; //플레이어와의 거리
    

    private void Awake()
    {
        _playerObj = GameManager.Instance.playerObj;
        _animationData = new MonsterAnimationData();
        _animationData.InitializeParameters();
        _monsterStatus = GetComponent<MonsterStatus>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //플레이어와의 거리
        _distanceFromPlayer = Vector2.Distance(_playerObj.transform.position, transform.position);
        
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
        }
    }

    //기본상태 동작
    private void IdleUpdate()
    {
        //플레이어가 공격사정거리 안으로 들어오지 않으면 이동
        if (_distanceFromPlayer < attackDistance)
        {
            ChangeState(MonsterState.Walk); //몬스터 이동상태 전환
        }
    }
    
    //이동상태 동작
    private void WalkUpdate()
    {
        
    }
    
    //공격상태 동작
    private void AttackUpdate()
    {
        
    }
    
    //피격상태 동작
    private void HitUpdate()
    {
        
    }
    
    //상태 변경
    private void ChangeState(MonsterState newState)
    {
        _state = newState;
        
        switch (_state)
        {
            case MonsterState.Idle:
                _animator.SetBool(_animationData.IdleParameterName, true);
                break;
            
            case MonsterState.Walk:
                _animator.SetBool(_animationData.WalkParameterName, true);
                break;
            
            case MonsterState.Attack:
                _animator.SetBool(_animationData.AttackParameterName, true);
                break;
            
            case MonsterState.Hit:
                _animator.SetBool(_animationData.HitParameterName, true);
                break;
        }
    }
}