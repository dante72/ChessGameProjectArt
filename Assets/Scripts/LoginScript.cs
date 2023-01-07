using ChessGameClient;
using ChessGameClient.AuthWebAPI;
using ChessGameClient.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LoginSqript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void StartGame()
    {
        Debug.Log("Start game");

        try
        {
            var result = await GameClient.Client.authService.Autorization(new AccountRequestModel()
            {
                Login = "admin",
                Password = "admin"
            });

            if (result)
                Debug.Log($"Success!");
            else
                Debug.Log($"incorrect login or password!");
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }
}
