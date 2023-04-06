using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrocarCor : MonoBehaviour
{
    public Color[] colors; // Array de cores predefinidas
    private int currentColorIndex = 0; // Índice da cor atual
    private Image objectImage; // Componente Image do objeto

    public bool corCerta;

    public int numeroCerto;

    public int corInicial;

    PuzzleCor puzzleCor;

    void Start()
    {
        objectImage = GetComponent<Image>(); // Obter o componente Image do objeto
        objectImage.color = colors[corInicial]; // Definir a cor inicial do objeto
        puzzleCor = FindObjectOfType<PuzzleCor>();
    }



    public void TrocaCor()
    {
        currentColorIndex++; // Incrementar o índice da cor atual

        if (currentColorIndex >= colors.Length) // Verificar se o índice está fora do alcance do array de cores
        {
            currentColorIndex = 0; // Voltar para a primeira cor
        }
        objectImage.color = colors[currentColorIndex]; // Definir a nova cor do objeto

        if (numeroCerto == currentColorIndex)
        {
            Debug.Log("Cor certa");
            corCerta = true;
            puzzleCor.ConferirPuzzle();
        }
        else
        {
            Debug.Log("Cor errada");
            corCerta = false;
        }
    }
}



