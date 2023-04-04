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
    //Variavel para verificar se a pe�a esta em contato com o espa�o vazio
    public bool contatoCCV = false;

    //Variavel para verificar o cooldown e n�o espamar o click do mouse, evitando bugs
    private bool coooldown;

    //Varial para verificar se o click do mouse pode ser habilitado
    public bool modPuzzle;

    //Vetores para marcar a posi��o dos cubos que ir�o trocar de lugar
    Vector3 posCubo, posCuboVazio;

    //Variaveis para a verifica��o do fim do puzzle
    public bool ct1, ct2, ct3, ct4, ct5;

    //Variaveis para pegar a posi��o do cubo vazio, e velocidade do movimento do cubo ao se mover
    public float posX, posY, posZ, vel;

    //GameObjects para interagir com os outros scripts
    public GameObject cuboVazio, c1, c2, c3, c4, c5, Interage, Player;

    public GameObject[] cancelando;

    //Texto que vai ajudar o player
    public Text texto;

    //Vari�vel que vai mudar a posi��o da c�mera
    public Transform cameraTransform;

    //Variaveis que ir�o controlar quando o player pode interagir ou n�o com os puzzles
    public bool fimPuzzle;

    //Variaveis que controlar�o quando o player pode andar, abrir e fechar os puzzles
    public bool abrirPuzzle, cancelaPuzzle;

    //Vari�vel para habilitar e desabilitar o cinemachine
    public CinemachineBrain cinemachine;

    //Variaveis para receber o valor de posi��o e rota��o da camera
    Vector3 camPos, camRot, jogPos;

    public Vector3 posFinal, posInicial;
    public bool fim;

    public VerificaCubos verificaCubos;

    // Start is called before the first frame update
    void Start()
    {
        //Determinado os scripts
        cuboVazio.GetComponent<CuboVazio>();

        //c1.GetComponent<Vermelhos>();
        //c2.GetComponent<Vermelhos>();
        //c3.GetComponent<Vermelhos>();
        //c4.GetComponent<Vermelhos>();
        //c5.GetComponent<Vermelhos>();

        //Posi��o e rota��o da camera ao iniciar o puzzle
        camPos = new Vector3(60.35f, 1.38f, 34.67f);
        camRot = new Vector3(0f, 90f, 0f);

        //posi��o do player
        jogPos = new Vector3(58.83f, 0f, 34.52f);

        //Determinando vari�vel como falsa
        cancelaPuzzle = false;

        //Script de intera��o com player
        Interage.GetComponent<Interage>();

        //Script de movimenta��o do player
        Player.GetComponent<FirstPersonController>().andar = true;

        posInicial = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Atualizando o cooldown do click
        coooldown = cuboVazio.GetComponent<CuboVazio>().cooldown;

        //Atualizando a posi��o das pe�as corretas
        //ct1 = c1.GetComponent<Vermelhos>().corretoV;
        //ct2 = c2.GetComponent<Vermelhos>().corretoV;
        //ct3 = c3.GetComponent<Vermelhos>().corretoV;
        //ct4 = c4.GetComponent<Vermelhos>().corretoV;
        //ct5 = c5.GetComponent<VCuboVazio>().corretoV;

        if (abrirPuzzle)
        {
            //Verificando se a tecla E foi pressionada
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Verificando se o player est� andando e se est� em contato com o puzzle para abri-lo
                if (!cancelaPuzzle && !modPuzzle)
                {
                    //Chamando fun��o que inicia os puzzles
                    Puzzle();
                }

                //Verificando se o player n�o esta andando e n�o pode abrir puzzles, significanfo que ele j� est� em um puzzle, logo, ele fecha o puzzle
                else if (!cancelaPuzzle && modPuzzle)
                { 
                    //Chamando a fun��o que fecha os puzzles
                    SairPuzzle();  
                }
            }
        }
    }

    void OnMouseDown()
    {
        //Verificando se o cubo est� ao lado do cubo vazio e se modo puzzle esta ligado
        if (contatoCCV && modPuzzle)
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
        //Pegando os valores da variavel com o valor da posi��o do cubo clicado
        posX = posCubo.x;
        posY = posCubo.y;
        posZ = posCubo.z;

        //Atualizando a posi��o do cubo vazio com os valores
        cuboVazio.GetComponent<CuboVazio>().AtualizaPos(posX, posY, posZ);

        //Chamando Coroutine que far� a transi��o da posi��o do cubo clicado
        StartCoroutine(Mover());
        if (gameObject.transform.position == posFinal)
        {
            fim = true;
        }
        else
        {
            fim = false;
        }
    }

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

        verificaCubos.VerificaFim();
    }

    //Fun��o para ativar o puzzle em intera��o
    public void Puzzle()
    {
        //Atualizando a posi��o da camera
        Camera.main.transform.position = camPos;
        Camera.main.transform.rotation = Quaternion.Euler(camRot);

        Player.gameObject.transform.position = jogPos;

        //Desabilitando o cinemachine
        cinemachine.enabled = false;

        //Habilitando o cursor, e n�o deixando ele sair da tela
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        //Habilitando o modo puzzle
        modPuzzle = true;

        texto.enabled = false;

        Player.GetComponent<FirstPersonController>().andar = false;

        //Desabilitando collider do player
        Interage.gameObject.GetComponent<Collider>().enabled = false;
    }

    //Fun��o para sair do puzzle em intera��o
    public void SairPuzzle()
    {
        //Habilitando o Cinemachine
        cinemachine.enabled = true;

        texto.enabled = true;

        //Desabilitando o cursor, e travando ele na tela
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Desabilitando o modo puzzle
        modPuzzle = false;

        Player.GetComponent<FirstPersonController>().andar = true;

        //Habilitando collider do player
        Interage.gameObject.GetComponent<Collider>().enabled = true;
    }

    //Fun��o para finalizar o puzzle
    public void FimPuzzle()
    {
        //FeedBack b�sico
        Debug.Log("PUZZLE COMPLETO, voc� ganhou: NADA!!!");

        //Definindo que ele pode abir puzzles
        abrirPuzzle = false;

        //Desabilitando o texto de ajuda
        texto.enabled = false;

        for (int i = 0; i < cancelando.Length; i++)
        {
            cancelando[i].GetComponent<PuzzleCubos>().cancelaPuzzle = true;
            abrirPuzzle = false;
        }

        Destroy(Interage);

        //Habilitando o Cinemachine
        cinemachine.enabled = true;

        //Desabilitando o cursor, e travando ele na tela
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Desabilitando o modo puzzle
        modPuzzle = false;

        Player.GetComponent<FirstPersonController>().andar = true;
    }

    public void Interagindo()
    {
        abrirPuzzle = Interage.GetComponent<Interage>().abrirPuzzle;
    }
}

