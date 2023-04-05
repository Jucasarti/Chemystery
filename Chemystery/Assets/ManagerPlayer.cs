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
    Quaternion rotCam;
    Camera cam;

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
 

        //Evitando multiplicação no valor de movimento, ao andar em diagonal
        movimento = movimento.normalized;

        //Movendo personagem
        controller.Move(movimento * Time.deltaTime * vel);

        //Detectando se está correndo ou não
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
            //Diminuindo a velocidade caso não estejas
            vel /= 4;
        }

        //Rotação do personagem
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0), velRotacao * Time.deltaTime);
    }

    void TravaCamera()
    {
        if (cinemachine.enabled)
        {
            cinemachine.enabled = false;
            Camera.main.transform.LookAt(objeto.transform.position);
            Andando();
        }
        else
        {
            cinemachine.enabled = true;
            Andando();
        }
    }
}
