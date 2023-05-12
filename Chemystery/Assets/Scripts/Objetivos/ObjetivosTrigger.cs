using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivosTrigger : MonoBehaviour
{
    private SistemaObjetivos sistemaObjetivos;
    [SerializeField] private string novoTextoObjetivo;
    void Awake() {

        sistemaObjetivos = FindObjectOfType<SistemaObjetivos>();

    }
    public void ColocarNovoObjetivo() {

        sistemaObjetivos.AtualizarObjetivo(novoTextoObjetivo);

    }
}
