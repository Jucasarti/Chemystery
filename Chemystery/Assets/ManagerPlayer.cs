using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.InputSystem.XR;
using Cinemachine;


public class ManagerPlayer : MonoBehaviour
{
    [Header("Variaveis Booleanas")]
    public bool andar;

    [Header("Variaveis Float")]
    public float vel;
    public float correr;
    public float velRotacao;

    [Header("Outros")]
    public Transform cameraTransform;
    public CinemachineBrain cinemachine;
    CharacterController controller;
    Vector3 movimento = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //Ativando o controlller do personagem
        controller = GetComponent<CharacterController>();

        //Desativando o cursor e travando a tela
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Verificando se o player pode andar
        if (andar)
        {
            //Capturando inputs do jogador
            movimento = transform.right * Input.GetAxis("Horizontal");
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
            }
            else if (Input.GetButtonUp("Correr"))
            {
                //Diminuindo a velocidade caso não estejas
                vel /= correr;
            }

            //Rotação do personagem
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0), velRotacao * Time.deltaTime);
        }
    }

    public void Andando()
    {
        if (andar)
            andar = false;
        else
            andar = true;
    }
}
