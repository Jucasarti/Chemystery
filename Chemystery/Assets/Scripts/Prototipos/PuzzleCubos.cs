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
    //Variaveis de verificação de cooldown do click do mouse;
    private bool coooldown;

    //se está habilitado o modo puzzle;
    public bool modoPuzzle;

    //se esta na posição final;
    public bool posFim;

    //e se está em contato com o cubo vazio.
    public bool contatoCCV = false;

    [Header("Vector3")]
    //Vetores para marcar a posição dos cubos que irão trocar de lugar
    Vector3 posCubo, posCuboVazio;
    public Vector3 posFinal, posInicial;

    [Header("Outros")]
    //Variaveis para pegar a posição do cubo vazio, e velocidade do movimento do cubo ao se mover
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
        //Verificando se o cubo está ao lado do cubo vazio e se modo puzzle esta ligado
        if (contatoCCV && modoPuzzle)
        {
            //Verificando se está no cooldown
            if (!coooldown)
            {
                //Desativando contato com o cubovazio para evitar bugs
                contatoCCV = false;

                //Passando o valor da posição do cubo que foi clicado, para uma variável
                posCubo = gameObject.transform.position;

                //Passando o valor da posição do cubo vazio, para uma variável
                posCuboVazio = cuboVazio.transform.position;

                //Chamando função que troca a posição dos cubos
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

    //Trocando a posição do cubo vazio
    public void TrocarPos()
    {
        //Pegando os valores da variavel com o valor da posição do cubos
        posX = posCubo.x;
        posY = posCubo.y;
        posZ = posCubo.z;

        //Atualizando a posição do cubo vazio com os valores
        cuboVazio.AtualizaPos(posX, posY, posZ);

        //Chamando Coroutine que fará a transição da posição do cubo
        StartCoroutine(Mover());
    }

    //Animação de troca de lugar dos cubos
    IEnumerator Mover()
    {
        //Calculando a distância entre os cubos
        float distancia = Vector3.Distance(transform.position, posCuboVazio);

        //Calculando duração da "animação"
        float duracao = distancia / vel;

        //Determinando tempo para calculo do fim da "animação"
        float tempoDecorrido = 0f;

        while (tempoDecorrido < duracao)
        {
            //Movendo cubo
            transform.position = Vector3.Lerp(posCubo, posCuboVazio, tempoDecorrido / duracao);
            //Adicionando a variável de controle do tempo
            tempoDecorrido += Time.deltaTime;
            yield return null;
        }

        //Determinando a posição final do cubo clicado
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

