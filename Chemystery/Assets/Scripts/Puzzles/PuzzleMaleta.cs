using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleMaleta : MonoBehaviour, IInteractable
{

    [SerializeField] private string senhaCorreta;
    [SerializeField] private TextMeshProUGUI textoVisor;

    [SerializeField] private GameObject puzzle;

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

            AcertouSenha();

        } else {

            ErrouSenha();

        }

    }

    private void AcertouSenha () {

        Debug.Log("Acertou a senha");

        puzzle.SetActive(false);

        player.EstaInspecionando();

        player.TravaCamera();

        crosshair.AtivarCrosshair();

    }

    private void ErrouSenha() {

        contadorSenha = 0;

        textoVisor.text = "";

    }

    void Update () {

        if(puzzleAtivo) {

            if(Input.GetKeyDown(KeyCode.Space)) {

                ErrouSenha();

                puzzle.SetActive(false);

                puzzleAtivo = false;

                crosshair.AtivarCrosshair();

                player.TravaCamera();
                
                player.EstaInspecionando();

            }

        }


    }


}
