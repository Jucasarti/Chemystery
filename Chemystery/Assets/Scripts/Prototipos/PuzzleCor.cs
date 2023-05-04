using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleCor : MonoBehaviour, IInteractable
{
    public int contador = 0;
    private TrocarCor item;
    public TrocarCor[] quadrados;
    ManagerPlayer player;
    public bool modoPuzzle;
    Collider colisao;
    public GameObject background;

    private Aviso aviso;

    public bool energiaAtivada = false;

    private Crosshair crosshair;

    public void Interagir() {

        if(energiaAtivada) {

            Puzzle();

            crosshair.DesativarCrosshair();

        } else {

            aviso.AvisoDoPc();

        }
    }

    void Awake () {

        player = FindObjectOfType<ManagerPlayer>();

        colisao = GetComponent<Collider>();

        aviso = FindObjectOfType<Aviso>();

        crosshair = FindObjectOfType<Crosshair>();
    }

    void Update()
    {
        if(modoPuzzle) {

            if(Input.GetKeyDown(KeyCode.Space)) {

                SairPuzzle();
            }
        }
    }

    void Puzzle()
    {
        for (int i = 0; i < quadrados.Length; i++)
        {
            quadrados[i].gameObject.SetActive(true);
        }

        background.SetActive(true);

        //Habilitando o modo puzzle
        modoPuzzle = true;

        player.TravaCamera();

        //Desabilitando a colis�o com o player
        colisao.enabled = false;
    }

    public void SairPuzzle()
    {
        for (int i = 0; i < quadrados.Length; i++)
        {
            quadrados[i].gameObject.SetActive(false);
        }

        background.SetActive(false);

        crosshair.AtivarCrosshair();

        //Habilitando o modo puzzle
        modoPuzzle = false;

        player.TravaCamera();

        //Desabilitando a colis�o com o player
        colisao.enabled = true;
    }

    public void ConferirPuzzle()
    {
        for (int i = 0; i < quadrados.Length; i++)
        {

            if (quadrados[i].corCerta == true)
            {
                contador++;
                Debug.Log(contador);
            }

        }

        if (contador == 6)
        {
            Debug.Log("Terminou o puzzle");
            FimPuzzle();
        }
        else
        {
            Debug.Log("Nao terminou");
            contador = 0;
        }

    }

    public void FimPuzzle()
    {
        //Desabilitando o modo puzzle
        modoPuzzle = false;

        player.TravaCamera();

        for (int i = 0; i < quadrados.Length; i++)
        {
            quadrados[i].gameObject.SetActive(false);
        }
        
        background.SetActive(false);

        Destroy(gameObject);
    }
}
