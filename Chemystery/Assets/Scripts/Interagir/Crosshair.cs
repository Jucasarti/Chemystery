using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private Image crosshair;

    [SerializeField] private Sprite crosshairNormal;

    [SerializeField] private Sprite crosshairInteracao;

    void Awake () {

        crosshair = GetComponent<Image>();

    }


    public void CrosshairInteracao() {

        crosshair.sprite = crosshairInteracao;

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
}
