using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using Cinemachine;
using TMPro;

public class ManagerXadrez : MonoBehaviour, IInteractable
{
    private bool cooldown;
    private bool comeco;
    public bool aberto;
    private float pecasCertas;

    Selecionado selecionado;
    Collider colisao;
    ManagerPlayer player;
    Crosshair crosshair;

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
        comeco = true;
        aberto = false;
        DesativaSelecionado();
        fim = false;
    }

    public void Interagir()
    {
        if (!aberto)
        {
            AtivaSelecionado();
        }
    }

    private void Update()
    {
        if (cooldown)
        {
            DesabilitaPuzzleXadrez();
        }

        if (aberto)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DesativaSelecionado();
            }
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

        crosshair.DesativarCrosshair();

        cameras[0].enabled = false;
        cameras[1].enabled = true;

        player.EstaInspecionando();
        player.Andando();

        selecionado.mexerSelecionado = true;
        player.gameObject.transform.position = posPlayer;
        aberto = true;
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
            crosshair.AtivarCrosshair();

            cameras[1].enabled = false;
            cameras[0].enabled = true;

            player.EstaInspecionando();
            player.Andando();
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
            selecionado.trocarCamera = false;
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
        aberto = false;
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
