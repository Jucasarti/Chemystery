using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private Image crosshair;

    [SerializeField] private Sprite crosshairNormal;

    [SerializeField] private Sprite crosshairInteracao;

    [SerializeField] private Sprite crosshairCadeado;

    private AudioSource sourcePortaTrancada;

    private bool cadeadoAtivado = false;

    void Awake () {

        crosshair = GetComponent<Image>();

        sourcePortaTrancada = GetComponent<AudioSource>();
    }


    public void CrosshairInteracao() {

        if(!cadeadoAtivado) {
            crosshair.sprite = crosshairInteracao;
        }
        

    }

    public void CrosshairNormal () {

        crosshair.sprite = crosshairNormal;

    }

    public void DesativarCrosshair() {

        crosshair.enabled = false;

    }

    public void AtivarCrosshair() {

        crosshair.enabled = true;

    }

    public void CrosshairCadeado () {

        StartCoroutine(CadeadoRoutine());

    }

    IEnumerator CadeadoRoutine () {

        if(!cadeadoAtivado) {
        crosshair.sprite = crosshairCadeado;
        cadeadoAtivado = true;
        sourcePortaTrancada.PlayOneShot(sourcePortaTrancada.clip);

        yield return new WaitForSeconds(1);

        crosshair.sprite = crosshairNormal;
        cadeadoAtivado = false;
        }

    }

    
}
