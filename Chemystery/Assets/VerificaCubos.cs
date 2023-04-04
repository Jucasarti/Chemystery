using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificaCubos : MonoBehaviour
{
    public PuzzleCubos []cubos;
    public CuboVazio cuboVazio;
    int jogadas = 5, contador = 0;

    public void VerificaFim()
    {
        jogadas--;
        if(jogadas == 0)
        {
            for(int i= 0; i < cubos.Length; i++)
            {
                if (cubos[i].fim)
                {
                    contador++;
                }
            }

            Debug.Log(contador);
            if (contador == 5)
            {
                for (int i = 0; i < cubos.Length; i++)
                    cubos[i].FimPuzzle();
                Debug.Log("Ganhei");
            }
            else
            {
                ResetaPuzzle();
                Debug.Log("Perdi");
            }
        }  
    }

    public void ResetaPuzzle()
    {
        for(int j= 0; j < cubos.Length; j++)
        {
            cubos[j].gameObject.transform.position = cubos[j].posInicial;
        }

        jogadas = 5;
        contador = 0;

        cuboVazio.gameObject.transform.position = cuboVazio.posInicial;

        for (int i = 0; i < cubos.Length; i++)
        {
            cubos[i].SairPuzzle();
            cubos[i].fim = false;
        }
    }
}
