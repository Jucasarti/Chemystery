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
    [Header("Variaveis Booleanas")]
    bool andar = true;
    public bool pegouChave = false;
    public bool jaEstaInspecionando = false;

    

    [Header("Variaveis Float")]
    public float velRotacao;
    public float vel;
    public float correr;
    public float correndo;
    float raycastRange = 3f;

    [Header("Outros")]
    public Transform cameraTransform;
    public CinemachineBrain cinemachine;
    CharacterController controller;
    Vector3 movimento = Vector3.zero;
    public GameObject objeto;
    Camera cam;

    public LayerMask interactableMask;

    [Header("Inspeção dos Itens")]

    public GameObject notinha;
    public GameObject porta;
    bool notinhaAtiva = false;
    public bool pertoPorta = false;

    [Header("UI")]
    public GameObject interagirUI;




    // Start is called before the first frame update
    void Start()
    {
        //Ativando o controlller do personagem
        controller = GetComponent<CharacterController>();

        //Desativando o cursor e travando a tela
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cam = Camera.main;

        correndo = vel * correr;
    }

    // Update is called once per frame
    void Update()
    {

        if(jaEstaInspecionando == false) {
        //Criando Raycast
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Verificando se o raycast atingiu um objeto, dentro da layermask
        if(Physics.Raycast(ray, out hit, raycastRange, interactableMask)) {

            //Ativando UI 
            interagirUI.SetActive(true);

            //Verificando se o player apertou E
            if(Input.GetKeyDown(KeyCode.E)) {

                //Pegando o objeto selecionado e ativando sua função de interagir
                IInteractable itemInteractable = hit.collider.GetComponent<IInteractable>();

                itemInteractable.Interagir();
                EstaInspecionando();

            }

        } else {

            //Desativando a UI
            interagirUI.SetActive(false);

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


        if(notinhaAtiva) {

            if(Input.GetKeyDown(KeyCode.E)) {

                notinha.SetActive(false);
                notinhaAtiva = false;

                TravaCamera();

            }
            
        }

        if (pertoPorta)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (pegouChave)
                {
                    Destroy(porta);
                }
            }
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

    void Movimentacao()
    {
        //Capturando inputs do jogador
        movimento = transform.right * Input.GetAxisRaw("Horizontal");
        movimento += transform.forward * Input.GetAxisRaw("Vertical");
 

        //Evitando multiplica��o no valor de movimento, ao andar em diagonal
        movimento = movimento.normalized;

        //Movendo personagem
        controller.Move(movimento * Time.deltaTime * vel);

        //Detectando se est� correndo ou n�o
        if (Input.GetButtonDown("Correr"))
        {
            //Aumentando a velocidade caso esteja
            vel *= correr;          
            if (vel > correndo)
            {
                vel = correndo;
            }
        }
        else if (Input.GetButtonUp("Correr"))
        {
            //Diminuindo a velocidade caso n�o estejas
            vel /= 4;
        }

        //Rota��o do personagem
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0), velRotacao * Time.deltaTime);
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Porta"))
        {
            pertoPorta = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Porta"))
        {
            pertoPorta = false;
        }
    }
}
