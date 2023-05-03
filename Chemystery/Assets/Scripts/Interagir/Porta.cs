using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour, IInteractable
{
    private bool pegouChave = false;

    private Aviso aviso;

    void Awake () {

        aviso = FindObjectOfType<Aviso>();

    }


    public void Interagir() {

        if(pegouChave) {

            Destroy(gameObject);

        } else {

            aviso.AvisoDaPorta();

        }

    }


    public void DestrancarPorta() {

        pegouChave = true;

    }



}
