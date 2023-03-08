using ChessGameClient.AuthWebAPI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginSqript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject menuComponent;
    public GameObject loginComponent;
    public TextMeshProUGUI errorMessage;
    public TMP_InputField login;
    public TMP_InputField password;
    internal static bool close = true;

    void Start()
    {
        errorMessage.text = string.Empty;
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
                Login = login.text.ToString(),
                Password = password.text.ToString()
            });

            if (result)
            {
                Debug.Log($"Success!");
                loginComponent.SetActive(false);
                menuComponent.SetActive(true);
            }
            else
            {
                Debug.Log($"Incorrect login or password! {login.text}, {password.text}");
                SetErrorMessage("Incorrect login or password!");
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
            SetErrorMessage(ex.Message);
        }
    }

    private void SetErrorMessage(string message)
    {
        errorMessage.text = message;
    }

    public void Exit() => Application.Quit();
}
