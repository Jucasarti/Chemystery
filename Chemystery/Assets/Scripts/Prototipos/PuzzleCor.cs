using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCor : MonoBehaviour
{
    public int contador = 0;

    private TrocarCor item;

    public TrocarCor[] quadrados;

    public ManagerPlayer player;



    public void ConferirPuzzle () {


        for(int i = 0; i < quadrados.Length; i++) {

            if(quadrados[i].corCerta == true) {

                contador++;
                Debug.Log(contador);

            }

        }


        if(contador == 6) {

            Debug.Log("Terminou o puzzle");
            this.gameObject.SetActive(false);
            player.TravaCamera();

        } else {

            Debug.Log("Nao terminou");
            contador = 0;

        }
    
    }

    

    

}
