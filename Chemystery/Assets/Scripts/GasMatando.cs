using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMatando : MonoBehaviour
{
    ManagerPlayer player;

    void Awake () {

        player = FindObjectOfType<ManagerPlayer>();

    }

    void OnTriggerEnter (Collider other) {

        if(other.CompareTag("Player")) {

            player.Morrer();

        }


    }



}
