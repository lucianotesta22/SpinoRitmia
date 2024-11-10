using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BF : MonoBehaviour {

    //Esta funcion la usamos para determinar que escena vamos a llamar con el boton seleccionado. 
    //Recuerden que para que esto funcione las funciones deben estar agregadas en  File --> Builds Settings. En este lugar van a poder ver la numeracion de las escenas.
    //Es publica para que desde el boton determinen que escena se llama.
	public void LoadScene(int a)
    {
        SceneManager.LoadScene(a);
    }

    //Esta funcion la usamos para salir del juego.
    public void Quit(){
        Application.Quit();
    }
}
