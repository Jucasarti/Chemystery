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
    //Variavel para verificar se a peça esta em contato com o espaço vazio
    public bool contatoCCV = false;

    //Variavel para verificar o cooldown e não espamar o click do mouse, evitando bugs
    private bool coooldown;

    //Varial para verificar se o click do mouse pode ser habilitado
    public bool modPuzzle;

    //Vetores para marcar a posição dos cubos que irão trocar de lugar
    Vector3 posCubo, posCuboVazio;

    //Variaveis para a verificação do fim do puzzle
    public bool ct1, ct2, ct3, ct4, ct5;

    //Variaveis para pegar a posição do cubo vazio, e velocidade do movimento do cubo ao se mover
    public float posX, posY, posZ, vel;

    //GameObjects para interagir com os outros scripts
    public GameObject cuboVazio, c1, c2, c3, c4, c5, Interage, Player;

    public GameObject[] cancelando;

    //Texto que vai ajudar o player
    public Text texto;

    //Variável que vai mudar a posição da câmera
    public Transform cameraTransform;

    //Variaveis que irão controlar quando o player pode interagir ou não com os puzzles
    public bool fimPuzzle;

    //Variaveis que controlarão quando o player pode andar, abrir e fechar os puzzles
    public bool abrirPuzzle, cancelaPuzzle;

    //Variável para habilitar e desabilitar o cinemachine
    public CinemachineBrain cinemachine;

    //Variaveis para receber o valor de posição e rotação da camera
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

        //Posição e rotação da camera ao iniciar o puzzle
        camPos = new Vector3(60.35f, 1.38f, 34.67f);
        camRot = new Vector3(0f, 90f, 0f);

        //posição do player
        jogPos = new Vector3(58.83f, 0f, 34.52f);

        //Determinando variável como falsa
        cancelaPuzzle = false;

        //Script de interação com player
        Interage.GetComponent<Interage>();

        //Script de movimentação do player
        Player.GetComponent<FirstPersonController>().andar = true;

        posInicial = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Atualizando o cooldown do click
        coooldown = cuboVazio.GetComponent<CuboVazio>().cooldown;

        //Atualizando a posição das peças corretas
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
                //Verificando se o player está andando e se está em contato com o puzzle para abri-lo
                if (!cancelaPuzzle && !modPuzzle)
                {
                    //Chamando função que inicia os puzzles
                    Puzzle();
                }

                //Verificando se o player não esta andando e não pode abrir puzzles, significanfo que ele já está em um puzzle, logo, ele fecha o puzzle
                else if (!cancelaPuzzle && modPuzzle)
                { 
                    //Chamando a função que fecha os puzzles
                    SairPuzzle();  
                }
            }
        }
    }

    void OnMouseDown()
    {
        //Verificando se o cubo está ao lado do cubo vazio e se modo puzzle esta ligado
        if (contatoCCV && modPuzzle)
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
        //Pegando os valores da variavel com o valor da posição do cubo clicado
        posX = posCubo.x;
        posY = posCubo.y;
        posZ = posCubo.z;

        //Atualizando a posição do cubo vazio com os valores
        cuboVazio.GetComponent<CuboVazio>().AtualizaPos(posX, posY, posZ);

        //Chamando Coroutine que fará a transição da posição do cubo clicado
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

        verificaCubos.VerificaFim();
    }

    //Função para ativar o puzzle em interação
    public void Puzzle()
    {
        //Atualizando a posição da camera
        Camera.main.transform.position = camPos;
        Camera.main.transform.rotation = Quaternion.Euler(camRot);

        Player.gameObject.transform.position = jogPos;

        //Desabilitando o cinemachine
        cinemachine.enabled = false;

        //Habilitando o cursor, e não deixando ele sair da tela
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        //Habilitando o modo puzzle
        modPuzzle = true;

        texto.enabled = false;

        Player.GetComponent<FirstPersonController>().andar = false;

        //Desabilitando collider do player
        Interage.gameObject.GetComponent<Collider>().enabled = false;
    }

    //Função para sair do puzzle em interação
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

    //Função para finalizar o puzzle
    public void FimPuzzle()
    {
        //FeedBack básico
        Debug.Log("PUZZLE COMPLETO, você ganhou: NADA!!!");

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

