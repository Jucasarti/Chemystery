using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{       
    public static MenuPausa instance;

    [SerializeField] GameObject menuPausa;

    ManagerPlayer player;
    public static bool jogoPausado = false;

    void Awake() {

        if(instance == null) {

            instance = this;
            DontDestroyOnLoad(gameObject);

        } else {

            Destroy(gameObject);
            return;

        }

        player = FindObjectOfType<ManagerPlayer>();
    }


    public void PausarJogo() {

        menuPausa.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        Time.timeScale = 0f;

        jogoPausado = true;

        AudioListener.pause = true;

    }

    public void DespausarJogo() {

        menuPausa.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;

        jogoPausado = false;

        AudioListener.pause = false;
    }

    public void BotaoMainMenu () {

        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");

    }

    public void FecharJogo () {

        Application.Quit();

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
