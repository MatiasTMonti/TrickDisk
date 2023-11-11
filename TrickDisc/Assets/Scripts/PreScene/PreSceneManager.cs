using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PreSceneManager : MonoBehaviour
{
    [SerializeField] private string menuSceneName;
    [SerializeField] private float tiempoMinimoDeEspera = 1.0f;

    private void Start()
    {
        // Inicia una carga asíncrona de la escena del menú en segundo plano
        StartCoroutine(CargarMenuAsync());
    }

    private IEnumerator CargarMenuAsync()
    {
        // Carga de manera asíncrona la escena del menú
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(menuSceneName);

        // Hace que la operación no se complete hasta que el juego esté cargado
        asyncOperation.allowSceneActivation = false;

        // Espera hasta que se cumpla el tiempo mínimo
        yield return new WaitForSeconds(tiempoMinimoDeEspera);

        // Permitir que la operación se complete y cambie a la escena del menú
        asyncOperation.allowSceneActivation = true;
    }
}
