using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Computador : MonoBehaviour, IInteractable
{

    [SerializeField] GameObject pcUI;
    [SerializeField] TextMeshProUGUI mostrarConteudo;
    

    [SerializeField] Button botaoFecharPC;
    
    [Header("Audios")]

    [SerializeField] AudioSource sfxClickMouse;

    [SerializeField] AudioSource sfxLigarPc;
 


    [Header("Slot das mensagens")]
    [SerializeField] Button[] slotsMSG;
    [SerializeField] TextMeshProUGUI[] textoSlots;

    [Header("Textos dos computadores")]
    [SerializeField] string nomeDoUsuarioDoPc;

    [SerializeField] string[] titulosDasMensagens;

    [TextArea(10, 7)]
    [SerializeField] string[] textoDasMensagens;

    [Header("Outras variáveis")]

    [SerializeField] TextMeshProUGUI nomeNoPCText;

    private Crosshair crosshair;
    private ManagerPlayer player;

    private Aviso aviso;

    void Awake() {

        player = FindObjectOfType<ManagerPlayer>();
        aviso = FindObjectOfType<Aviso>();

        crosshair = FindObjectOfType<Crosshair>();

    }
    
    public void Interagir() {

        AbrirPC();

        crosshair.DesativarCrosshair();

    }


    public void AbrirPC () {

        player.TravaCamera();

        pcUI.SetActive(true);

        mostrarConteudo.text = "";

        nomeNoPCText.text = nomeDoUsuarioDoPc;

        //sfxLigarPc.PlayOneShot(sfxLigarPc.clip);


        for(int i = 0; i < titulosDasMensagens.Length; i++) {

            textoSlots[i].text = titulosDasMensagens[i];

            slotsMSG[i].gameObject.SetActive(true);

        }


        AtribuirFuncaoAoBotao();

        player.EstaInspecionando();


    }

    void AtribuirFuncaoAoBotao () {

        for(int i = 0; i < titulosDasMensagens.Length; i++) {


            int aux = i;

            slotsMSG[i].onClick.AddListener(() => CliqueNaMensagem(aux));


        }

        botaoFecharPC.onClick.AddListener(() => FecharPC());

    }

    public void CliqueNaMensagem (int numeroDaMensagem) {

        mostrarConteudo.text = textoDasMensagens[numeroDaMensagem];

        sfxClickMouse.PlayOneShot(sfxClickMouse.clip);

    }

    void FecharPC() {

        RetirarFuncaoDoBotao();

        for(int i = 0; i < titulosDasMensagens.Length; i++) {

            slotsMSG[i].gameObject.SetActive(false);

        }

        pcUI.SetActive(false);

        crosshair.AtivarCrosshair();

        player.TravaCamera();
        player.EstaInspecionando();


    }

    void RetirarFuncaoDoBotao() {

        for(int i = 0; i < titulosDasMensagens.Length; i++) {

            slotsMSG[i].onClick.RemoveAllListeners();

        }

        botaoFecharPC.onClick.RemoveAllListeners();
    }



    
}
