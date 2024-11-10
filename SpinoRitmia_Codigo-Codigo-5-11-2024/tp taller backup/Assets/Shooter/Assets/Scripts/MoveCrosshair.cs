/*
    ESTE SCRIPT LO UTILIZAMOS PARA QUE LA MIRA SIGA LA POSICIÓN DEL MOUSE 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrosshair : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Para que no se vea el cursor de windows
        Cursor.visible = false; 
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = this.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        var crosshair = GameObject.FindGameObjectWithTag ("Player");

        crosshair.transform.position = new Vector3(pos.x, pos.y, -9);

        
		
	}
}
