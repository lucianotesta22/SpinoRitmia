using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private bool isDead = false; //Variable booleana que utilizamos para saber si "murió" o no el Player

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isDead) //Si está muerto, no dejamos que siga la ejecución del juego
            return;
    }

    /*
 ESTA FUNCIÓN LA UTILIZAMOS PARA "MATAR" AL PLAYER
*/
    public void Death()
    {
        isDead = true;
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<Score>().OnDeath(); //Cortamos el aumento de puntaje

    }
}
