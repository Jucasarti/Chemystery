using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteDeObjetivo : MonoBehaviour
{
    public ObjetivosTrigger trigger;

    void OnTriggerEnter (Collider colisao) {

        trigger.ColocarNovoObjetivo();

    }

}
