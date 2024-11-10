using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Necesitamos esto para poder trabajar con elementos UI

//Esta funcion la vamos a utilizar mostrar los valores que tenemos guardados en su UI.
public class PPText : MonoBehaviour {

    //Nombre publico del texto del UI
    public string nombre;

    // Update is called once per frame
    void Update () {
        GetComponent<Text>().text = PlayerPrefs.GetInt(name) + "";		
	}

    
}
