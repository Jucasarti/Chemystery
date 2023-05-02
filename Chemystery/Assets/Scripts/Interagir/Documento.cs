using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Documento : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject docUI;
    private ManagerPlayer player;

    private bool docAtivado = false;

    public void Interagir() {

        docUI.SetActive(true);

        player.TravaCamera();

        docAtivado = true;

    }

    void Awake() {

        player = FindObjectOfType<ManagerPlayer>();

    }


    void Update() {

        if(docAtivado) {

            if(Input.GetKeyDown(KeyCode.Space)) {

                docUI.SetActive(false);

                player.TravaCamera();

                docAtivado = false;

            }

        }


    }


    
}
