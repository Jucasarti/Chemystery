using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ManagerPuzzleCubo : MonoBehaviour
{
    #region Vari�veis

    [Header("Vector3")]
    Vector3 posCam, rotCam, posJog;

    [Header("Variav�is Booleanas")]
    public bool pertoPuzzle;
    public bool olhandoPuzzle;
    public bool modoPuzzle;

    [Header("Variav�is Inteiras")]
    int jogadas = 5, contador = 0;
    int luzAtual = 0;

    [Header("Arrays")]
    public PuzzleCubos[] cubos;
    public GameObject[] luzes;
    //public Material[] materialLuz;

    [Header("Outros")]
    public Text texto;
    public CuboVazio cuboVazio;
    public ManagerPlayer player;
    public CinemachineBrain cinemachine;
    public Color corInicial, corFinal;
    Collider colisao;
    PuzzleCubos olhandoPuzzleCubo;
    public GameObject puzzleCor;

    #endregion

    void Start()
    {
        colisao = gameObject.GetComponent<Collider>();

        //Posi��o e rota��o da camera ao iniciar o puzzle
        posCam = new Vector3(111.988f, 1.415f, 7.209f);
        rotCam = new Vector3(0f, 180f, 0f);

        //posi��o do player
        posJog = new Vector3(112.013f, 1f, 7.717f);
    }

    void Update()
    {
        if (pertoPuzzle)
        {
            Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            PuzzleCubos olhando = null;

            if (Physics.Raycast(raio, out hit))
            {
                olhando = hit.transform.GetComponent<PuzzleCubos>();
            }
            if (olhando != null)
            {
                olhandoPuzzle = true;
            }
            if (olhando != olhandoPuzzleCubo)
            {
                if (olhandoPuzzleCubo != null)
                {
                    olhandoPuzzle = false;
                }
                olhandoPuzzleCubo = olhando;
            }

            //Verificando se a tecla E foi pressionada
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Verificando se o player est� andando e se est� em contato com o puzzle para abri-lo
                if ( !modoPuzzle && olhandoPuzzle)
                {
                    //Chamando fun��o que inicia os puzzles
                    Puzzle();
                }

                //Verificando se o player n�o esta andando e n�o pode abrir puzzles, significanfo que ele j� est� em um puzzle, logo, ele fecha o puzzle
                else if (modoPuzzle)
                {
                    //Chamando a fun��o que fecha os puzzles
                    SairPuzzle();
                }
            }
        }
        if (pertoPuzzle && !modoPuzzle && olhandoPuzzle)
        {
            //Habilitando texto de ajuda
            texto.enabled = true;
        }
        else
        {
            //Desabilitando texto de ajuda
            texto.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Habilitando dist�ncia minima para acionamento do puzzle
            pertoPuzzle = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Desabilitando dist�ncia minima para acionamento do puzzle
            pertoPuzzle = false;
        }
    }

    //Fun��o para abrir o puzzle
    public void Puzzle()
    {
        //Atualizando a posi��o da camera para o puzzle
        Camera.main.transform.position = posCam;
        Camera.main.transform.rotation = Quaternion.Euler(rotCam);

        //Mudando a posi��o do jogador
        player.gameObject.transform.position = posJog;

        //Desabilitando o cinemachine
        cinemachine.enabled = false;

        //Habilitando o cursor, e n�o deixando ele sair da tela
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        //Habilitando o modo puzzle
        modoPuzzle = true;

        //Habilitando o modo puzzle nos cubos
        for (int i = 0; i < cubos.Length; i++)
            cubos[i].modoPuzzle = true;

        //Desabilitando texto de ajuda
        texto.enabled = false;

        //N�o permitindo o player andar enquanto estiver no puzzle
        player.Andando();

        //Desabilitando a colis�o com o player
        colisao.enabled = false;
    }

    //Fun��o para sair do puzzle em intera��o
    public void SairPuzzle()
    {
        //Habilitando o Cinemachine
        cinemachine.enabled = true;

        //Desabilitando o cursor, e travando ele na tela
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Desabilitando o modo puzzle
        modoPuzzle = false;

        //Desabilitando o modo puzzle nos cubos
        for (int i = 0; i < cubos.Length; i++)
            cubos[i].modoPuzzle = false;

        //Habilitando texto de ajuda
        texto.enabled = true;

        //Permitindo o player andar
        player.Andando();

        //Habilitando a colis�o com o player
        colisao.enabled = true;
    }

    //Fun��o que verifica o fim do puzzle
    public void VerificaFim()
    {
        jogadas--;
        Luzes();
        if (jogadas == 0)
        {
            for (int i = 0; i < cubos.Length; i++)
            {
                if (cubos[i].posFim)
                {
                    contador++;
                }
            }

            Debug.Log("Cubos certos: " + contador);

            if (contador == 5)
            {
                FimPuzzle();
                puzzleCor.SetActive(true);
                Debug.Log("Ganhei");
            }
            else
            {
                ResetaPuzzle();
                Debug.Log("Perdi");
            }
        }
    }

    //Fun��o que reseta o puzzle, caso ele esteja errado
    public void ResetaPuzzle()
    {
        for (int j = 0; j < cubos.Length; j++)
        {
            cubos[j].gameObject.transform.position = cubos[j].posInicial;
        }

        jogadas = 5;
        contador = 0;

        cuboVazio.gameObject.transform.position = cuboVazio.posInicial;

        for (int i = 0; i < cubos.Length; i++)
        {
            cubos[i].posFim = false;
        }

        for(int i = 0; i < luzes.Length; i++)
        {
            luzes[i].gameObject.SetActive(false);
        }

        luzAtual = 0;

        SairPuzzle();
    }

    //Fun��o para finalizar o puzzle, caso ele esteja certo
    public void FimPuzzle()
    {
        //Definindo que ele n�o pode abrir puzzle
        pertoPuzzle = false;

        //Desabilitando o texto de ajuda
        texto.enabled = false;

        //Habilitando o Cinemachine
        cinemachine.enabled = true;

        //Desabilitando o cursor, e travando ele na tela
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Desabilitando o modo puzzle
        modoPuzzle = false;

        //Permitindo o playere andar
        player.Andando();

        //Destruindo colis�o com o player e finalizando o puzzle
        Destroy(gameObject);
    }

    //Fun��o para acender as luzes do painel
    void Luzes()
    {
        luzes[luzAtual].gameObject.SetActive(true);
        luzAtual++;
    }

}
