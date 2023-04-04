using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuboVazio : MonoBehaviour
{
    //Variável do cooldown
    public bool cooldown;

    //Lados do cubo vazio
    public GameObject lado1, lado2, lado3, lado4;

    public Vector3 posInicial;

    //Atualizando posição do cubo vazio
    public void AtualizaPos(float posX, float posY, float posZ)
    {
        gameObject.transform.position = new Vector3(posX, posY, posZ);
        if (cooldown == false)
        {
            //Invocando Cooldown
            Invoke("ResetCooldown", 0.7f);
            cooldown = true;
        }
    }

    //Iniciando Cooldown
    void ResetCooldown()
    {
        cooldown = false;
    }

    private void Start()
    {
        posInicial = gameObject.transform.position;
    }
}
