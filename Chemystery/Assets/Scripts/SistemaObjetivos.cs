using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SistemaObjetivos : MonoBehaviour
{
    [SerializeField] private TMP_Text textoObjetivos;
    [SerializeField] private GameObject backgroundObject;
    [SerializeField] private TextMeshProUGUI textoObjetivoAtulizado;
    [SerializeField] private float intervalo = .05f;

    private int numeroDeCaracteres;
    private string objetivoText;



    public void AtualizarObjetivo (string qualObjetivo) {

        objetivoText = qualObjetivo;

        numeroDeCaracteres = objetivoText.Length;

        StartCoroutine(EscreverTexto());

    }



    private IEnumerator EscreverTexto() {

        textoObjetivos.text = objetivoText;
        textoObjetivos.maxVisibleCharacters = 0;

        textoObjetivoAtulizado.enabled = true;

        yield return new WaitForSeconds(1f);

        backgroundObject.SetActive(true);

        while (textoObjetivos.maxVisibleCharacters <= numeroDeCaracteres) {

            yield return new WaitForSeconds(intervalo);

            textoObjetivos.maxVisibleCharacters++;

        }

        yield return new WaitForSeconds(3f);

        backgroundObject.SetActive(false);
        textoObjetivoAtulizado.enabled = false;

    }


}
