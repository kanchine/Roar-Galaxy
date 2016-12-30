using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

    private const string SUCESSFUL_LOGIN = "Succeed";
    private const string LOGINEURL = "localhost/roargalaxy/login.php";

    public GameObject InputUsername;
    public GameObject InputEmail;
    public GameObject InputPassword;

    private string username;
    //private string email;
    private string password;

	// Use this for initialization
	void Start () {
        InputUsername = GameObject.Find("InputUsername");
        InputPassword = GameObject.Find("InputPassword");
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Tab))
        {
            if (InputUsername.GetComponent<InputField>().isFocused)
                InputPassword.GetComponent<InputField>().Select();
            else if (InputPassword.GetComponent<InputField>().isFocused)
                InputUsername.GetComponent<InputField>().Select();
        }

        if (PlayerPrefs.HasKey("containsUserInfo") && PlayerPrefs.GetInt("containsUserInfo") == 1)
        {
            InputField usernameRef = InputUsername.GetComponent<InputField>();
            usernameRef.text = PlayerPrefs.GetString("username");
            InputField passwordref = InputPassword.GetComponent<InputField>();
            passwordref.text = PlayerPrefs.GetString("password");
        }

        username = InputUsername.GetComponent<InputField>().text;
        password = InputPassword.GetComponent<InputField>().text;
       
    }

    private IEnumerator LoginToRG(string username,string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("passwordPost", password);

        WWW www = new WWW(LOGINEURL, form);
        yield return www;

        string serverResponse = www.text.Trim();

        if (serverResponse.Equals(SUCESSFUL_LOGIN))
        {
            if (!PlayerPrefs.HasKey("containsUserInfo") || (PlayerPrefs.HasKey("containsUserInfo") && PlayerPrefs.GetInt("containsUserInfo") == 0))
            {
                PlayerPrefs.SetString("username", username);
                PlayerPrefs.SetString("password", password);
                PlayerPrefs.SetInt("containsUserInfo", 1);
            }
            SceneManager.LoadScene("RoarGalaxy");
        }
        else
            Debug.LogWarning(serverResponse);
    }

    public void login()
    {
        StartCoroutine(LoginToRG(username, password));
    }
}
