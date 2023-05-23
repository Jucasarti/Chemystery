using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class Cut : MonoBehaviour
{
    public CinemachineVirtualCamera [] camerasCutscene;
    public GameObject cutsceneSmoke;
    public GameObject managerCutscene;
    public ManagerPlayer player;

    [SerializeField] Chave chave;


    public void AtivaCutscene()
    {
        Invoke("MutandoSoundtrack", 0.1f);
        gameObject.GetComponent<Collider>().enabled = false;
        //camerasCutscene[0].gameObject.SetActive(false);
        //camerasCutscene[1].gameObject.SetActive(true);
        player.gameObject.transform.position = new Vector3(76.5200043f, player.gameObject.transform.position.y, 35.159996f);
        player.gameObject.SetActive(false);  
        managerCutscene.SetActive(true);
    }

    public void DesativaCutscene()
    {
        cutsceneSmoke.SetActive(true);
        player.gameObject.SetActive(true);
        //camerasCutscene[0].gameObject.SetActive(true);
        //camerasCutscene[1].gameObject.SetActive(false);
        managerCutscene.SetActive(false);

        chave.PegarChave();
    }
}
