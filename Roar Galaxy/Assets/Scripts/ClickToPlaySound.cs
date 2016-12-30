using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToPlaySound : MonoBehaviour {
    private Planet p;

    void OnMouseDown()
    {
        p.PlaySound();
    }

    public void SetPlanet(Planet p)
    {
        this.p = p;
    }
}
