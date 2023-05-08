using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Selecionado : MonoBehaviour
{
    #region Variraveis

    [Header("Animações")]
    public float velSelecionar;
    public Vector3 posInicial;
    public Color[] cores;

    [Header("Peças")]
    public GameObject peca;
    public GameObject pecaSelecionada;
    public GameObject pecaAnterior;

    [Header("Casas")]
    public MeshRenderer casa;
    public GameObject casaAnterior;
    public Casas casas;

    [Header("Posições das Peças")]
    public Pecas posPecas;
    public Pecas posPecasSelecionada;
    public Pecas posPecasAnterior;

    [Header("Outros")]
    public Lados[] lados;
    public ManagerXadrez managerXadrez;
    public bool sair;
    public bool pegouPeca;
    public bool trocarPecas;
    public bool mexerSelecionado;
    private bool pegarPeca;
    public bool trocarCamera;

    #endregion

    private void Awake()
    {
        posInicial = gameObject.transform.position;
    }

    void Update()
    {
        if (mexerSelecionado)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (lados[0].podeAndar == true)
                    AtualizaPos(gameObject.transform.position.x, transform.position.y, transform.position.z - 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (lados[1].podeAndar == true)
                    AtualizaPos(gameObject.transform.position.x, transform.position.y, transform.position.z + 0.1f);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (lados[2].podeAndar == true)
                    AtualizaPos(gameObject.transform.position.x + 0.1f, transform.position.y, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (lados[3].podeAndar == true)
                    AtualizaPos(gameObject.transform.position.x - 0.1f, transform.position.y, transform.position.z);
            }
        }
        if (pegarPeca)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pegarPeca = false;
                mexerSelecionado = false;
                sair = false;

                pecaAnterior = pecaSelecionada;     
                posPecasAnterior = posPecasSelecionada;

                if (pegouPeca && pecaSelecionada && peca)
                {
                    trocarPecas = true;
                    posPecasAnterior.AtualizaPos();
                }
                else
                {
                    trocarPecas = false;
                    if (casaAnterior != casa && casaAnterior != null)
                    {
                        casaAnterior.GetComponent<MeshRenderer>().material.color = casas.corPadrao;
                    }
                    casaAnterior = casa.gameObject;
                    casas = casa.GetComponent<Casas>();
                }

                pecaSelecionada = peca ? peca : pecaAnterior;               

                posPecasSelecionada = posPecas ? posPecas : posPecasAnterior;
                posPecasSelecionada.AtualizaPos();
                pegouPeca = !pegouPeca;

                
                if (pegouPeca)
                {
                    casa.material.color = cores[2];
                    gameObject.GetComponent<MeshRenderer>().material.color = cores[1];
                    StartCoroutine(SelecionaPeca(posPecasSelecionada.posAbaixada, posPecasSelecionada.posLevantada, velSelecionar, pecaSelecionada));
                    posPecasSelecionada.levantado = true;
                    peca = null;
                }
                else if (!pegouPeca && trocarPecas)
                {
                    gameObject.GetComponent<MeshRenderer>().material.color = cores[1];
                    StartCoroutine(SelecionaPeca(posPecasSelecionada.posAbaixada, posPecasSelecionada.posLevantada, velSelecionar, posPecasSelecionada.gameObject));
                    StartCoroutine(SelecionaPeca(posPecasAnterior.posLevantada, posPecasAnterior.posAbaixada, velSelecionar, posPecasAnterior.gameObject));
                    posPecasAnterior.levantado = false;
                    pegouPeca = true;
                    trocarPecas = false;
                    peca = pecaAnterior;
                    posPecas = posPecasAnterior;
                }
                else
                {
                    casaAnterior.GetComponent<MeshRenderer>().material.color = casas.corPadrao;
                    gameObject.GetComponent<MeshRenderer>().material.color = cores[0];
                    StartCoroutine(SelecionaPeca(posPecasAnterior.posLevantada, posPecasAnterior.posAbaixada, velSelecionar, pecaAnterior));         
                    posPecasAnterior.levantado = false;
                }
            }
        }
    }

    public void AtualizaPos(float posX, float posY, float posZ)
    {
        casa.enabled = true;
        gameObject.transform.position = new Vector3(posX, posY, posZ);
        if (pegouPeca)
        {
            pecaSelecionada.transform.position = new Vector3(gameObject.transform.position.x, pecaSelecionada.transform.position.y, 
                gameObject.transform.position.z);
        }
    }

    public IEnumerator SelecionaPeca(Vector3 posPInicial, Vector3 posPFinal, float vel, GameObject pecaAtual)
    {
        //Calculando a distância entre as posições
        float distancia = Vector3.Distance(posPInicial, posPFinal);

        //Calculando duração da "animação"
        float duracao = distancia / vel;

        //Determinando tempo para calculo do fim da "animação"
        float tempoDecorrido = 0f;

        while (tempoDecorrido < duracao)
        {
            //Movendo cubo
            pecaAtual.transform.position = Vector3.Lerp(posPInicial, posPFinal, tempoDecorrido / duracao);
            //Adicionando a variável de controle do tempo
            tempoDecorrido += Time.deltaTime;
            yield return null;
        }

        //Determinando a posição final do cubo clicado
        pecaAtual.transform.position = posPFinal;  
        if (sair)
        {
            pecaSelecionada = null;   
        }
        pegarPeca = true;
        mexerSelecionado = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PecasXadrez"))
        {
            peca = other.gameObject;
            posPecas = other.GetComponent<Pecas>();
            pegarPeca = true;
            if (trocarPecas)
            {
                posPecas.AtualizaPos();
            }
        }
        if (other.CompareTag("CasaXadrez"))
        {
            casa = other.GetComponent<MeshRenderer>();
            casa.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PecasXadrez"))
        {
            peca = null;
            posPecas = null;

            if (!pegouPeca)
            {
                pegarPeca = false;
            }
        }
    }
}