using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneFinal : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoCutscene;

    [SerializeField] GameObject continuar;

    [SerializeField] TextMeshProUGUI ofim, obrigado, booboots;

    [TextArea(10, 7)]
    [SerializeField] string[] textosDaCutscene;

    AudioSource source;

    private float intervalo = 0.02f;

    private int totalCaracteres;

    private int textoArrayIndex = 0;

    private bool estaEscrevendo = false;

    void Start() {

        source = GetComponent<AudioSource>();

        EscreverTexto();


    }


    void Update() {

        if(estaEscrevendo == false) {

            if(Input.GetMouseButtonDown(0)) {

                EscreverTexto();

            }

        }


    }




    void EscreverTexto() {

        if(textoArrayIndex >= textosDaCutscene.Length) {

            StartCoroutine(IrProximaCena());
            return;
        }

        totalCaracteres = textosDaCutscene[textoArrayIndex].Length;

        textoCutscene.text = textosDaCutscene[textoArrayIndex];

        StartCoroutine(EfeitoMaquinaEscrever());

    }


    private IEnumerator EfeitoMaquinaEscrever () {

        source.enabled = true;

        textoCutscene.maxVisibleCharacters = 0;

        estaEscrevendo = true;

        continuar.SetActive(false);

        textoCutscene.enabled = true;

        while(textoCutscene.maxVisibleCharacters <= totalCaracteres) {

            yield return new WaitForSeconds (intervalo);
            textoCutscene.maxVisibleCharacters++;

        }

        source.enabled = false;

        yield return new WaitForSeconds (1f);

        textoArrayIndex++;

        Debug.Log(textoArrayIndex);

        estaEscrevendo = false;

        continuar.SetActive(true);

    }



    IEnumerator IrProximaCena () {

        continuar.SetActive(false);
        textoCutscene.enabled = false;
        
        ofim.enabled = true;

        yield return new WaitForSeconds (1.5f);

        ofim.enabled = false;
        obrigado.enabled = true;

        yield return new WaitForSeconds (1.5f);

        obrigado.enabled = false;
        booboots.enabled = true;

        yield return new WaitForSeconds (2f);

        SceneManager.LoadScene("MainMenu");

    }
}
