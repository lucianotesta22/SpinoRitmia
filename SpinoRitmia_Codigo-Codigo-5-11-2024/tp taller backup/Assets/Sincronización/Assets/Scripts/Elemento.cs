using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elemento : MonoBehaviour
{
    [SerializeField] public string nombre;
    public KeyCode tecla;
    public GameObject secuenciaManager;

    [SerializeField] private bool apretada;
    [SerializeField] SpriteRenderer sr;
    private Color viejo;

    void Awake(){
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        apretada = false;
        viejo = sr.color;
        
    }


    void Update()
    {
         if ((Input.GetKeyDown(tecla)) && (apretada==false))
            {
                //https://docs.unity3d.com/es/current/Manual/Coroutines.html
                StartCoroutine(Apretado());
            }


    }
        IEnumerator Apretado()
    {
        sr.color = new Color(0,255,0);
       
        nombre=tecla.ToString();               
        secuenciaManager.GetComponent<SecuenciaPasosManager>().recibirTecla(nombre);
        yield return new WaitForSeconds(0.05f);
        sr.color = viejo;      
        apretada=true;   
        
    }

}
