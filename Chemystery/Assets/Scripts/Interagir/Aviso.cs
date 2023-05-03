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

    [Header("Textos dos avisos")]
    [SerializeField] private string avisoDaPorta;
    [SerializeField] private string avisoDoPc;

    void Awake() {

        animator = GetComponentInChildren<Animator>();

        corInicial = textAviso.color;

    }





    public void AvisoDaPorta() {

        textAviso.text = avisoDaPorta;

        Debug.Log("Passei por aqui hein: " + textAviso.text);

        StartCoroutine(EscreverTexto());

    }

    public void AvisoDoPc () {

        textAviso.text = avisoDoPc;

        StartCoroutine(EscreverTexto());

    }



    private IEnumerator EscreverTexto () {

        //textAviso.ForceMeshUpdate();

        Debug.Log("Characteres: " + textAviso.textInfo.characterCount);

        textAviso.maxVisibleCharacters = 0;
        var charCount = textAviso.textInfo.characterCount;

        textAviso.enabled = true;

        Debug.Log("Characteres: " + charCount);

        while(textAviso.maxVisibleCharacters <= charCount) {

            yield return new WaitForSeconds (intervalo);
            textAviso.maxVisibleCharacters++;

            Debug.Log("Mais uma letra");

        }

        Debug.Log("Sai do loop hein");

        yield return new WaitForSeconds (3f);

        textAviso.enabled = false;

    }

}
