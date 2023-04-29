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


        for(int i = 0; i < titulosDasMensagens.Length; i++) {

            textoSlots[i].text = titulosDasMensagens[i];

            slotsMSG[i].gameObject.SetActive(true);

        }


        AtribuirFuncaoAoBotao();


    }

    void AtribuirFuncaoAoBotao () {

        for(int i = 0; i < titulosDasMensagens.Length; i++) {


            int aux = i;

            slotsMSG[i].onClick.AddListener(() => CliqueNaMensagem(aux));


        }

    }

    public void CliqueNaMensagem (int numeroDaMensagem) {

        mostrarConteudo.text = textoDasMensagens[numeroDaMensagem];

    }



    
}
