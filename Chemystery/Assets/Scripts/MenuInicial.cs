using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
   [SerializeField] GameObject menuIinicalUI, controlesUI, creditosUI;


    void Start() {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

    }


    public void BotaoJogar() {

        SceneManager.LoadScene("CutsceneInicial");

    }

    public void ControlesUI() {

        menuIinicalUI.SetActive(false);

        controlesUI.SetActive(true);

    }

    public void CreditosUI () {

        menuIinicalUI.SetActive(false);

        creditosUI.SetActive(true);

    } 

    public void BotaoVoltar () {

        menuIinicalUI.SetActive(true);

        creditosUI.SetActive(false);

        controlesUI.SetActive(false);

    }

    public void BotaoSairDoJogo () {

        Application.Quit();

    }
}
