using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour, IInteractable
{
    public bool precisaDeChave = false;

    private bool pegouChave = false;

    private Aviso aviso;

    private Vector3 posFechada;

    private Vector3 posAberta;

    private float intervalo = 0.8f;


    void Awake () {

        aviso = FindObjectOfType<Aviso>();

    }

    void Start() {

        posFechada = transform.position;

        posAberta = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);

    }


    public void Interagir() {

        if(precisaDeChave) {

            if(pegouChave) {

                Destroy(gameObject);
                //AbrirPorta();

            } else {

                aviso.AvisoDaPorta();

                }

        } else {

            //AbrirPorta();
            Destroy(gameObject);

        }

    }

    public void AbrirPorta() {

        StartCoroutine(IAbrirPorta());

    }

    IEnumerator IAbrirPorta () {

        Debug.Log("Posicao: " + transform.position.y);

        //transform.position = Vector3.MoveTowards(posFechada, posAberta, intervalo);

        Debug.Log("Posicao depois: " + transform.position.y);

        yield return new WaitForSeconds(5);

        //transform.position = Vector3.MoveTowards(posAberta, posFechada, intervalo);
        
    }


    public void DestrancarPorta() {

        pegouChave = true;

    }



}
