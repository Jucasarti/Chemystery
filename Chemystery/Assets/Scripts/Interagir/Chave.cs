using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : MonoBehaviour
{
    [SerializeField] private Porta porta;


    public void PegarChave() {

        porta.DestrancarPorta();

    }
}
