using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.InputSystem.XR;
using Cinemachine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;

public class ManagerPlayer : MonoBehaviour
{
    #region Variavéis
    [Header("Variaveis Booleanas")]
    bool andar = true;
    public bool pegouChave = false;
    public bool jaEstaInspecionando = false;

    [Header("Movimentação do Player")]
    [SerializeField] private float velJogadorNormal;
    [SerializeField] private float velJogadorCorrendo;
    CharacterController characterController;
    private Vector3 entradasJogador;
    public Transform myCamera;
    private float raycastRange = 3f;
    private float vel;
    //private float alturaSalto = 1f;
    private float gravidade = -7.5f;
    private float velVertical;
    private bool podePular;

    [Header("Outros")]
    public CinemachineBrain cinemachine;
    public GameObject objeto;
    public GameObject cutSceneMorte;
    public CinemachineVirtualCamera cameraMorte;
    public LayerMask interactableMask;
    public LayerMask cenarioMask;
    public Transform verificaChao;
    public Cut cutscene;

    public int numeroDeValvulas = 0;

    [Header("UI")]
    public GameObject interagirUI;
    private Crosshair crosshair;

    [Header("Audio")]
    [SerializeField ] AudioSource sourceAndando;

    [SerializeField ] AudioSource sourceCorrendo;

    [SerializeField] AudioSource sourceMorte;

    #endregion

    void Awake () {

        crosshair = FindObjectOfType<Crosshair>();

        numeroDeValvulas = 0;

    }



    // Start is called before the first frame update
    void Start()
    {
        //Ativando o controlller do personagem
        characterController = GetComponent<CharacterController>();

        //Desativando o cursor e travando a tela
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        myCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {

        if(jaEstaInspecionando == false && MenuPausa.jogoPausado == false) {
        //Criando Raycast
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Verificando se o raycast atingiu um objeto, dentro da layermask
        if(Physics.Raycast(ray, out hit, raycastRange, interactableMask)) {

            //Ativando UI 
            interagirUI.SetActive(true);

            crosshair.CrosshairInteracao();

            //Verificando se o player apertou E
            if(Input.GetKeyDown(KeyCode.E)) {

                //Pegando o objeto selecionado e ativando sua função de interagir
                IInteractable itemInteractable = hit.collider.GetComponent<IInteractable>();

                itemInteractable.Interagir();

            }

        } else {

            //Desativando a UI
            interagirUI.SetActive(false);

            crosshair.CrosshairNormal();

            }

        }

        //Verificando se o player pode andar
        if (andar)
        {
            Movimentacao();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            TravaCamera();
        }
    }


    public void Andando()
    {
        andar = !andar;
    }

    public void EstaInspecionando() {
        jaEstaInspecionando = !jaEstaInspecionando;

        Debug.Log(jaEstaInspecionando);

        if(jaEstaInspecionando == true) {

            interagirUI.SetActive(false);

            TirarSomAndar();

        } else {

            ColocarSomAndar();

        }

    }

    public void ColetarValvula() {

        numeroDeValvulas++;

        //Atualizar UI

    }

    public void RetirarValvula () {

        numeroDeValvulas--;

        //Atualizar UI

    }

    void Movimentacao()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, myCamera.eulerAngles.y, transform.eulerAngles.z);

        entradasJogador = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        entradasJogador = transform.TransformDirection(entradasJogador).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            vel = velJogadorCorrendo;
            
            TestandoCorrer();
            //SomCorrer();
        }
        else
        {
            vel = velJogadorNormal;
           
            TestandoAndar();
            //SomAndar();
        }

        characterController.Move(entradasJogador * Time.deltaTime * vel);

        //podePular = Physics.CheckSphere(verificaChao.position, 0.3f, cenarioMask);

        /*if(Input.GetKeyDown(KeyCode.Space) && verificaChao)
        {
            velVertical = Mathf.Sqrt(alturaSalto * -2f * gravidade);
        }*/

        if(podePular && velVertical < 0)
        {
            velVertical = -1f;
        }

        velVertical += gravidade * Time.deltaTime;
        characterController.Move(new Vector3(0, velVertical, 0) * Time.deltaTime);
    }

    void TestandoCorrer () {

        if(entradasJogador.x != 0 || entradasJogador.z != 0) {

            sourceAndando.enabled = false;
            sourceCorrendo.enabled = true;

        } else {

            sourceAndando.enabled = false;
            sourceCorrendo.enabled = false;
        
        }

    }

    void TestandoAndar () {

        if(entradasJogador.x != 0 || entradasJogador.z != 0) {

            sourceAndando.enabled = true;
            sourceCorrendo.enabled = false;

        } else {

            sourceAndando.enabled = false;
            sourceCorrendo.enabled = false;
        
        }
    }

    public void TirarSomAndar () {

        sourceAndando.enabled = false;
        sourceCorrendo.enabled = false;

    }

    public void ColocarSomAndar() {

        sourceAndando.enabled = true;
        sourceCorrendo.enabled = true;

    }

    public void TravaCamera()
    {
        if (cinemachine.enabled)
        {
            cinemachine.enabled = false;
            Camera.main.transform.LookAt(objeto.transform.position);
            Andando();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            cinemachine.enabled = true;
            Andando();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AtivaSmoke"))
        {
            cutscene.AtivaCutscene();
        }
    }

    public void Morrer () 
    {
        myCamera = Camera.main.transform;
        cameraMorte.transform.position = myCamera.position;
        cameraMorte.transform.rotation = myCamera.rotation;
        cutSceneMorte.SetActive(true);
        Andando();
        StartCoroutine(AnimMorte());

    }

    IEnumerator AnimMorte () {

        sourceMorte.PlayOneShot(sourceMorte.clip);

        yield return new WaitForSeconds(2);
        Andando();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
