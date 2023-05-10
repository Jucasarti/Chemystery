using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCena : MonoBehaviour
{
    public GameObject[] cutScenes;
    public string cena;
    int i;
    //public CinemachineVirtualCamera[] cameras;

    // Start is called before the first frame update
    void Start()
    {
        FadeOut();
    }
    public void FadeIn()
    {
        cutScenes[0].SetActive(true);
        i = 0;
        Invoke("LoadScene", 5f);
    }

    public void FadeOut()
    {
        cutScenes[1].SetActive(true);
        i = 1;
    }

    public void SairCutscene()
    {
        cutScenes[i].SetActive(false);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(cena);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FadeIn();
        }
    }
}

