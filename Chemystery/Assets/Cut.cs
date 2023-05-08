using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class Cut : MonoBehaviour
{
    public CinemachineVirtualCamera [] camerasCutscene;
    public GameObject [] cutsceneSmoke;
    public GameObject managerCutscene;
    public ManagerPlayer player;

    public void AtivaCutscene()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        camerasCutscene[0].gameObject.SetActive(false);
        camerasCutscene[1].gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        managerCutscene.SetActive(true);
    }

    public void DesativaCutscene()
    {
        camerasCutscene[0].gameObject.SetActive(true);
        camerasCutscene[1].gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        for (int i = 0; i < cutsceneSmoke.Length; i++)
        {
            cutsceneSmoke[i].SetActive(true);
        }
        managerCutscene.SetActive(false);
    }
}
