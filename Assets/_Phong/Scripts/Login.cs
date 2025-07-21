using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using PimDeWitte.UnityMainThreadDispatcher;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
      public TMP_InputField inputName,inputPass;
      public TMP_InputField nameReg,passReg,playerName;
      [SerializeField]
      private TMP_Text messageText,wellcomeText;
      private FirebaseAuth auth;
      public GameObject registerPanel;
      public void PrintName()
      {
            string username = inputName.text;
            Debug.Log("account : " +username);
            if(string.IsNullOrEmpty(username))
                  return;
            LeaderBoard.Instance.account = username;

            //gameObject.SetActive(false);
            LoginGame();
      }
      
      public void Register()
      {
            string email = nameReg.text;
            string password = passReg.text;

            auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                  if (task.IsCanceled || task.IsFaulted)
                  {
                        Debug.LogWarning("Lỗi đăng ký: " + task.Exception?.Message);
                        UpdateMessage("Đăng ký thất bại. Kiểm tra email & mật khẩu.");
                  }
                  else
                  {
                        FirebaseUser newUser = task.Result.User;
                        Debug.Log("Đăng ký thành công: " + newUser.Email);
                        UpdateMessage("Đăng ký thành công: " + newUser.Email);
                        UnityMainThreadDispatcher.Instance().Enqueue(() =>
                        {
                              GameManager.Instance.Profile = new DataPlayer()
                              {
                                    name = playerName.text,
                                    gold = 10,
                                    currentWave = 0,
                                    maxWave = 0,
                              };
                              string userId = auth.CurrentUser.UserId;
                              var json = JsonConvert.SerializeObject(GameManager.Instance.Profile);
                              FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(userId)
                                    .SetRawJsonValueAsync(json).ContinueWithOnMainThread(t =>
                                    {
                                          Debug.Log("set value: "+t.Exception);
                                    });
                              registerPanel.SetActive(false);
                              inputName.text = nameReg.text;
                              inputPass.text = passReg.text;
                        });
                  }
            });
      }


      void Awake()
      {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                  if (task.Result == DependencyStatus.Available)
                  {
                        auth = FirebaseAuth.DefaultInstance;
                        Debug.Log("init firebase success");
                  }
                  else
                  {
                        Debug.LogError("Firebase không khả dụng: " + task.Result);
                  }
            });
      }

      public void LoginGame()
      {
            string email = inputName.text;
            string password = inputPass.text;

            auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                  if (task.IsCanceled || task.IsFaulted)
                  {
                        Debug.LogWarning("Lỗi đăng nhập: " + task.Exception?.Message);
                        UpdateMessage("Đăng nhập thất bại.");
                  }
                  else
                  {
                        var user = task.Result.User;
                        Debug.Log("Đăng nhập thành công: " + user.Email);
                        UpdateMessage("Chào mừng: " + user.Email);
                        UnityMainThreadDispatcher.Instance().Enqueue(() =>
                        {
                              FirebaseManager.Instance.Init();
                              FirebaseManager.Instance.LoadData(() =>
                              {
                                    string playerName = GameManager.Instance.Profile.name;
                                    wellcomeText.text = $"Chào mừng trở lại {playerName}";
                                    gameObject.SetActive(false);
                              });
                        });

                  }
            });
      }

      void UpdateMessage(string msg)
      {
            // UI phải được cập nhật từ main thread
            UnityMainThreadDispatcher.Instance().Enqueue(() => {
                  messageText.text = msg;
            });
      }

}
