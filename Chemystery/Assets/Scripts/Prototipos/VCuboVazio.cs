using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCuboVazio : MonoBehaviour
{
    //Variável que irá verificar a posição correta
    public bool corretoV;


    private void OnTriggerStay(Collider correto)
    {
        if (correto.CompareTag("PosCuboVazio"))
        {
            //Caso esteja na posição certa, a variável ganha o valor de verdadeiro
            corretoV = true;
        }
    }

    private void OnTriggerExit(Collider correto)
    {
        if (correto.CompareTag("PosCuboVazio"))
        {
            //Caso esteja na posição certa, a variável ganha o valor de falso
            corretoV = false;
        }
    }
}
