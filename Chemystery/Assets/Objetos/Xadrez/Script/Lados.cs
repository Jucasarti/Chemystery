using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lados : MonoBehaviour
{
    public bool podeAndar;

    //Verificando se o cubo permanece em contato com o cubo vazio
    private void OnTriggerStay(Collider outro)
    {
        if (outro.CompareTag("CasaXadrez"))
        {
            //E alterando o valor
            podeAndar = true;
        }
    }
    //Verificando se o cubo deixou de ter contato com o cubo vazio
    private void OnTriggerExit(Collider outro)
    {
        if (outro.CompareTag("CasaXadrez"))
        {
            //E alterando o valor
            podeAndar = false;
        }
    }
}
