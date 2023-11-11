using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PreSceneManager : MonoBehaviour
{
    [SerializeField] private string menuSceneName;
    [SerializeField] private float tiempoMinimoDeEspera = 1.0f;

    private void Start()
    {
        // Inicia una carga as�ncrona de la escena del men� en segundo plano
        StartCoroutine(CargarMenuAsync());
    }

    private IEnumerator CargarMenuAsync()
    {
        // Carga de manera as�ncrona la escena del men�
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(menuSceneName);

        // Hace que la operaci�n no se complete hasta que el juego est� cargado
        asyncOperation.allowSceneActivation = false;

        // Espera hasta que se cumpla el tiempo m�nimo
        yield return new WaitForSeconds(tiempoMinimoDeEspera);

        // Permitir que la operaci�n se complete y cambie a la escena del men�
        asyncOperation.allowSceneActivation = true;
    }
}
