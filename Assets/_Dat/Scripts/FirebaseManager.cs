using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Newtonsoft.Json;
using PimDeWitte.UnityMainThreadDispatcher;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;
    public FirebaseAuth auth;
    private DatabaseReference dbRef;

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        auth = FirebaseAuth.DefaultInstance;
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SetData()
    {
        var json = JsonConvert.SerializeObject(GameManager.Instance.Profile);
        UploadJsonFile(json);
    }

    public void GetLeaderBoardData(Action onComplete)
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("users")
            .OrderByChild("maxWave")
            .LimitToLast(5)
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;


                    foreach (var user in snapshot.Children)
                    {
                        string name = user.Child("name").Value?.ToString() ?? "Unknown";
                        int wave = int.Parse(user.Child("maxWave").Value.ToString());
                        LeaderBoard.Instance.AddData(name,wave);
                    }
                    
                    onComplete?.Invoke();

                }
            });
    }

    public void UploadJsonFile(string json)
    {
        if (!string.IsNullOrEmpty(json))
        {
            
            string userId = auth.CurrentUser.UserId;
            dbRef.Child("users").Child(userId).SetRawJsonValueAsync(json).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Upload JSON thành công!");
                }
                else
                {
                    Debug.LogError("Lỗi khi upload JSON: " + task.Exception);
                }
            });
        }
        else
        {
            Debug.LogError("JSON trống");
        }
    }
    public void LoadData(Action onComplete = null)
    {
        string userId = auth.CurrentUser.UserId;
        dbRef.Child("users").Child(userId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Lỗi khi lấy dữ liệu: " + task.Exception);
                return;
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    string json = snapshot.GetRawJsonValue();
                    var profile = JsonConvert.DeserializeObject<DataPlayer>(json);
                    Debug.Log("Tên người chơi: " + profile.name);
                    Debug.Log("Điểm: " + profile.maxWave);
                    GameManager.Instance.Profile = profile;
                }
                else
                {
                    Debug.Log("Không tìm thấy dữ liệu cho người dùng.");
                    GameManager.Instance.Profile = new DataPlayer()
                    {
                        name = $"User{auth.CurrentUser.UserId}",
                        maxWave = 0,
                        currentWave = 0,
                    };
                    SetData();
                }
                UnityMainThreadDispatcher.Instance().Enqueue(onComplete);
            }
        });
    }
}
