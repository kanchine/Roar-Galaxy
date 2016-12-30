using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CreatePlanet : MonoBehaviour {

    private const string CREATE_PLANET_URL = "localhost/roargalaxy/insert_planet_data.php";
    private const string servername = "localhost";
    private const string server_username = "root";
    private const string server_password = "Tcw@388254";
    private const string stringdbName = "Roar_Galaxy";

    new AudioSource audio;

    void Start () {
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private IEnumerator CreatePlanetdata(byte[] roardata)
    {

        WWWForm form = new WWWForm();
        form.AddField("usernamePost", PlayerPrefs.GetString("username"));

        string byteString = Convert.ToBase64String(roardata, Base64FormattingOptions.None);
        //string byteString = Encoding.UTF8.GetString(roardata);
        form.AddField("filesourcePost", byteString);

        WWW www = new WWW(CREATE_PLANET_URL, form);

        yield return www;

        string response = www.text.Trim();
        //Debug.Log(response);
    }

    public void Roar()

    {
        AudioClip clip = Microphone.Start(null, false, 10, 44100);
        
        audio.clip = clip;
    }

    public void StopRoar()
    {
        Microphone.End(null);

        float[] samples = new float[audio.clip.samples * audio.clip.channels];
        audio.clip.GetData(samples, 0);

        byte[] byteArray = new byte[samples.Length * 4];
        Buffer.BlockCopy(samples, 0, byteArray, 0, byteArray.Length);

        audio.Play();

        StartCoroutine(CreatePlanetdata(byteArray));
    }
}
