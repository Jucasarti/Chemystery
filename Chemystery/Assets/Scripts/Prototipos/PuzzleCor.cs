using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCor : MonoBehaviour
{
    public int contador = 0;

    private TrocarCor item;

    public TrocarCor[] quadrados;

    public void ConferirPuzzle () {


        for(int i = 0; i < quadrados.Length; i++) {

            if(quadrados[i].corCerta == true) {

                contador++;
                Debug.Log(contador);

            }

        }


        if(contador >= 6) {

            Debug.Log("Terminou o puzzle");

        } else {

            Debug.Log("Nao terminou");
            contador = 0;

        }
    


    }

    void Update() {

    if (Input.GetMouseButtonDown(0)) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.gameObject.CompareTag("ItemCor")) {

                    item = hit.collider.GetComponent<TrocarCor>();
                    item.TrocaCor();
                }
            }       

        }

    }

}
