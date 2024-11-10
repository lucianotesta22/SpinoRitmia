using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMeter : MonoBehaviour {

    //El rockMeter lo vamos utilizar para determinar cuando pierde el jugador. Si llega a un valor menor de 25 pierde el juego (Esto lo controlamos desde el manager).

    //Creamos una variable para guardar el valor del Rock Meter.
    float rm;
    //Creamos una variable para llamar a la aguja
    GameObject aguja;
    //Posicion x aguja
    float xAguja = 3;

    void Start () {
        //Ubicamos a nuestro gameObject Aguja
        aguja = GameObject.Find("Aguja");
    }
	
	void Update () {
        rm = PlayerPrefs.GetInt("RockMeter");

        //Cambiamos la posicion de la aguja dependiendo del valor del RockMeter
        xAguja = ((rm - 60) / 16.6666f) + 3; //Le sumamos 3 porque es la posicion central del meter.
        aguja.transform.localPosition = new Vector3( xAguja, 3, 1 );
    }
}
