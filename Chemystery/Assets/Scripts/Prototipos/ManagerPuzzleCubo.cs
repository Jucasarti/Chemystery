using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ManagerPuzzleCubo : MonoBehaviour, IInteractable
{
    #region Vari�veis

    [Header("Vector3")]
    Vector3 posCam, rotCam, posJog;

    [Header("Variav�is Booleanas")]
    public bool modoPuzzle;

    [Header("Variav�is Inteiras")]
    int jogadas = 5, contador = 0;
    int luzAtual = 0;

    [Header("Arrays")]
    public PuzzleCubos[] cubos;
    public GameObject[] luzes;
    //public Material[] materialLuz;

    [Header("Outros")]
    public CuboVazio cuboVazio;
    public CinemachineBrain cinemachine;
    public Color corInicial, corFinal;

    [SerializeField] AudioSource sourceErrouPuzzle;

    [SerializeField] CinemachineVirtualCamera[] cameras;

    ManagerPlayer player;
    Collider colisao;

    private PuzzleCor puzzleCor;

    private Crosshair crosshair;

    #endregion

    void Awake() {

        player = FindObjectOfType<ManagerPlayer>();

        colisao = GetComponent<Collider>();

        puzzleCor = FindObjectOfType<PuzzleCor>();

        crosshair = FindObjectOfType<Crosshair>();

    }



    void Start()
    {

        //Posi��o e rota��o da camera ao iniciar o puzzle
        posCam = new Vector3(111.988f, 1.415f, 7.209f);
        rotCam = new Vector3(0f, 180f, 0f);

        //posi��o do player
        posJog = new Vector3(112.013f, 1f, 7.717f);
    }

    public void Interagir() {

        Puzzle();

        crosshair.DesativarCrosshair();

    }

    void Update()
    {
        if(modoPuzzle) {

            if(Input.GetKeyDown(KeyCode.Space)) {

                SairPuzzle();

            }
        }
    }
    

    //Fun��o para abrir o puzzle
    public void Puzzle()
    {
        //Atualizando a posi��o da camera para o puzzle
        //Camera.main.transform.position = posCam;
        //Camera.main.transform.rotation = Quaternion.Euler(rotCam);

        //Mudando a posi��o do jogador
        //player.gameObject.transform.position = posJog;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        //player.TravaCamera();

        cameras[0].enabled = false;
        cameras[1].enabled = true;
            
        player.EstaInspecionando();
        player.Andando();

        //Habilitando o modo puzzle
        modoPuzzle = true;

        colisao.enabled = false;

        //Habilitando o modo puzzle nos cubos
        for (int i = 0; i < cubos.Length; i++) {

            cubos[i].modoPuzzle = true;

        }

    }

    //Fun��o para sair do puzzle em intera��o
    public void SairPuzzle()
    {
        //player.TravaCamera();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        

        cameras[1].enabled = false;
        cameras[0].enabled = true;

        player.EstaInspecionando();
        player.Andando();

        //Desabilitando o modo puzzle
        modoPuzzle = false;

        colisao.enabled = true;

        crosshair.AtivarCrosshair();

        //Desabilitando o modo puzzle nos cubos
        for (int i = 0; i < cubos.Length; i++) {
            cubos[i].modoPuzzle = false;

        }
    

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
                puzzleCor.energiaAtivada = true;
                Debug.Log("Ganhei");
            }
            else
            {
                ResetaPuzzle();
                sourceErrouPuzzle.PlayOneShot(sourceErrouPuzzle.clip);
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        

        cameras[1].enabled = false;
        cameras[0].enabled = true;

        player.EstaInspecionando();
        player.Andando();

        crosshair.AtivarCrosshair();


        //player.TravaCamera();

        //Desabilitando o modo puzzle
        modoPuzzle = false;
    
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
