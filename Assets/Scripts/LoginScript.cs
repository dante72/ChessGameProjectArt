using ChessGameClient.AuthWebAPI;
using System;

using UnityEngine;

public class LoginSqript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject menuComponent;
    public GameObject loginComponent;
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
            var result = await GameClientV2.Client.authService.Autorization(new AccountRequestModel()
            {
                Login = "admin",
                Password = "admin"
            });

            if (result)
            {
                Debug.Log($"Success!");
                loginComponent.SetActive(false);
                menuComponent.SetActive(true);
            }
            else
                Debug.Log($"incorrect login or password!");
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }
    }
}
