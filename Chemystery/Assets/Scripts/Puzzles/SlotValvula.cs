using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotValvula : MonoBehaviour, IInteractable
{
    ManagerPlayer player;

    ManagerPuzzleValvulas puzzleValvulas;

    bool temValvula = false;

    [SerializeField] int qualSlot;

    [SerializeField] GameObject valvulaModelo;


    void Awake () {

        player = FindObjectOfType<ManagerPlayer>();

        puzzleValvulas = FindObjectOfType<ManagerPuzzleValvulas>();

    }

    public void Interagir() {

        if(temValvula) {

            valvulaModelo.SetActive(false);

            player.ColetarValvula();

            TirarValvula();

            temValvula = false;


        } else if (player.numeroDeValvulas > 0) {

            valvulaModelo.SetActive(true);

            player.RetirarValvula();

            ColocarValvula();

            temValvula = true;

        } else {

            //aviso de que nao tem valvulas

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
