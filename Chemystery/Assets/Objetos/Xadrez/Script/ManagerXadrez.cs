using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using Cinemachine;
using TMPro;

public class ManagerXadrez : MonoBehaviour, IInteractable
{
    private bool cooldown;
    private bool comeco;
    private bool trocandoCamera;
    public bool aberto;
    private float pecasCertas;

    Selecionado selecionado;
    Collider colisao;
    ManagerPlayer player;
    Crosshair crosshair;
    Chave chave;

    public Text textoAjuda;
    public bool fim;
    public Vector3 posPlayer;
    public Pecas[] pecas;
    public CinemachineVirtualCamera[] cameras;

    private void Awake()
    {
        crosshair = FindObjectOfType<Crosshair>();
        player = FindObjectOfType<ManagerPlayer>();
        selecionado = FindObjectOfType<Selecionado>();
        selecionado.casa = FindObjectOfType<MeshRenderer>();
        colisao = gameObject.GetComponent<Collider>();
        chave = GetComponent<Chave>();
        comeco = true;
        aberto = false;
        trocandoCamera = false;
        DesativaSelecionado();
        fim = false;
    }

    public void Interagir()
    {
        if (!aberto && !trocandoCamera)
        {
            TrocandoCamera();
            AtivaSelecionado();
        }
    }

    private void Update()
    {
        if (cooldown)
        {
            DesabilitaPuzzleXadrez();
        }

        if (aberto && !trocandoCamera)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TrocandoCamera();
                DesativaSelecionado();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                ResetarPecas();
            }
        }
    }

    public void AtivaSelecionado()
    {
        selecionado.gameObject.SetActive(true);
        selecionado.gameObject.GetComponent<MeshRenderer>().material.color = selecionado.cores[0];
        selecionado.gameObject.transform.position = selecionado.posInicial;

        textoAjuda.enabled = true;

        crosshair.DesativarCrosshair();

        cameras[0].enabled = false;
        cameras[1].enabled = true;
            
        player.EstaInspecionando();
        player.Andando();

        selecionado.mexerSelecionado = true;
        player.gameObject.transform.position = posPlayer;

        Invoke("Abrir", 2f);
    }

    public void DesativaSelecionado()
    {
        selecionado.mexerSelecionado = false;
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
        if (!comeco)
        {
            textoAjuda.enabled = false;

            crosshair.AtivarCrosshair();

            cameras[1].enabled = false;
            cameras[0].enabled = true;

            player.EstaInspecionando();
            player.Andando();
            Invoke("Abrir", 2f);
        }
        else
        {
            comeco = false;
        }

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

    void Abrir()
    {
        if (aberto)
        {
            aberto = false;
        }
        else
        {
            aberto = true;
        }
        TrocandoCamera();
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
        Debug.Log(pecasCertas);
        if(pecasCertas == 32)
        {
            FimPuzzleXadrez();
        }
    }

    void FimPuzzleXadrez()
    {
        colisao.enabled = false;
        fim = true;
        chave.PegarChave();
        DesativaSelecionado();
    }

    public void ResetarPecas()
    {
        for(int i = 0; i < pecas.Length; i++)
        {
            pecas[i].gameObject.transform.position = pecas[i].posReset;
        }
    }

    void TrocandoCamera()
    {
        Debug.Log(trocandoCamera + " 1");
        trocandoCamera = !trocandoCamera;
        Debug.Log(trocandoCamera + " 2");
    }
}
