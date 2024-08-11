using UnityEngine;
using Firebase.Auth;
using System;

public class FirebaseAuthManager : Singleton<FirebaseAuthManager>
{
    private FirebaseAuth _auth; //로그인, 회원가입에 사용
    private FirebaseUser _user; //인증이 완료된 유저 정보

    public void Init()
    {
        _auth = FirebaseAuth.DefaultInstance;
        _auth.StateChanged += OnChanged; //계정 상태 변경시 호출
    }

    private void OnChanged(object sender_, EventArgs e)
    {
        if (_auth.CurrentUser != _user)
        {
            bool signed = (_auth.CurrentUser != _user && _auth.CurrentUser != null);
            if (!signed && _user != null)
            {
                Logger.Log("로그아웃");
            }

            _user = _auth.CurrentUser;
            if (signed)
            {
                Logger.Log("로그인");
                UIManager.Instance.CloseUIPopup(nameof(LoginPopup)); //로그인창 숨기기
                GameManager.Instance.InitializeObjects(); // 게임시작
            }
        }
    }
    
    public void Create(string id_, string password_)
    {
        _auth.CreateUserWithEmailAndPasswordAsync(id_, password_).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Logger.LogError("회원가입 취소");
                return;
            }

            if (task.IsFaulted)
            {
                Logger.LogError("회원가입 실패");
                return;
            }

            AuthResult authResult = task.Result;
            FirebaseUser newUser = authResult.User;
            
            Logger.LogError("회원가입 완료");
        });
    }

    public void Login(string id_, string password_)
    {
        _auth.SignInWithEmailAndPasswordAsync(id_, password_).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Logger.LogError("로그인 취소");
                return;
            }

            if (task.IsFaulted)
            {
                Logger.LogError("로그인 실패");
                return;
            }

            AuthResult authResult = task.Result;
            FirebaseUser newUser = authResult.User;
            
            Logger.Log("로그인 완료");
            GameManager.Instance.InitializeObjects(); //오브젝트 생성
            
        });
    }

    public void Logout()
    {
        _auth.SignOut();
        Logger.Log("로그아웃");
    }
}
