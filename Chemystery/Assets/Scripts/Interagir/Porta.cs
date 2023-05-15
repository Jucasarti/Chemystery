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

    }

    IEnumerator IAbrirPorta (Vector3 posInicial, Vector3 posFinal) {

        //Calculando a distância entre os cubos
        float distancia = Vector3.Distance(posInicial, posFinal);

        //Calculando duração da "animação"
        float duracao = distancia / 2f;

        //Determinando tempo para calculo do fim da "animação"
        float tempoDecorrido = 0f;

        while (tempoDecorrido < duracao)
        {
            //Movendo cubo
            transform.position = Vector3.Lerp(posInicial, posFinal, tempoDecorrido / duracao);
            //Adicionando a variável de controle do tempo
            tempoDecorrido += Time.deltaTime;
            yield return null;
        }

        //Determinando a posição final do cubo clicado
        transform.position = posFinal;

    }


    public void DestrancarPorta() {

        pegouChave = true;

    }



}
