using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSom : MonoBehaviour
{
    [SerializeField] Slider sliderMasterVolume;

    [SerializeField] GameObject menuGameObject;

    void Start() {

        AudioListener.volume = sliderMasterVolume.value;

    }

    public void AbrirMenu () {

        menuGameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.Confined;

    }

    public void FecharMenu () {

        menuGameObject.SetActive(false);

    }

    public void MudandoValorSom() {

        AudioListener.volume = sliderMasterVolume.value;

    }

    public void MutandoSFX() {

        AudioListener.pause = !AudioListener.pause;

    }
}
