using ChessGameClient.AuthWebAPI;
using ChessGameClient.Services;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject loginComponent;
    public GameObject menuComponent;
    private bool visible = true;
    public static bool flag = false;

    public static bool closeMenu = false;
    internal static bool HasActiveSession = false;
    public TextMeshProUGUI errorMessage;
    public Button StartGame;
    public Button GiveUp;
    public Button Exit;

    void Start()
    {
        errorMessage.text = string.Empty;
        GiveUp.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || closeMenu)
        {
            closeMenu = false;

            visible = !visible;
            menuComponent.SetActive(visible);
        }

        if (HasActiveSession)
            IsActiveSession();
        else
            IsNotActiveSession();
    }


    internal void IsNotActiveSession()
    {
        StartGame.gameObject.SetActive(true);
        GiveUp.gameObject.SetActive(false);
        Exit.gameObject.SetActive(true);
    }

    internal void IsActiveSession()
    {
        StartGame.gameObject.SetActive(false);
        GiveUp.gameObject.SetActive(true);
        Exit.gameObject.SetActive(false);
    }

    public async void NewGame()
    {
        Debug.Log("Start game");

        if (!GameClientV2.Client.gameHubService.IsConnected)
        {
            if (!await GameClientV2.Client.gameHubService.InitConnection())
            {
                Debug.Log("No connection!");
                errorMessage.text = "No connection!";
                return;
            }
        }

        if (await GameClientV2.Client.authWebApi.SessionExists())
        {
            await GameClientV2.Client.gameHubService.GetBoard();
            IsActiveSession();
        }
        else
        {
            var status = await GameClientV2.Client.authWebApi.AddOrRemovePlayer();

            if (status)
            {
                Debug.Log("Search!");
                errorMessage.text = "Search!";
            }
            else
            {
                Debug.Log("Stop search!");
                errorMessage.text = "";
            }
        }
    }

    public async void GameOver()
    {
        await GameClientV2.Client.gameHubService.GameOver();
        HasActiveSession = false;
    }

    public async void LogOut()
    {
        await GameClientV2.Client.authService.LogOut();
        IsNotActiveSession();
        loginComponent.SetActive(true);
        menuComponent.SetActive(false);

    }

    public void StartScene()
    {
        //await GameClientV2.Client.gameHubService.GetBoard();
        SceneManager.LoadScene(1);
    }

}
