using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MainSceneManager : MonoBehaviour
{
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
    }
}