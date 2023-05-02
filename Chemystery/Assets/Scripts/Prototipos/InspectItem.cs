using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectItem : MonoBehaviour, IInteractable
{
    public float rotateSpeed = 10f;
    private bool isRotating = false;

    private bool podeRotar = false;

    ManagerPlayer player;

    [SerializeField] private GameObject canvasInspectItem;

    [SerializeField] private string qualObjeto;

    Chave chave;


    public void Interagir() {

        Inspecionar();

    }



    void Awake() {

        player = FindObjectOfType<ManagerPlayer>();

        if(qualObjeto == "Chave") {

            chave = GetComponent<Chave>();

        }

    }

    void Inspecionar() {

        transform.position = player.objeto.transform.position;
        
        PodeRotacionar();

        canvasInspectItem.SetActive(true);

        player.TravaCamera();
        player.EstaInspecionando();
        

    }

    void PegarItem() {

        canvasInspectItem.SetActive(false);
        player.TravaCamera();
        player.EstaInspecionando();
        
        if(chave != null) {

            chave.PegarChave();

        }


        Destroy(gameObject);



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

        if(podeRotar) {

            if(Input.GetKeyDown(KeyCode.Space)) {

                PegarItem();
            }


        }
    
    }

    public void PodeRotacionar () {

        podeRotar = !podeRotar;

    }

} 
