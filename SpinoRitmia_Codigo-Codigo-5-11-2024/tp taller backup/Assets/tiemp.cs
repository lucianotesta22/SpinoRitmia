using UnityEngine;
using UnityEngine.UI;

public class ContadorSegundos : MonoBehaviour
{
    public Text contadorTexto; // Asigna aquí un componente Text desde el Inspector.
    private int segundosTranscurridos = 0; // Almacena los segundos contados.
    private float tiempoAcumulado = 0f; // Almacena el tiempo acumulado entre cada segundo.

    void Update()
    {
        // Acumula el tiempo que ha pasado en cada fotograma.
        tiempoAcumulado += Time.deltaTime;

        // Cuando haya pasado 1 segundo (o más, en caso de saltos de frame), incrementa el contador.
        if (tiempoAcumulado >= 1f)
        {
            segundosTranscurridos++; // Incrementa los segundos.
            tiempoAcumulado = 0f; // Reinicia el acumulador.

            // Actualiza el texto del contador.
            if (contadorTexto != null)
            {
                contadorTexto.text = "Tiempo: " + segundosTranscurridos + " segundos";
            }
        }
    }
}
