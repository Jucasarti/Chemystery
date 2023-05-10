using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSom : MonoBehaviour
{
    [SerializeField] Slider sliderMasterVolume;

    [SerializeField] GameObject menuGameObject;

    [SerializeField] AudioSource sourceSoundtrack;

    [SerializeField] Button mutarMusicaBotao, mutarSFXBotao;

    [SerializeField] Sprite botaoMusicaMutado, botaoMusicaAtiva, botaoSFXmutado, botaoSFXativo;

    void Start() {

        AudioListener.volume = sliderMasterVolume.value;

        sourceSoundtrack.ignoreListenerPause = true;

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

        if(AudioListener.pause == true) {

            AudioListener.pause = false;
            
            mutarSFXBotao.image.sprite = botaoSFXativo;

        } else {

            AudioListener.pause = true;

            mutarSFXBotao.image.sprite = botaoSFXmutado;

        }

    }

    public void MutandoSoundtrack () {

        sourceSoundtrack.mute = !sourceSoundtrack.mute;

        if(sourceSoundtrack.mute == true) {

            mutarMusicaBotao.image.sprite = botaoMusicaMutado;

        } else {

            mutarMusicaBotao.image.sprite = botaoMusicaAtiva;

        }

    }
}
