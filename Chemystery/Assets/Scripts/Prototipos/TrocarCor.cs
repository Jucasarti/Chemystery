using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocarCor : MonoBehaviour
{
    public Color[] colors; // Array de cores predefinidas
    private int currentColorIndex = 0; // Índice da cor atual
    private Renderer objectRenderer; // Componente Renderer do objeto

    public bool corCerta;

    public int numeroCerto;

    PuzzleCor puzzleCor;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>(); // Obter o componente Renderer do objeto
        objectRenderer.material.color = colors[currentColorIndex]; // Definir a cor inicial do objeto
        puzzleCor = FindObjectOfType<PuzzleCor>();
    }

     

    public void TrocaCor () {

        currentColorIndex++; // Incrementar o índice da cor atual

        if (currentColorIndex >= colors.Length) // Verificar se o índice está fora do alcance do array de cores
        {
            currentColorIndex = 0; // Voltar para a primeira cor
        }
            objectRenderer.material.color = colors[currentColorIndex]; // Definir a nova cor do objeto

            if(numeroCerto == currentColorIndex) {
                
                Debug.Log("Cor certa");
                
                corCerta = true;
                puzzleCor.ConferirPuzzle();

            } else {
                
                Debug.Log("Cor errada");
                corCerta = false;
            }
        }



    }



