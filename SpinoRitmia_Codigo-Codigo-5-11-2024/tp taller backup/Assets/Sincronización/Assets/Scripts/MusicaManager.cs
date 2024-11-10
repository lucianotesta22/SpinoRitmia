using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaManager : MonoBehaviour {

    //Esta booleana la vamos a usar para asegurarnos que se llame una sola vez
    bool called = false;

    void Update(){
        //Esto lo hacemos para asegurarnos que la musica empiece junto con la caida de las notas.
        //Como necesitamos que la musica se llame una sola vez agragamos esta variable "called", que deja de ser falsa luego de hacer arrancar la musica.
        if (PlayerPrefs.GetInt("Start") == 1 && !called)
        {
            GetComponent<AudioSource>().Play();
            called = true;
        }
    }
}
