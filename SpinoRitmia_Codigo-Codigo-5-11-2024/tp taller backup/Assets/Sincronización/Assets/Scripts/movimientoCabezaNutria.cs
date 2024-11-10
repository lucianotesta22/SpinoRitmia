using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoCabezaNutria : MonoBehaviour
{
    private float numero=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numero=transform.position.z;

        if(numero <= 0.85f) {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        else if(numero <= 6f) {
            transform.Translate(Vector3.back * Time.deltaTime);
        }
        
    }
}
