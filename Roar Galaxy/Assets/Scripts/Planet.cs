using System;
using UnityEngine;
using UnityEngine.UI;

public class Planet{

    public AudioClip clip;

    private string id;
    private string sound;
    private string user;
    private Coordinate coor;
    private GameObject s;

    private int position = 0;
    private int samplerate = 44100;
    private float frequency = 10;

    public Planet(string id, string sound, string user, Coordinate coor)
    {
        this.id = id;
        this.sound = sound;
        this.user = user;
        this.coor = coor;
        this.s = CreateGameObject();
    }

    public string GetID()
    {
        return id;
    }

    public GameObject GetPlanetForRender()
    {
        return s;
    }

    public void PlaySound()
    {
        //Recreate float array and test again
        byte[] byteArray = Convert.FromBase64String(GetSoundString());
        float[] floatArray = new float[byteArray.Length / 4];
        Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);

        GetAudioSource().clip.SetData(floatArray, 0);
        GetAudioSource().Play();
    }

    private string GetSoundString()
    {
        return sound;
    }

    private GameObject CreateGameObject()
    {
        GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        s.transform.position = new Vector3(coor.GetX(), coor.GetY(), coor.GetZ());

        clip = AudioClip.Create("Roar", samplerate * 4, 1, samplerate, false);
        s.AddComponent<AudioSource>().clip = clip;
        s.AddComponent<SphereCollider>().radius = 1f;
        s.AddComponent<ClickToPlaySound>().SetPlanet(this);
 
        return s;
    }
    

    private AudioSource GetAudioSource()
    {
        return s.GetComponent<AudioSource>();
    }
}
