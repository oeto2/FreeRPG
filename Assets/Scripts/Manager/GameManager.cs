using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Constants;

//스테이지 정보
[System.Serializable]
public class Stage
{
    public int StageNum = 1; //현재 스테이지 번호
    public int CurSpawnMonsterNum; //현재 소환한 몬스터 갯수
    public List<string> Stage_MonsterPrefabPath; //소환할 몬스터의 프리팹 주소

    public Stage()
    {
        Stage_MonsterPrefabPath = new List<string>();
    }
}

public class GameManager : Singleton<GameManager>
{
    public GameObject playerObj; //플레이어 오브젝트
    public GameObject monsterObj; //몬스터 오브젝트
    public MonsterStatus targetMonsterStatus; //공격 타겟 몬스터 스텟
    
    //변수 : 스테이지 관련
    [SerializeField] private Stage _stage;
    
    //변수 : 몬스터 스폰 관련
    private Dictionary<Monsters, string> _allMonsters; //Key : 이름, Value : 프리팹 주소
    
    //이벤트 : 게임 시스템관련
    public event Action MonsterSpawnEvent;
    
    private void Awake()
    {
        _stage = new Stage();
        _allMonsters = new Dictionary<Monsters, string>();
    }

    private void Start()
    {
        MonsterSpawnEvent += SpawnMonster; //몬스터 소환 이벤트 구독
        
        GetAllMonstersData(); //모든 몬스터 데이터 가져오기
        // InitializeObjects(); //오브젝트 세팅
    }

    //게임 진행에 필요한 오브젝트 생성
    public void InitializeObjects()
    {
        UIManager.Instance.ShowPopup<MainPopup>(); //메인팝업 생성
        SetStage1_Monster(); //스테이지 1 몬스터 세팅
        playerObj = ResourceManager.Instance.Instantiate(PrefabsPath.PlayerPrefabPath); //플레이어 생성
        SpawnMonster(); //몬스터 생성
    }
    
    //모든 몬스터 목록 가져오기
    private void GetAllMonstersData()
    {
        //몬스터 CSV파일 읽기
        string[] allLines = File.ReadAllLines(Application.dataPath + CSVPath.MonsterCSVPath);
        
        for (int i = 1; i < allLines.Length; i++)
        {
            string[] splitData = allLines[i].Split(',');
            string reName = splitData[0].Replace(" ", ""); //공백 제거

            //가져온 string 값 enum으로 변환
            Monsters monsters = (Monsters)Enum.Parse(typeof(Monsters), reName);

            //enum값에 따라 AllMonsters 작성
            switch (monsters)
            {
                case Monsters.Skeleton : 
                    _allMonsters.Add(monsters,PrefabsPath.SkeletonPrefabPath);
                    break;
                
                case Monsters.EliteOrc : 
                    _allMonsters.Add(monsters,PrefabsPath.EliteOrcPrefabPath);
                    break;
                
                case Monsters.Wizard : 
                    _allMonsters.Add(monsters,PrefabsPath.WizardPrefabPath);
                    break;
                
                case Monsters.Werebear : 
                    _allMonsters.Add(monsters,PrefabsPath.WerebearPrefabPath);
                    break;
                
                case Monsters.Orcrider : 
                    _allMonsters.Add(monsters,PrefabsPath.OrcRiderPrefabPath);
                    break;
            }
        }
    }
    
    //몬스터 스폰 이벤트 호출
    public void CallMonsterSpawnEvent()
    {
        MonsterSpawnEvent?.Invoke();
    }
    
    //몬스터 소환
    public void SpawnMonster()
    {
        //마지막 몬스터 순서까지 돌았다면, 처음 순서부터 다시
        if (_stage.CurSpawnMonsterNum >= _stage.Stage_MonsterPrefabPath.Count)
            _stage.CurSpawnMonsterNum = 0;
        
        int index = _stage.CurSpawnMonsterNum; //소환할 몬스터 인덱스
        monsterObj = PoolManager.Instance.SpawnFromPool(_stage.Stage_MonsterPrefabPath[index]); //몬스터 소환
        targetMonsterStatus = monsterObj.GetComponent<MonsterStatus>();
        _stage.CurSpawnMonsterNum ++; //몬스터 소환 횟수 증가
    }
    
    //스테이지 1 몬스터 설정
    private void SetStage1_Monster()
    {
        //모든 몬스터 리스트 순서대로 설정
        foreach (var monster in _allMonsters)
        {
            _stage.Stage_MonsterPrefabPath.Add(monster.Value);
        }
    }
}