using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class RegisterUser : MonoBehaviour {
    private const string CREATEUSERURL = "localhost/roargalaxy/insert_user.php";
    private const string SUCESSFUL_REGISTER = "Register sucessfully!";

    private GameObject InputUsername;
    public GameObject InputEmail;
    public GameObject InputPassword;
    public GameObject InputConfirmPassword;
    //public GameObject RegisterButton;

    private string username;
    private string email;
    private string password;
    private string confPassword;

    // Use this for initialization
    void Start () {
        InputUsername = GameObject.Find("InputUsername");
        InputEmail = GameObject.Find("InputEmail");
        InputPassword = GameObject.Find("InputPassword");
        InputConfirmPassword = GameObject.Find("InputConfirmPassword");
    }
	
	// Update is called once per frame
	void Update () {
        
        //RegisterButton = GameObject.Find("RegisterButton");

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (InputUsername.GetComponent<InputField>().isFocused)
                InputEmail.GetComponent<InputField>().Select();
            else if (InputEmail.GetComponent<InputField>().isFocused)
                InputPassword.GetComponent<InputField>().Select();
            else if (InputPassword.GetComponent<InputField>().isFocused)
                InputConfirmPassword.GetComponent<InputField>().Select();
            else if (InputConfirmPassword.GetComponent<InputField>().isFocused)
                InputUsername.GetComponent<InputField>().Select();
        }

        username = InputUsername.GetComponent<InputField>().text;
        email = InputEmail.GetComponent<InputField>().text;
        password = InputPassword.GetComponent<InputField>().text;
        confPassword = InputConfirmPassword.GetComponent<InputField>().text;
        Regex.Replace(password, @"\s+", "");
        Regex.Replace(confPassword, @"\s+", "");

    }

    private IEnumerator createUserInfo(string username, string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("emailPost", email);
        form.AddField("passwordPost", password);
        form.AddField("confPasswordPost", confPassword);
        
        WWW www = new WWW(CREATEUSERURL, form);
        yield return www;

        string serverResponse = www.text.Trim();

        if (serverResponse.Equals(SUCESSFUL_REGISTER))
        {
            SceneManager.LoadScene("Login");
            PlayerPrefs.SetString("username", username);
            PlayerPrefs.SetString("password", password);
            PlayerPrefs.SetInt("containsUserInfo", 1);
        }
        else
            Debug.LogWarning(serverResponse);
    }

    public void registerUserInfo()
    {
        StartCoroutine(createUserInfo(username, email, password));
    }
}
