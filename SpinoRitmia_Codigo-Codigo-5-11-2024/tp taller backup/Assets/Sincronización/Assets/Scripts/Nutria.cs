using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutria : MonoBehaviour
{
[SerializeField] SpriteRenderer sr;

 public Configuracion_General config;
 Rigidbody2D rb;
 //Esta variable nos va a indicar la velocidad con la que se va a mover en el eje y, osea que tan rapido va a "aparecer y desaparecer".
    //Hacemos esta variable publica para poder setearla desde el inspector de la nutria
    public float velocidad;
    //Esta booleana la vamos a usar para asegurarnos que se llama una sola vez
    bool called = false;

    public float tiempoParaAcertar;

    void Awake(){
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        velocidad = config.velocidad;
        tiempoParaAcertar=2f;
    }

    void Update()
    {
        if ( PlayerPrefs.GetInt("Start") == 1 && !called)
        {
            rb.linearVelocity = new Vector2(0, -velocidad);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag == "ActivadorNutria"){     
        this.sr.enabled =true;                              //Muestra a la nutria
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        
        rb.linearVelocity = new Vector2(0, -velocidad);
        StartCoroutine(Espera(col));
       }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "ActivadorNutria"){
            this.sr.enabled =false;
       }
    }

    IEnumerator Espera(Collider2D c){
        yield return new WaitForSeconds(tiempoParaAcertar); // Espera este tiempo
        Destroy(this.gameObject);
    }
}
