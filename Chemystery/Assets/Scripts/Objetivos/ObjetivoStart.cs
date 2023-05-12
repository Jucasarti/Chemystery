using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivoStart : MonoBehaviour
{
    [SerializeField] ObjetivosTrigger trigger;

    void Start() {

        StartCoroutine(Objetivo());

    }

    IEnumerator Objetivo () {

        yield return new WaitForSeconds(2f);

        trigger.ColocarNovoObjetivo();


    }
}
