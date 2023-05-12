using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChequeAnotacoes : MonoBehaviour
{
    TextMeshProUGUI text;

    void Awake () {

        text = GetComponentInChildren<TextMeshProUGUI>();

    }


    void Start()
    {
        StartCoroutine(Cheque());

    }


    IEnumerator Cheque () {

        yield return new WaitForSeconds (2);

        text.enabled = true;

        yield return new WaitForSeconds (3);

        Destroy(gameObject);

    }
}
