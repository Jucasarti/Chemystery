using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{       
    public static MenuPausa instance;

    [SerializeField] GameObject menuPausa;

    [SerializeField] GameObject anotacoesUI;

    ManagerPlayer player;
    public static bool jogoPausado = false;

    SistemaObjetivos objetivos;

    void Awake() {

        if(instance == null) {

            instance = this;
            DontDestroyOnLoad(gameObject);

        } else {

            Destroy(gameObject);
            return;

        }

        player = FindObjectOfType<ManagerPlayer>();

        objetivos = FindObjectOfType<SistemaObjetivos>();
    }


    public void PausarJogo() {

        menuPausa.SetActive(true);

        objetivos.MostrarObjetivoAtual();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        Time.timeScale = 0f;

        jogoPausado = true;

    }

    public void DespausarJogo() {

        menuPausa.SetActive(false);

        objetivos.FecharObjetivoAtual();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;

        jogoPausado = false;

    }

    public void BotaoMainMenu () {

        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");

    }

    public void FecharJogo () {

        Application.Quit();

    }

    public void Anotacoes () {

        anotacoesUI.SetActive(true);

        menuPausa.SetActive(false);

        objetivos.FecharObjetivoAtual();


    }

    public void VoltarAnotacoes () {

        anotacoesUI.SetActive(false);

        menuPausa.SetActive(true);

        objetivos.MostrarObjetivoAtual();

    }



    void Update()
    {   
        if(player.jaEstaInspecionando == false) {

        if(Input.GetKeyDown(KeyCode.Escape)) {

            if(jogoPausado) {

                DespausarJogo();

            } else {

                PausarJogo();

            }
        }
    }

    }
}
