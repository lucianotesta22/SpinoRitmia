

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public GameObject[] enemiesPrefabs;

    public int amountOfEnemiesOnScreen = 5;

    private List<GameObject> activeEnemies; //Utilizamos esta lista para guardar los Enemigos que están en uso (y así borrar los que no y mejorar el rendimiento)

    private int lastPrefabIndex = 0; // La usamos para que no spawneen de manera consecutiva los Enemigos random

    public float time = 0.0f;
    private float timeDestroy = 0.0f;

    // Use this for initialization
    void Start () {

        activeEnemies = new List<GameObject>(); //Inicializamos la lista.
    }
	
	// Update is called once per frame
	void Update () {
        time = time + Time.deltaTime;
        timeDestroy = timeDestroy + Time.deltaTime;

        if (time >= 3.0f)
        {
            SpawnEnemie();
            time = 0.0f;
        }

        if (timeDestroy >= 7.0f)
        {
            DeleteEnemie();
            timeDestroy = 0.0f;
        }
        
    }

    /*
     FUNCIÓN QUE UTILIZAMOS PARA GENERAR LOS ENEMIGOS
    */
    private void SpawnEnemie(int prefabIndex = -1)
    {

        GameObject going;
        if (prefabIndex == -1)
            going = Instantiate(enemiesPrefabs[RandomPrefabIndex()]) as GameObject;
        else
            going = Instantiate(enemiesPrefabs[prefabIndex]) as GameObject;

        going.transform.SetParent(transform);
        going.transform.position = enemiesPrefabs[1].transform.position;
        activeEnemies.Add(going);
    }


    private int RandomPrefabIndex()
    {
        if (enemiesPrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, enemiesPrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

    private void DeleteEnemie()
    {
        Destroy(activeEnemies[0]);
        activeEnemies.RemoveAt(0);
    }
}
