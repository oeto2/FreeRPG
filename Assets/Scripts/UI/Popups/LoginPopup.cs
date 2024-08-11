using TMPro;
using UnityEngine;

public class LoginPopup : UIBase
{
    public TMP_InputField idInput;
    public TMP_InputField pwInput;

    void Start()
    {
        FirebaseAuthManager.Instance.Init();
    }

    public void Create()
    {
        string id = idInput.text;
        string password = pwInput.text;
        
        FirebaseAuthManager.Instance.Create(id,password);
    }

    public void Login()
    {
        string id = idInput.text;
        string password = pwInput.text;
        
        FirebaseAuthManager.Instance.Login(id,password);
    }

    public void Logout()
    {
        FirebaseAuthManager.Instance.Logout();
    }
}
