using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Documento : MonoBehaviour, IInteractable
{
    [SerializeField] private Image docUI;

    [SerializeField] private Sprite sprite;
    private ManagerPlayer player;

    private Crosshair crosshair;

    private bool docAtivado = false;

    public void Interagir() {

        docUI.sprite = sprite;

        docUI.gameObject.SetActive(true);

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

                docUI.gameObject.SetActive(false);

                player.TravaCamera();

                docAtivado = false;

                crosshair.AtivarCrosshair();

            }

        }


    }


    
}
