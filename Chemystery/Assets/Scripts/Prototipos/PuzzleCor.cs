using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleCor : MonoBehaviour
{
    public int contador = 0;

    private TrocarCor item;

    public TrocarCor[] quadrados;

    public ManagerPlayer player;

    public bool pertoPuzzle, olhandoPuzzle, modoPuzzle;

    Collider colisao;

    ContatoPuzzleCor olhandoPuzzleCor;

    public Text texto;

    void Start()
    {
        colisao = GetComponent<Collider>();
    }

    void Update()
    {

        if (pertoPuzzle)
        {
            Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            ContatoPuzzleCor olhando = null;

            if (Physics.Raycast(raio, out hit))
            {
                olhando = hit.transform.GetComponent<ContatoPuzzleCor>();
            }
            if (olhando != null)
            {
                olhandoPuzzle = true;
            }
            if (olhando != olhandoPuzzleCor)
            {
                if (olhandoPuzzleCor != null)
                {
                    olhandoPuzzle = false;
                }
                olhandoPuzzleCor = olhando;
            }

            //Verificando se a tecla E foi pressionada
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Verificando se o player está andando e se está em contato com o puzzle para abri-lo
                if (!modoPuzzle && olhandoPuzzle)
                {
                    //Chamando função que inicia os puzzles
                    Puzzle();
                }

                //Verificando se o player não esta andando e não pode abrir puzzles, significanfo que ele já está em um puzzle, logo, ele fecha o puzzle
                else if (modoPuzzle)
                {
                    //Chamando a função que fecha os puzzles
                    SairPuzzle();
                }
            }
        }
        if (pertoPuzzle && !modoPuzzle && olhandoPuzzle)
        {
            //Habilitando texto de ajuda
            texto.enabled = true;
        }
        else
        {
            //Desabilitando texto de ajuda
            texto.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Habilitando distância minima para acionamento do puzzle
            pertoPuzzle = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Desabilitando distância minima para acionamento do puzzle
            pertoPuzzle = false;
        }
    }

    void Puzzle()
    {
        for (int i = 0; i < quadrados.Length; i++)
        {
            quadrados[i].gameObject.SetActive(true);
        }

        //Habilitando o modo puzzle
        modoPuzzle = true;

        //Desabilitando texto de ajuda
        texto.enabled = false;

        //Não permitindo o player andar enquanto estiver no puzzle
        player.Andando();

        player.TravaCamera();

        //Desabilitando a colisão com o player
        colisao.enabled = false;
    }

    void SairPuzzle()
    {
        for (int i = 0; i < quadrados.Length; i++)
        {
            quadrados[i].gameObject.SetActive(false);
        }

        //Habilitando o modo puzzle
        modoPuzzle = false;

        //Desabilitando texto de ajuda
        texto.enabled = true;

        //Não permitindo o player andar enquanto estiver no puzzle
        player.Andando();

        player.TravaCamera();

        //Desabilitando a colisão com o player
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
        //Definindo que ele não pode abrir puzzle
        pertoPuzzle = false;

        //Desabilitando o texto de ajuda
        texto.enabled = false;

        //Desabilitando o modo puzzle
        modoPuzzle = false;

        //Permitindo o player andar
        player.Andando();

        player.TravaCamera();

        for (int i = 0; i < quadrados.Length; i++)
        {
            quadrados[i].gameObject.SetActive(false);
        }

        colisao.enabled = false;
    }
}
