using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Computador : MonoBehaviour, IInteractable
{

    [SerializeField] PCUI pcUI;
    [SerializeField] TextMeshProUGUI mostrarConteudo;

    Computador essePC;

    Scrollbar scrollbar;
    

    [SerializeField] Button botaoFecharPC;
    
    [Header("Audios")]

    [SerializeField] AudioSource sfxClickMouse;

    [Header("Slot das mensagens")]
    [SerializeField] Button[] slotsMSG;
    [SerializeField] TextMeshProUGUI[] textoSlots;

    [Header("Textos dos computadores")]
    [SerializeField] string nomeDoUsuarioDoPc;

    [SerializeField] string[] titulosDasMensagens;

    [TextArea(15, 10)]
    [SerializeField] string[] textoDasMensagens;

    [Header("Outras variáveis")]

    [SerializeField] TextMeshProUGUI nomeNoPCText;

    private Crosshair crosshair;
    private ManagerPlayer player;

    private Aviso aviso;

    void Awake() {

        player = FindObjectOfType<ManagerPlayer>();
        aviso = FindObjectOfType<Aviso>();

        essePC = GetComponent<Computador>();

        crosshair = FindObjectOfType<Crosshair>();

    }
    
    public void Interagir() {

        AbrirPC();

        crosshair.DesativarCrosshair();

        player.EstaInspecionando();

    }

        void AbrirPC () {

        player.TravaCamera();

        pcUI.LoadingScreen(essePC);



    }


    public void LigarPC() {

        scrollbar = FindObjectOfType<Scrollbar>();

        mostrarConteudo.text = "";

        nomeNoPCText.text = nomeDoUsuarioDoPc;

        for(int i = 0; i < titulosDasMensagens.Length; i++) {

            textoSlots[i].text = titulosDasMensagens[i];

            slotsMSG[i].gameObject.SetActive(true);

        }


        AtribuirFuncaoAoBotao();

        player.TirarSomAndar();


    }

    void AtribuirFuncaoAoBotao () {

        for(int i = 0; i < titulosDasMensagens.Length; i++) {


            int aux = i;

            slotsMSG[i].onClick.AddListener(() => CliqueNaMensagem(aux));

            slotsMSG[i].onClick.AddListener(() => ResetarScrollbar());

        }

        botaoFecharPC.onClick.AddListener(() => FecharPC());

    }

    public void CliqueNaMensagem (int numeroDaMensagem) {

        mostrarConteudo.text = textoDasMensagens[numeroDaMensagem];

        sfxClickMouse.PlayOneShot(sfxClickMouse.clip);

    }

    void ResetarScrollbar() {

        scrollbar.value = 1;

    }

    void FecharPC() {

        RetirarFuncaoDoBotao();

        for(int i = 0; i < titulosDasMensagens.Length; i++) {

            slotsMSG[i].gameObject.SetActive(false);

        }

        pcUI.FecharUI();

        crosshair.AtivarCrosshair();

        player.TravaCamera();
        player.EstaInspecionando();
        player.ColocarSomAndar();


    }

    void RetirarFuncaoDoBotao() {

        for(int i = 0; i < titulosDasMensagens.Length; i++) {

            slotsMSG[i].onClick.RemoveAllListeners();

        }

        botaoFecharPC.onClick.RemoveAllListeners();
    }



    
}
