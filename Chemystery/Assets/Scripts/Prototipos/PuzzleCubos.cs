using Cinemachine;
using JetBrains.Annotations;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class PuzzleCubos : MonoBehaviour
{
    [Header("Variaveis Booleanas")]
    //Variaveis de verifica��o de cooldown do click do mouse;
    private bool coooldown;

    //se est� habilitado o modo puzzle;
    public bool modoPuzzle;

    //se esta na posi��o final;
    public bool posFim;

    //e se est� em contato com o cubo vazio.
    public bool contatoCCV = false;

    [Header("Vector3")]
    //Vetores para marcar a posi��o dos cubos que ir�o trocar de lugar
    Vector3 posCubo, posCuboVazio;
    public Vector3 posFinal, posInicial;

    [Header("Outros")]
    //Variaveis para pegar a posi��o do cubo vazio, e velocidade do movimento do cubo ao se mover
    private float posX, posY, posZ;
    public float vel;

    //Outros scripts
    public CuboVazio cuboVazio;
    public ManagerPuzzleCubo managerPuzzleCubo;

    // Start is called before the first frame update
    void Start()
    {
        posInicial = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Atualizando o cooldown do click
        coooldown = cuboVazio.cooldown;
    }

    void OnMouseDown()
    {
        //Verificando se o cubo est� ao lado do cubo vazio e se modo puzzle esta ligado
        if (contatoCCV && modoPuzzle)
        {
            //Verificando se est� no cooldown
            if (!coooldown)
            {
                //Desativando contato com o cubovazio para evitar bugs
                contatoCCV = false;

                //Passando o valor da posi��o do cubo que foi clicado, para uma vari�vel
                posCubo = gameObject.transform.position;

                //Passando o valor da posi��o do cubo vazio, para uma vari�vel
                posCuboVazio = cuboVazio.transform.position;

                //Chamando fun��o que troca a posi��o dos cubos
                TrocarPos();
            }
        }
    }

    //Verificando se o cubo permanece em contato com o cubo vazio
    void OnTriggerStay(Collider outro)
    {
        if (outro.CompareTag("CuboVazio"))
        {
            //E alterando o valor
            contatoCCV = true;
        }
    }
    //Verificando se o cubo deixou de ter contato com o cubo vazio
    void OnTriggerExit(Collider outro)
    {
        if (outro.CompareTag("CuboVazio"))
        {
            //E alterando o valor
            contatoCCV = false;
        }
    }

    //Trocando a posi��o do cubo vazio
    public void TrocarPos()
    {
        //Pegando os valores da variavel com o valor da posi��o do cubos
        posX = posCubo.x;
        posY = posCubo.y;
        posZ = posCubo.z;

        //Atualizando a posi��o do cubo vazio com os valores
        cuboVazio.AtualizaPos(posX, posY, posZ);

        //Chamando Coroutine que far� a transi��o da posi��o do cubo
        StartCoroutine(Mover());
    }

    //Anima��o de troca de lugar dos cubos
    IEnumerator Mover()
    {
        //Calculando a dist�ncia entre os cubos
        float distancia = Vector3.Distance(transform.position, posCuboVazio);

        //Calculando dura��o da "anima��o"
        float duracao = distancia / vel;

        //Determinando tempo para calculo do fim da "anima��o"
        float tempoDecorrido = 0f;

        while (tempoDecorrido < duracao)
        {
            //Movendo cubo
            transform.position = Vector3.Lerp(posCubo, posCuboVazio, tempoDecorrido / duracao);
            //Adicionando a vari�vel de controle do tempo
            tempoDecorrido += Time.deltaTime;
            yield return null;
        }

        //Determinando a posi��o final do cubo clicado
        transform.position = posCuboVazio;

        if (gameObject.transform.position == posFinal)
        {
            posFim = true;
        }
        else
        {
            posFim = false;
        }
        managerPuzzleCubo.VerificaFim();
    }
}

