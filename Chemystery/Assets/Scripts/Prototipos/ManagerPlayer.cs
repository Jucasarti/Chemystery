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
    public bool andar;
    public bool pegouChave = false;

    bool puzzleCorAtivo = false;

    [Header("Variaveis Float")]
    public float velRotacao;
    public float vel;
    public float correr;
    public float correndo;

    [Header("Outros")]
    public Transform cameraTransform;
    public CinemachineBrain cinemachine;
    CharacterController controller;
    Vector3 movimento = Vector3.zero;
    public GameObject objeto;
    Camera cam;

    [Header("Inspeção dos Itens")]
    InspectItem inspectItem;

    public GameObject canvasInspectItens;

    bool estaRotacionando = false;

    [Header("PuzzleCor")] 
    public GameObject puzzleCor;


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
        //Verificando se o player pode andar
        if (andar)
        {
            Movimentacao();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            TravaCamera();
        }

        if(Input.GetMouseButton(0)) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit)) {

                if(hit.collider.gameObject.CompareTag("Item")) {
                    
                    if(estaRotacionando == false) {

                        inspectItem = hit.collider.gameObject.GetComponent<InspectItem>();

                        InspecionarItem(inspectItem);
                    }

                }

            }

        }

        if(estaRotacionando) {

            if(Input.GetKeyDown(KeyCode.E)) {

                PegouItem();
            }

            if(Input.GetKeyDown(KeyCode.Space)) {

                NaoPegouItem();
            }

        }
    }

    void InspecionarItem (InspectItem itemAtual) {

        estaRotacionando = true;

        TravaCamera();

        Cursor.lockState = CursorLockMode.Confined;

        itemAtual.gameObject.transform.position = objeto.transform.position;

        canvasInspectItens.gameObject.SetActive(true);

        itemAtual.PodeRotacionar();

    }

    void PegouItem () {

        if(inspectItem != null) {

            Destroy(inspectItem.gameObject);
            pegouChave = true;
            inspectItem = null;

            estaRotacionando = false;
            canvasInspectItens.gameObject.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            TravaCamera();

        }
    } 

    void NaoPegouItem () {

        if(inspectItem != null) {

            inspectItem.VoltarPosOriginal();
            TravaCamera();
            inspectItem = null;
            estaRotacionando = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            canvasInspectItens.gameObject.SetActive(false);
        }


    }

    public void Andando()
    {
        andar = !andar;
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

}
