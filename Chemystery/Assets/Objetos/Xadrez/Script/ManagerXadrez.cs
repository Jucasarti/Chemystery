using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public class ManagerXadrez : MonoBehaviour
{
    private bool cooldown;
    public bool fim;
    private float pecasCertas;
    Selecionado selecionado;
    Collider colisao;
    public Pecas[] pecas;

    private void Awake()
    {
        selecionado = FindObjectOfType<Selecionado>();
        selecionado.casa = FindObjectOfType<MeshRenderer>();
        selecionado.pecaSelecionada = FindObjectOfType<GameObject>();
        colisao = gameObject.GetComponent<Collider>();
        DesativaSelecionado();
        fim = false;
    }

    private void Update()
    {
        if (cooldown)
        {
            DesabilitaPuzzleXadrez();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            AtivaSelecionado();
        }
    }

    public void AtivaSelecionado()
    {
        selecionado.gameObject.SetActive(true);
        selecionado.gameObject.GetComponent<MeshRenderer>().material.color = selecionado.cores[0];
        if (fim)
        {
            ResetarPeças();
            fim = false;
            DesativaSelecionado();
        }
        selecionado.gameObject.transform.position = selecionado.posInicial;
    }

    public void DesativaSelecionado()
    {
        if (selecionado.pegouPeca)
        {
            selecionado.sair = true;
            selecionado.posPecasSelecionada.AtualizaPos();
            StartCoroutine(selecionado.SelecionaPeca(selecionado.posPecasSelecionada.posLevantada, selecionado.posPecasSelecionada.posAbaixada,
                1f, selecionado.pecaSelecionada));
            selecionado.posPecasSelecionada.levantado = false;
            selecionado.casaAnterior.GetComponent<MeshRenderer>().material.color = selecionado.casas.corPadrao;
        }
        selecionado.gameObject.GetComponent<MeshRenderer>().material.color = selecionado.casa.GetComponent<MeshRenderer>().material.color;
        selecionado.pegouPeca = false;
        if (fim)
        {
            Invoke("ResetCooldown", 0.5f);
            cooldown = false;
        }
        else
        {
            Invoke("ResetCooldown", 1.1f);
            cooldown = false;
        }
    }

    void ResetCooldown()
    {
        cooldown = true;
    }

    void DesabilitaPuzzleXadrez()
    {
        if (fim)
        {
            //Cutscenepospuzzle.AtivaCutscene();
        }
        cooldown = false;
        selecionado.casa.enabled = true;
        selecionado.gameObject.SetActive(false);
    }

    public void VerificaFim(bool acertou)
    {
        if (acertou)
        {
            pecasCertas++;
        }
        else
        {
            pecasCertas--;
        }
        if(pecasCertas == 8)
        {
            FimPuzzleXadrez();
        }
    }

    void FimPuzzleXadrez()
    {
        colisao.enabled = false;
        fim = true;
        DesativaSelecionado();
    }

    public void ResetarPeças()
    {
        for(int i = 0; i < pecas.Length; i++)
        {
            pecas[i].gameObject.transform.position = pecas[i].posReset;
        }
    }
}
