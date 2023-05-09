using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotValvula : MonoBehaviour, IInteractable
{
    ManagerPlayer player;

    ManagerPuzzleValvulas puzzleValvulas;

    bool temValvula = false;

    [SerializeField] int qualSlot;

    MeshRenderer valvulaModelo;

    Aviso aviso;

    AudioSource source;


    void Awake () {

        player = FindObjectOfType<ManagerPlayer>();

        puzzleValvulas = FindObjectOfType<ManagerPuzzleValvulas>();

        valvulaModelo = GetComponent<MeshRenderer>();

        aviso = FindObjectOfType<Aviso>();

        source = GetComponent<AudioSource>();
    }

    public void Interagir() {

        if(temValvula) {

            valvulaModelo.enabled = false;

            player.ColetarValvula();

            TirarValvula();

            temValvula = false;

            source.PlayOneShot(source.clip);


        } else if (player.numeroDeValvulas > 0) {

            valvulaModelo.enabled = true;

            player.RetirarValvula();

            ColocarValvula();

            temValvula = true;

            source.PlayOneShot(source.clip);

        } else {

            //aviso de que nao tem valvulas
            aviso.AvisoDaValvula();

        }

    }

    void ColocarValvula () {

        switch (qualSlot) {

            case 1:
            puzzleValvulas.ColocarValvula1();

            break;

            case 2:
            puzzleValvulas.ColocarValvula2();

            break;

            case 3:
            puzzleValvulas.ColocarValvula3();

            break;

            case 4:
            puzzleValvulas.ColocarValvula4();

            break;

        }
    }

    void TirarValvula() {

        switch (qualSlot) {

            case 1:
            puzzleValvulas.TirarValvula1();

            break;

            case 2:
            puzzleValvulas.TirarValvula2();

            break;

            case 3:
            puzzleValvulas.TirarValvula3();

            break;

            case 4:
            puzzleValvulas.TirarValvula4();

            break;

        }
    }


}
