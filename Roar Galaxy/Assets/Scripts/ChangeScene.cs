﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {

    public void ChangeToScene (string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
