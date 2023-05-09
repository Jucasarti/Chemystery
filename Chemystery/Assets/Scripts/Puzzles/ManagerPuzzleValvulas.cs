using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPuzzleValvulas : MonoBehaviour
{
    [SerializeField] GameObject gases1, gases2, gases3, gases4;

    Chave chave;

    private int hubValvulas1 = 0, hubValvulas2 = 0, hubValvulas3 = 0, hubValvulas4 = 0;

    void Awake () {

        chave = FindObjectOfType<Chave>();

    }


    public void ColocarValvula1 () {

        hubValvulas1++;

        gases1.SetActive(false);

        VerificarFim();


    }

    public void TirarValvula1 () {

        hubValvulas1--;

        if(hubValvulas1 <= 0) {

            gases1.SetActive(true);

        }

    }

    public void ColocarValvula2 () {

        hubValvulas2++;

        gases2.SetActive(false);

        VerificarFim();


    }

    public void TirarValvula2 () {

        hubValvulas2--;

        if(hubValvulas2 <= 0) {

            gases2.SetActive(true);

        }

    }

    public void ColocarValvula3 () {

        hubValvulas3++;

        gases3.SetActive(false);

        VerificarFim();


    }

    public void TirarValvula3 () {

        hubValvulas3--;

        if(hubValvulas3 <= 0) {

            gases3.SetActive(true);

        }

    }

    public void ColocarValvula4 () {

        hubValvulas4++;

        gases4.SetActive(false);

        VerificarFim();


    }

    public void TirarValvula4 () {

        hubValvulas4--;

        if(hubValvulas4 <= 0) {

            gases4.SetActive(true);

        }

    }

    void VerificarFim () {

        if(hubValvulas1 > 0 && hubValvulas2 > 0 && hubValvulas3 > 0 && hubValvulas4 > 0) {

            chave.PegarChave();

            Debug.Log("Abriu a porta final");

        }
    }




}
