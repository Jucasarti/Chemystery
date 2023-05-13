using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCUI : MonoBehaviour
{
    [SerializeField] GameObject backgroundLoading;

    [SerializeField] GameObject background;

    [SerializeField] Image loadingBar;

    Computador computador;

    [SerializeField] AudioSource source;

    public void LoadingScreen(Computador computadorAux) {

        computador = computadorAux;

        StartCoroutine(Loading(computador));

    }


    IEnumerator Loading(Computador pc) {

        backgroundLoading.SetActive(true);

        source.PlayOneShot(source.clip);

        while(loadingBar.fillAmount < 1) {

            yield return new WaitForSeconds(0.1f); 

            loadingBar.fillAmount += 0.025f;

        }

        loadingBar.fillAmount = 0;

        backgroundLoading.SetActive(false);

        background.SetActive(true);

        pc.LigarPC();


    }


    public void FecharUI () {

        background.SetActive(false);

        backgroundLoading.SetActive(false);


    }

}
