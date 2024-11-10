using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement; //Agregamos esto para poder manejar las escenas

public class GameManager : MonoBehaviour {

    //Esta variable la vamos a utilizar para multiplicar el puntanje dependiendo de cuantos golpes acertados llevamos.
    int multiplicador = 1;
    //Esta variable la vamos a utilizar como contador de golpes consecutivos de las notas.
    int golpes = 0;
    //Puntaje por nota
    int valorNota = 100;
    //CreateMode: este modo lo usamos para crear las notas del juego. Al estar activo creamos notas en vez de eliminarlas.
    public bool createMode;
    private Configuracion_General.TiposDeDificultad dif;

    public Nota nota;

    public float tiempo = 0;
    public bool isJuego = true;
    public float Timer;

    public Timer tiempos;
    

    //GameObject nota;
	void Start () {
        nota.fuego = false;
        EventManager.OnTimerStart();
        //Reseteamos el puntaje y otras cosillas al comienzo del juego
        PlayerPrefs.SetInt("Puntaje", 0);
        PlayerPrefs.SetInt("RockMeter", 60);
        PlayerPrefs.SetInt("Golpes", 0);
        PlayerPrefs.SetInt("RecordGolpes", 0);
        PlayerPrefs.SetInt("Multiplicador", 1);
        PlayerPrefs.SetInt("NotasHit", 0);
        //Este valor vamos usarlo para asegurarnos que la musica empiece al mismo tiempo que las notas caen.
        PlayerPrefs.SetInt("Start", 1);
    }

    //Esta funcion se encarga de destruir cualquier nota que el jugador perdio y resetar los golpes.
    void OnTriggerEnter2D(Collider2D coll)
    {
        //Solo eliminamos las notas si no esta en el modo creacion.
        if (!createMode)
        {
            ResetearGolpes();
            //Destroy(coll.gameObject);
            //transform.Translate(0,transform.position.y + 50, 0);
        }

    }


    public void AgregarGolpe()
    {
        //Si el valor de RockMeter es menor a 50 le sumamos 1
        if (PlayerPrefs.GetInt("RockMeter") < 120)
        {
            if(tiempos.escena == 9 || tiempos.escena == 14){
              if(nota.fuego == false){
                Debug.Log("Tiene Fuego");
              PlayerPrefs.SetInt("RockMeter", PlayerPrefs.GetInt("RockMeter") - 20 );
              }
              else if (nota.fuego == true){
                PlayerPrefs.SetInt("RockMeter", PlayerPrefs.GetInt("RockMeter") + 2 );
              }
            }
            else{
                PlayerPrefs.SetInt("RockMeter", PlayerPrefs.GetInt("RockMeter") + 2 );
            }
        }
        

        //Cada vez que entra a esta funcion le suma 1 al valor de golpes. 
        golpes++; //Esto es igual a poner golpes = golpes +1;

        //Aca vereficamos cual es el valor de golpes y en el caso de ser multiple de 8 aumentamos el valor del multiplicador.
        if (golpes >=24) {
            multiplicador = 4; //Si golpes es igual o mayor a 24 queremos que multiplicador sea igual a 4
        }else if (golpes >= 16){
            multiplicador = 3;
        }else if (golpes >= 8){
            multiplicador = 2;
        }else{
            multiplicador = 1; //Si no se cumple ninguna de las condiciones anteriores queremos que multiplicador sea igual a 1.
        }

        //Aca guardamos la mayor cantidad de golpes acertados, si es mayor al que ya tenemos guardado
        if ( golpes > PlayerPrefs.GetInt("RecordGolpes"))
        {
            PlayerPrefs.SetInt("RecordGolpes", golpes);
        }

        //Cada vez que acertamos una nota lo guardamos en el contador de notas acertadas para mostrarlo al final del juego.
        PlayerPrefs.SetInt("NotasHit", PlayerPrefs.GetInt("NotasHit") + 1);
 

        //Actualizamos el hud ya que cambiaron los valores.
        ActualizarHUD();
    }

    //Cuando fallamos una nota reseteamos a 0 el contador de golpes y el multiplicador de puntos.
    public void ResetearGolpes()
    {
        
        //Cada vez que erramos una nota le restamos 2 al rockMeter
        switch (dif)
        {   
                case Configuracion_General.TiposDeDificultad.facil:
                    PlayerPrefs.SetInt("RockMeter", PlayerPrefs.GetInt("RockMeter") - 2);
                    break;
                case Configuracion_General.TiposDeDificultad.intermedio:
                    PlayerPrefs.SetInt("RockMeter", PlayerPrefs.GetInt("RockMeter") - 2);
                    break; 
                case Configuracion_General.TiposDeDificultad.dificil:
                    PlayerPrefs.SetInt("RockMeter", PlayerPrefs.GetInt("RockMeter") - 2);
                    break;          
             
        }
       
        //Si el valor del RockMeter baja a 0 perdemos el juego.
        if (PlayerPrefs.GetInt("RockMeter") <0)
        {
            Perder();
        }
        golpes = 0;
        multiplicador = 1;

        //Actualizamos el hud ya que cambiaron los valores.
        ActualizarHUD();
    }

    //Actualizamos los valores de golpes y multiplicadores en el HUD.
    void ActualizarHUD()
    {
        PlayerPrefs.SetInt("Golpes" , golpes);
        PlayerPrefs.SetInt("Multiplicador", multiplicador);
    }

        public int GetScore()
    {
        //Multiplicamos el valor de la nota por el multiplicador
        return valorNota*multiplicador;
    }

    void Perder()
    {
        //El juego esta detenido
        PlayerPrefs.SetInt("Start", 0);
        //Perdiste el juego, cargamos la pantalla de perder.
        SceneManager.LoadScene("Perder");
        Debug.Log("PERDISTE D:"); //Dejamos un msj en la consola
    }

    //Se gana el juego cuando se llega al final de la cancion. Para determinar esto vamos a crear una nota invisible que se llama NotaGanadora (Buscar en el historial de objetos, tiene alfa 0).
    //Esta la vamos a colocar al final de nuestra cancion, asi al colicionar con uno de los activadores sabremos que el jugador llego al final de la cancion.
    public void Ganar()
    {
        //El juego esta detenido
        PlayerPrefs.SetInt("Start", 0);
        //Guardamos mejor puntaje si es mayor al que ya tenemos guardado
        if (PlayerPrefs.GetInt("MejorPuntaje") < PlayerPrefs.GetInt("Puntaje"))
        {
            PlayerPrefs.SetInt("MejorPuntaje", PlayerPrefs.GetInt("Puntaje"));
        }
        //Ganaste el juego, cargamos la pantalla de ganar
        SceneManager.LoadScene("Ganar");
        Debug.Log("GANASTEEEEE :D");
        Debug.Log( "Notas Hit " + PlayerPrefs.GetInt("NotasHit"));
        Debug.Log( "Record golpe "  + PlayerPrefs.GetInt("RecordGolpes"));
        Debug.Log( "Puntaje " + PlayerPrefs.GetInt("Puntaje"));
        Debug.Log("Mejor Puntaje " + PlayerPrefs.GetInt("MejorPuntaje"));
    }

    public bool GetCreateMode()
    {
        return createMode;
    }
}
