using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
   [SerializeField] GameObject menuIinicalUI, controlesUI, creditosUI, andaresUI;


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

        andaresUI.SetActive(false);

    }

    public void AndaresUI()
    {

        menuIinicalUI.SetActive(false);

        andaresUI.SetActive(true);

    }

    public void BotaoSairDoJogo () {

        Application.Quit();

    }

    public void BotaoRecepcao()
    {

        SceneManager.LoadScene("TestePP");

    }

    public void BotaoSubsolo1()
    {

        SceneManager.LoadScene("Subsolo3");

    }

    public void BotaoAdmin()
    {

        SceneManager.LoadScene("2 Andar");

    }

    public void BotaoSubsolo2()
    {

        SceneManager.LoadScene("Subsolo2");

    }
}
