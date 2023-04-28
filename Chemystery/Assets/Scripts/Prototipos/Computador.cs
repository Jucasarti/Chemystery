using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Computador : MonoBehaviour
{

    [SerializeField] GameObject pcUI;

    [SerializeField] TextMeshProUGUI mostrarConteudo;

    [Header("Slot das mensagens")]
    [SerializeField] Button[] slotsMSG;
    [SerializeField] TextMeshProUGUI[] textoSlots;

    [Header("Textos dos computadores")]
    [SerializeField] string nomeDoUsuarioDoPc;

    [SerializeField] string[] titulosDasMensagens;

    [SerializeField] string[] textoDasMensagens;

    //[Header("Outras variáveis")]

    

    void Start() {

        AbrirPC();

    }


    public void AbrirPC () {

        pcUI.SetActive(true);

        mostrarConteudo.text = "";

        AtribuirFuncaoAoBotao();


        for(int i = 0; i < titulosDasMensagens.Length; i++) {

            textoSlots[i].text = titulosDasMensagens[i];

            slotsMSG[i].gameObject.SetActive(true);

        }


    }

    void AtribuirFuncaoAoBotao () {

        for(int i = 0; i < titulosDasMensagens.Length; i++) {

            Debug.Log("I antes:" + i);

            slotsMSG[i].onClick.AddListener(() => CliqueNaMensagem(i));

            Debug.Log("I depois:" + i);

        }

    }

    public void CliqueNaMensagem (int numeroDaMensagem) {

        Debug.Log("Numero da msg:" + numeroDaMensagem);

        mostrarConteudo.text = textoDasMensagens[numeroDaMensagem];

    }



    
}
