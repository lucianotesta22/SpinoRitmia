/*
    ESTE SCRIPT LO UTILIZAMOS PARA MANEJAR EL PUNTAJE EN EL VIDEOJUEGO
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Agregamos esto para manejar las propiedades UI (Canvas, Text, Image, etc).

public class Score : MonoBehaviour {

    private float score = 0.0f; //Variable que contendrá el puntaje

    private bool isDead = false;

    public Text scoreText; //Variable del texto que se visualizará en pantalla en el videojuego. La definimos como pública para poder arrastrar el objeto Text desde el Inspector

    public DeathMenuManager deathMenu;

    [SerializeField] private Configuracion_General config;

    private void Awake()
    {
        GameObject gm = GameObject.FindWithTag("GameController");

        if (gm != null)
        {
            config = gm.GetComponent<Configuracion_General>();

        }

    }
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Puntaje: " + ((float)config.puntos).ToString(); //Convertimos la variable Score en String (texto - cadena de caracteres)
        score= config.puntos;
    }

    public void OnDeath()
    {
        isDead = true;
        /*
        ACLARACIÓN: PARA QUE ESTO FUNCIONE TIENEN QUE COMPILAR EL JUEGO, EJECUTARLO Y JUNTAR ALGO DE PUNTAJE.
        LUEGO DE ESO, SALEN Y APRETAN LA TECLA WIN+R (ESCRIBEN REGEDIT) Y BUSCAN:
        EN HKEY_CURRENT_USER > SOFTWARE > TDMM1 > BaseShooter (EN ESTE CASO SE LLAMA ASÍ)
        */
        if (PlayerPrefs.GetFloat("Highscore") < config.puntos) //Si el puntaje actual es mayor que el guardado, guardamos el nuevo puntaje
            PlayerPrefs.SetFloat("Highscore", config.puntos);
        Cursor.visible = true; //Ponemos el cursor visible de nuevo para que el usuario pueda seleccionar qué hacer en el menú
        deathMenu.ToggleEndMenu(score);
    }

    //Se utiliza para aumentar el score
    public void GetScore(float _score)
    {
        score = _score;
        config.puntos += score;
    }
}
