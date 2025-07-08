using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
      public TMP_InputField inputName;

      public void PrintName()
      {
            string username = inputName.text;
            Debug.Log("account : " +username);
            LeaderBoard.Instance.account = username;
            gameObject.SetActive(false);
      }

      
      
      
}
