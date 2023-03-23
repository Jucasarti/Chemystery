using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectItem : MonoBehaviour
{
    public float rotateSpeed = 10f;
    private bool isRotating = false;

    private Vector3 posicaoInicialItem;
    public Transform posicaoParaRotacao;

    private GameObject item;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Item"))
                {
                    isRotating = true;
                    item = hit.collider.gameObject;
                    posicaoInicialItem = item.transform.position;
                    item.transform.position = posicaoParaRotacao.position;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
            if(item != null){

                item.transform.position = posicaoInicialItem;
                item.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (isRotating)
        {
            float rotX = Input.GetAxis("Mouse X") * rotateSpeed ;
            float rotY = Input.GetAxis("Mouse Y") * rotateSpeed;

            item.transform.Rotate(Vector3.up, -rotX);
            item.transform.Rotate(Vector3.right, rotY);

        }
    
    }
}
