using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Documento : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject docUI;
    private ManagerPlayer player;

    private Crosshair crosshair;

    private bool docAtivado = false;

    public void Interagir() {

        docUI.SetActive(true);

        player.TravaCamera();

        docAtivado = true;

        crosshair.DesativarCrosshair();

    }

    void Awake() {

        player = FindObjectOfType<ManagerPlayer>();

        crosshair = FindObjectOfType<Crosshair>();

    }


    void Update() {

        if(docAtivado && MenuPausa.jogoPausado == false) {

            if(Input.GetKeyDown(KeyCode.Space)) {

                docUI.SetActive(false);

                player.TravaCamera();

                docAtivado = false;

                crosshair.AtivarCrosshair();

            }

        }


    }


    
}
