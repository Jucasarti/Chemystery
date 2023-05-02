using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour, IInteractable
{
    private bool pegouChave = false;


    public void Interagir() {

        if(pegouChave) {

            Destroy(gameObject);

        }

    }


    public void DestrancarPorta() {

        pegouChave = true;

    }



}
