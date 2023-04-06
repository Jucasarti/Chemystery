using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectItem : MonoBehaviour
{
    public float rotateSpeed = 10f;
    private bool isRotating = false;

    private bool podeRotar = false;

    private Vector3 posicaoInicialItem;
    private Quaternion posRot;



    void Start() {

        posicaoInicialItem = transform.position;
        posRot = Quaternion.identity;

    }


    void Update()
    {   
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
        }


        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }


        if (isRotating && podeRotar)
        {
            float rotX = Input.GetAxis("Mouse X") * rotateSpeed ;
            float rotY = Input.GetAxis("Mouse Y") * rotateSpeed;

            transform.Rotate(Vector3.up, -rotX);
            transform.Rotate(Vector3.right, rotY);

        }
    
    }

    public void PodeRotacionar () {

        podeRotar = !podeRotar;

    }


    public void VoltarPosOriginal () {

        transform.position = posicaoInicialItem;
        transform.rotation = posRot;
        PodeRotacionar();

    }
} 
