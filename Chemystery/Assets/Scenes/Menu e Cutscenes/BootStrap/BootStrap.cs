using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootStrap : MonoBehaviour
{
    public float tempo;
    
    void Update()
    {
        tempo += Time.deltaTime;

        if (tempo >= 13f)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
