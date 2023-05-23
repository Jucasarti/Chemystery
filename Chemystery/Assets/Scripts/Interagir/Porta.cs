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

    private BoxCollider portaCollider;

    private AudioSource portaAbrindoSFX;

    private float intervalo = 0.8f;


    void Awake () {

        aviso = FindObjectOfType<Aviso>();

        portaCollider = GetComponent<BoxCollider>();

        portaAbrindoSFX = GetComponent<AudioSource>();
    }

    void Start() {

        posFechada = transform.position;

        posAberta = new Vector3(transform.position.x, transform.position.y + 3.8f, transform.position.z);

    }


    public void Interagir() {

        if(precisaDeChave) {

            if(pegouChave) {

                AbrirPorta();

            } else {

                aviso.AvisoDaPorta();

                }

        } else {

            AbrirPorta();

        }

    }

    public void AbrirPorta() {

        StartCoroutine(IAbrirPorta(posFechada, posAberta));

        portaAbrindoSFX.PlayOneShot(portaAbrindoSFX.clip);

    }

    IEnumerator IAbrirPorta (Vector3 posInicial, Vector3 posFinal) {

        //Calculando a dist�ncia entre os cubos
        float distancia = Vector3.Distance(posInicial, posFinal);

        //Calculando dura��o da "anima��o"
        float duracao = distancia / 2f;

        portaCollider.enabled = false;

        //Determinando tempo para calculo do fim da "anima��o"
        float tempoDecorrido = 0f;

        while (tempoDecorrido < duracao)
        {
            //Movendo cubo
            transform.position = Vector3.Lerp(posInicial, posFinal, tempoDecorrido / duracao);
            //Adicionando a vari�vel de controle do tempo
            tempoDecorrido += Time.deltaTime;
            yield return null;
        }

        //Determinando a posi��o final do cubo clicado
        transform.position = posFinal;

    }


    public void DestrancarPorta() {

        pegouChave = true;

    }



}
