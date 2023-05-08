using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.InputSystem.XR;
using Cinemachine;
using UnityEngine.Rendering.Universal.Internal;

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
    private Transform myCamera;
    private float vel;
    float raycastRange = 3f;

    [Header("Outros")]
    public Transform cameraTransform;
    public CinemachineBrain cinemachine;
    public GameObject objeto;
    Camera cam;
    public LayerMask interactableMask;
    public Cut cutscene;

    public int numeroDeValvulas = 0;

    [Header("UI")]
    public GameObject interagirUI;

    private Crosshair crosshair;
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

        cam = Camera.main;
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

        if(jaEstaInspecionando == true) {

            interagirUI.SetActive(false);

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
        entradasJogador = transform.TransformDirection(entradasJogador);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            vel = velJogadorCorrendo;
        }
        else
        {
            vel = velJogadorNormal;
        }

        characterController.Move(entradasJogador * Time.deltaTime * vel);
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
}
