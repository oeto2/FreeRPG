using System;
using UnityEngine;
using Constants;

public class PlayerAttack : MonoBehaviour
{
    private PlayerController playerController;
    private Collider2D[] _attackColider = new Collider2D[10]; //공격 감지 콜라이더

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    //공격 로직 (애니메이터 사용)
    private void OnAttack()
    {
        PlayerStatus _playerStatus = playerController.playerStatus;
        
        //데미지 처리 콜라이더 생성
        int colidersNum = Physics2D.OverlapCircleNonAlloc(transform.position, _playerStatus.AttackRange, _attackColider);
        
        if (_attackColider != null)
        {
            for (int i=0; i< colidersNum; i++)
            {
                IDamagable iDamagable; //인터페이스 : 데미지 처리
                bool isHave = _attackColider[i].TryGetComponent(out iDamagable); 

                //충돌 레이어가 몬스터일 경우에만 데미지 적용
                if (isHave && _attackColider[i].gameObject.layer == Layer.MonsterLayerNum) 
                { 
                    //플레이어의 공격력 만큼 데미지 처리
                    iDamagable.TakeDamage(_playerStatus.Damage);
                    //타겟 몬스터의 정보를 가져옴
                    playerController.targetStatus = _attackColider[i].GetComponent<MonsterStatus>();
                }
            }
        }
    }
}
