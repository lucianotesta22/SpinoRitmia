using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorNutria : MonoBehaviour {

    SpriteRenderer sr;
    //Variable que usamos para determinar a que Letra responde.
    public KeyCode tecla;
    //Booleana que nos dice si esta pasando una nutria por el collider del activador.
    bool activo = false;
    //Llamamos a las nutrias
    GameObject nutria;
    //Creamos una variable gameObject para poder llamar a gameManager
    GameObject gm;
    //Guardamos el color viejo, para cambiarlo al hacer click
    Color viejo;

    //La nutria que vamos a crear (en el modo creador).
    public GameObject n;


    //Es lo primero en inicializarse, aunque el script este desactivado
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    //Se inicializa luego de awake, solo si el script esta activado
    void Start () {
        //Llamamos a nuestro Game Manager, para poder usar sus funciones
        gm = GameObject.Find("GameManager");
        //Asignamos el color viejo, el asignado al comienzo del juego.
        viejo = sr.color;
	}
	
	// Update is called once per frame
	void Update () {


        //Si el createMode esta activado vamos a crear nutrias al presionar los activadores.
        //Para que esto funcione tenemos que ir al GameManager y poner en verdadero la booleana createMode.
        //Ademas debemos indicar que la variable n sea igual a nutria (buscar en los assets), esto debemos cambiarlo en los objetos activadores.
        if (gm.GetComponent<GameManager>().GetCreateMode() )
        {
            //Creamos nutrias al hacer click en el activador
             if (Input.GetKeyDown(tecla))
            {
                Instantiate(n, transform.position, Quaternion.identity);
            }   

        }
        else
        {
            if (Input.GetKeyDown(tecla))
            {
                //https://docs.unity3d.com/es/current/Manual/Coroutines.html
                StartCoroutine(Apretado());
            }

            //Si apretamos la tecla del activador y activo es verdadero ( ya que esta pasando una nutria )
            if (Input.GetKeyDown(tecla) && activo)
            {
                Destroy(nutria); //Destruimos esa nutria
                gm.GetComponent<GameManager>().AgregarGolpe(); //Agregamos un golpe. Esta funcion existe en el gameManager
                AgregarPuntaje();//Agregamos puntaje
                activo = false;
            }else if (Input.GetKeyDown(tecla) && !activo)
            {
                gm.GetComponent<GameManager>().ResetearGolpes(); // Si activo es falso, ya que no esta pasando ninguna nutria, reseteamos los golpes.
            }
        }

        
    }

    //Enviado cuando otro objeto entra en un trigger collider adjunto a este objeto. https://github.com/glantucan/puzzle_game/wiki/Detecci%C3%B3n-de-colisiones-y-triggers
    void OnTriggerEnter2D(Collider2D col)
    {
        //Controlamos que no estemos en Create mode cuando escuchamos a la nutriaGanadora.
        //Si cambian los patrones de nutrias, deben colocar esta nutria al final.
        if (col.gameObject.tag == "NutriaGanadora" && !gm.GetComponent<GameManager>().GetCreateMode() )
        {
            //Si la nutria ganadora pasa el colider de los activadores significa que llegamos al final de la cancion
            gm.GetComponent<GameManager>().Ganar();
        }

        if ( col.gameObject.tag == "Nutria")
        {
            activo = true;
            nutria = col.gameObject;
            sr.color = new Color(0,255,0);
        }
    }

    //Enviado cuando otro objeto deja un trigger collider adjunto a este objeto.
    void OnTriggerExit2D(Collider2D col)
    {
         activo = false;
         sr.color = viejo;
    }

    //Esta funcion la utilizamos para cambiar el color de los activadores al presionarlos.
    IEnumerator Apretado()
    {
        sr.color = new Color(0,0,0);
        yield return new WaitForSeconds(0.05f);
        sr.color = viejo;
    }

    //Agregamos puntaje al tocar una nutria
    void AgregarPuntaje()
    {
        PlayerPrefs.SetInt("Puntaje", PlayerPrefs.GetInt("Puntaje") + gm.GetComponent<GameManager>().GetScore());
    }

}

