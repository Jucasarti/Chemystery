using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PuzzleMaleta : MonoBehaviour, IInteractable
{

    [SerializeField] private string senhaCorreta;
    [SerializeField] private TextMeshProUGUI textoVisor;

    [SerializeField] private GameObject puzzle;

    [SerializeField] private Image background;

    [SerializeField] Color vermelho;

    [SerializeField] Color verde;

    [SerializeField] Color corInicial;
 
    [SerializeField] private float intervalo = 0.2f;

    private ManagerPlayer player;

    private Crosshair crosshair;

    private bool puzzleAtivo = false;


    private int contadorSenha = 0;

    public void Interagir()
    {
        puzzle.SetActive(true);

        crosshair.DesativarCrosshair();

        player.TravaCamera();

        player.EstaInspecionando();
        
        puzzleAtivo = true;


    }

    void Awake() {

        player = FindObjectOfType<ManagerPlayer>();

        crosshair = FindObjectOfType<Crosshair>();


    }


    public void DigitarSenha(string numero) {

        textoVisor.text += numero;

        contadorSenha++;

        if(contadorSenha >= 6) {

            ConferirSenha();

        }

    }


    private void ConferirSenha() {

        if(textoVisor.text == senhaCorreta) {

            StartCoroutine(AcertouSenha());

        } else {

            StartCoroutine(ErrouSenha());

        }

    }

    //private void AcertouSenha () {

        //Debug.Log("Acertou a senha");

        //puzzle.SetActive(false);

        //player.EstaInspecionando();

        //player.TravaCamera();
//
        //crosshair.AtivarCrosshair();

    //}

    private IEnumerator AcertouSenha() {

        //tocar um audio

        
        yield return new WaitForSeconds(intervalo);

        background.color = verde;

        yield return new WaitForSeconds(intervalo);

        background.color = corInicial;

        yield return new WaitForSeconds(intervalo);

        background.color = verde;

        yield return new WaitForSeconds(intervalo);

        background.color = corInicial;

        yield return new WaitForSeconds(intervalo);

        background.color = verde;

        yield return new WaitForSeconds(2);

        Debug.Log("Acertou a senha");

        puzzle.SetActive(false);

        player.EstaInspecionando();

        player.TravaCamera();

        crosshair.AtivarCrosshair();

    }

    private IEnumerator ErrouSenha () {

        yield return new WaitForSeconds(intervalo);

        background.color = vermelho;

        Debug.Log("Alo");

        yield return new WaitForSeconds(intervalo);

        background.color = corInicial;

        yield return new WaitForSeconds(intervalo);

        background.color = vermelho;

        yield return new WaitForSeconds(intervalo);

        background.color = corInicial;

        yield return new WaitForSeconds(intervalo);

        background.color = vermelho;

        yield return new WaitForSeconds(2);

        contadorSenha = 0;

        textoVisor.text = "";

        background.color = corInicial;


    }


    //private void ErrouSenha() {

        //contadorSenha = 0;

        //textoVisor.text = "";

    //}

    void Update () {

        if(puzzleAtivo) {

            if(Input.GetKeyDown(KeyCode.Space)) {

                contadorSenha = 0;

                textoVisor.text = "";

                puzzle.SetActive(false);

                puzzleAtivo = false;

                crosshair.AtivarCrosshair();

                player.TravaCamera();
                
                player.EstaInspecionando();

            }

        }


    }


}
