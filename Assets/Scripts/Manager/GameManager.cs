using UnityEngine;
using Constants;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerObj; //플레이어 오브젝트
    public GameObject monsterObj; //몬스터 오브젝트

    private void Awake()
    {
        InitializeObjects(); //오브젝트 세팅
    }

    //게임 진행에 필요한 오브젝트 생성
    private void InitializeObjects()
    {
        playerObj = ResourceManager.Instance.Instantiate(PrefabsPath.PlayerPrefabPath); //플레이어 프리팹 생성
        monsterObj = ResourceManager.Instance.Instantiate(PrefabsPath.SkeletonPrefabPath); //스켈레톤 프리팹 생성
    }
}