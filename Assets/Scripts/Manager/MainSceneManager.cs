using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject uiParentObj; //UI 부모 오브젝트
    
    private void Awake()
    {
        Initialize();
    }

    //게임에 필요한 오브젝트 세팅
    private void Initialize()
    {
        //게임매니저가 존재하지 않다면 생성
        if (GameManager.Instance != null)
            GameManager.Instance.Init();
        
        //오브젝트 부모 설정
        UIManager.Instance.parentsUI = uiParentObj.transform;
        
        //로그인 팝업 띄우기
        UIManager.Instance.ShowPopup<LoginPopup>();
    }
}