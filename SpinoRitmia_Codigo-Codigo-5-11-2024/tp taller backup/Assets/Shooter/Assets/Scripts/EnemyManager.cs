using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public Vector3 initialPos = new Vector3(0, 0, 4); //Posición incial del enemigo
    public float XForce = 0.0f; //La fuerza que se aplica en X (de movimiento)
    public float YForce = 0.0f; //La fuerza que se aplica en Y (de movimiento)
    public float timeImpulse = 2.0f; //El tiempo que dura el impulso (las fuerzas de arriba)
    float time = 0.0f; // Variable que utilizamos para corroborar que se cumpla el tiempo de impulso.
    public bool isStatic = false; //Booleana que indicamos si el enemigo es estático o se mueve.
    public bool isRandom = false; //Booleana que indicamos si el enemigo se mueve de manera random
    public int score = 0; //Puntaje que da el enemigo

    
    // Use this for initialization
    void Start () {
        if (isRandom) //Si definimos que tenga una posición random, entonces...
        {
            initialPos = new Vector3(Random.Range(-9.65f, 9.78f), Random.Range(-2.69f, 0.0f)); //Posición inicial, la hacemos random
        }
        
        this.gameObject.transform.position = initialPos; //Le asignamos al objeto (enemigo) esa posición (puede ser la random o no).
    }
	
	// Update is called once per frame
	void Update () {

        time = time + Time.deltaTime;
        if (time <= timeImpulse ) //Calcula el tiempo de impulso del enemigo
        {
            if (!isStatic) //Si no es estático
            {
                moveEnemy(); //Lo mueve
            }
                
        }
    }

    /*
     FUNCIÓN QUE UTILIZAMOS PARA EL MOVIMIENTO DEL ENEMIGO.
    */
    void moveEnemy ()
    {
        if (!isStatic)
        {
            if (isRandom)
            {
                
                if (initialPos.x <= 0)
                {
                    XForce = Random.Range(1, 4);
                    
                }else if (initialPos.x > 0)
                {
                    XForce = Random.Range(-1, -4);
                }
                YForce = Random.Range(3, 7);
                isRandom = false;
            }
            //Una vez que calculamos las fuerzas en X e Y de movimiento, le aplicamos esa "fuerza" (velocidad) al enemigo (a su Rigidbody)
            this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(XForce, YForce); 

        }
    }

    /*
        FUNCIÓN QUE UTILIZAMOS PARA DESTRUÍR AL ENEMIGO (CUANDO LO CLICKEAMOS CON EL CLICK IZQUIERDO DEL MOUSE 
    */
    void destroyEnemy ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Score>().GetScore(score); //Llama a la función "GetScore" del script Score (para sumar el puntaje)
            Destroy(gameObject); //Destruye el gameObject enemigo.
        }
    }

    //Cuando el mouse está sobre el enemigo, ejecutamos la función de arriba (destroyEnemy)
    void OnMouseOver()
    {
        destroyEnemy();
    }
}
