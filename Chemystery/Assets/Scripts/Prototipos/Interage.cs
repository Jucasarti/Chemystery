using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class Interage : MonoBehaviour
{
    public bool abrirPuzzle, olhandoPuzzle;
    public Text texto;
    public GameObject[] interagindo;
    Collider colisao;

    void Start()
    {
        colisao = gameObject.GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Definindo que o player pode abirir o puzzle ao entrar no raio de interação com o puzzle
            abrirPuzzle = true;
            texto.enabled = true;

            for(int i = 0; i < interagindo.Length; i++)
            {
                interagindo[i].GetComponent<PuzzleCubos>().Interagindo();
            }


        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Definindo que o player não pode abirir o puzzle ao sair no raio de interação com o puzzle
            abrirPuzzle = false;
            texto.enabled = false;

            colisao.enabled = true;

            for (int i = 0; i < interagindo.Length; i++)
            {
                interagindo[i].GetComponent<PuzzleCubos>().Interagindo();
            }
        }
    }
}
