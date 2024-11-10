using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasoActualTexto : MonoBehaviour
{
    public TMP_Text PasoActualText; 
    public SecuenciaPasosManager sm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PasoActualText.text = "Paso" + sm.pasoActual.ToString() + "/" + sm.pasoFinal.ToString();
    }
}
