using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pecas : MonoBehaviour
{
    #region Variaveis

    [Header("Posição da Peça")]
    public Vector3 posReset;
    public Vector3 posLevantada;
    public Vector3 posAbaixada;
    public Vector3 posSair;

    [Header("Altura da Peça")]
    [SerializeField] float alturaBaixo;
    [SerializeField] float alturaAlto;

    [Header("Outros")]
    public Selecionado selecionado;
    public ManagerXadrez managerXadrez;
    public GameObject[] casasCerta;
    public GameObject casaAtual;

    [Header("Booleanas")]
    public bool levantado;
    private bool acertou;

    #endregion

    private void Awake()
    {
        posReset = gameObject.transform.position;
        alturaBaixo = gameObject.transform.position.y;
        alturaAlto = 1.126f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CasaXadrez"))
        {
            casaAtual = other.GetComponent<GameObject>();
            for (int i = 0; i < casasCerta.Length; i++)
            {
                if (other.gameObject == casasCerta[i])
                {
                    acertou = true;
                    managerXadrez.VerificaFim(acertou);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CasaXadrez"))
        {
            if (acertou)
            {
                acertou = false;
                managerXadrez.VerificaFim(acertou);
            }
        }
    }

    public void AtualizaPos()
    {
        if (!levantado)
        {
            posAbaixada = selecionado.pecaSelecionada.transform.position;
            posLevantada = new Vector3(selecionado.casa.transform.position.x,
                        alturaAlto, selecionado.casa.transform.position.z);
            levantado = true;
        }
        else if(levantado && selecionado.sair)
        {
            posLevantada = selecionado.pecaSelecionada.transform.position;
            posAbaixada = new Vector3(selecionado.casaAnterior.transform.position.x,
                        alturaBaixo, selecionado.casaAnterior.transform.position.z);
            levantado = false; 
        }
        else
        {
            posLevantada = selecionado.pecaSelecionada.transform.position;
            posAbaixada= new Vector3(selecionado.casa.transform.position.x,
                         alturaBaixo, selecionado.casa.transform.position.z);
            levantado= false;
        }
    }

}
