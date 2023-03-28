using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCuboVazio : MonoBehaviour
{
    //Vari�vel que ir� verificar a posi��o correta
    public bool corretoV;


    private void OnTriggerStay(Collider correto)
    {
        if (correto.CompareTag("PosCuboVazio"))
        {
            //Caso esteja na posi��o certa, a vari�vel ganha o valor de verdadeiro
            corretoV = true;
        }
    }

    private void OnTriggerExit(Collider correto)
    {
        if (correto.CompareTag("PosCuboVazio"))
        {
            //Caso esteja na posi��o certa, a vari�vel ganha o valor de falso
            corretoV = false;
        }
    }
}
