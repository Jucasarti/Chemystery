using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneInicial : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoCutscene;

    [SerializeField] GameObject continuar;

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

            IrProximaCena();
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

        estaEscrevendo = false;

        continuar.SetActive(true);

    }



    void IrProximaCena () {

        SceneManager.LoadScene("Level Blocagem");

    }

}
