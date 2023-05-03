using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Aviso : MonoBehaviour
{
    [SerializeField] TMP_Text textAviso;
    [SerializeField] private float intervalo = .2f;
    private Animator animator;
    private Color corInicial;

    private int totalCaracteres;

    [Header("Textos dos avisos")]
    [SerializeField] private string avisoDaPorta;
    [SerializeField] private string avisoDoPc;

    void Awake() {

        animator = GetComponentInChildren<Animator>();

        corInicial = textAviso.color;

    }





    public void AvisoDaPorta() {

        textAviso.text = avisoDaPorta;

        totalCaracteres = avisoDaPorta.Length;

        StartCoroutine(EscreverTexto());

    }

    public void AvisoDoPc () {

        textAviso.text = avisoDoPc;

        totalCaracteres = avisoDoPc.Length;

        StartCoroutine(EscreverTexto());

    }



    private IEnumerator EscreverTexto () {

        textAviso.maxVisibleCharacters = 0;

        textAviso.enabled = true;

        while(textAviso.maxVisibleCharacters <= totalCaracteres) {

            yield return new WaitForSeconds (intervalo);
            textAviso.maxVisibleCharacters++;

            Debug.Log("Mais uma letra");

        }

        Debug.Log("Sai do loop hein");

        yield return new WaitForSeconds (3f);

        textAviso.enabled = false;

    }

}
