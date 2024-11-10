using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Configuraci�n de movimiento")]
    public bool carriles = false;
    public bool autoPilot = false;
    // Este arreglo es para guardar las posiciones en donde se mueve el jugador cuando carriles esta activado.
    [HideInInspector] public float[] posCarriles;
    [SerializeField] private int cantCarriles = 3;
    [SerializeField] private float movementDistance = 6.0f;

    public float playerPosition;
    [SerializeField] private float limitY = -3.5f;
    [SerializeField] private float limitX = 8.10f;

    [HideInInspector] public float speed = 8;


    [SerializeField] private bool puedeVolar = false;

    [Header("Configuraci�n de vida")]
    [HideInInspector] public int life = 3;
    [HideInInspector] public bool inmunity = false;


    [Header("Configuraci�n de municiones")]
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private int bulletType = 0;
    [SerializeField] private float fireRate = 0.5F;
    [SerializeField] private bool canShoot = true;

    [Header("Configuraci�n generales")]
    [SerializeField] private Configuracion_General config;


    // Start is called before the first frame update
    void Start()
    {
        life = config.vidas;
        speed = config.velocidad;
        if (carriles)
        {
            if (cantCarriles == 2)
            {
                posCarriles = new float[3] { -movementDistance, 0, movementDistance };
            } else if (cantCarriles == 3)
            {
                posCarriles = new float[2] { -movementDistance, movementDistance };
                //transform.Translate(this.transform.position.x + movementDistance, transform.position.y, 0);
            } else
            {
                Debug.Log("Estas intentando usar" + cantCarriles + ". El permitido es tres o dos. Para otra config hay que programarlo");
            }


        }

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
    }

    private void Movement()
    {
        if (carriles)
        {
            float playerPosition = transform.position.x;

            if (Input.GetKeyDown(KeyCode.D))
            {
                print("ok");
                if (playerPosition < posCarriles[1])
                {
                    transform.Translate(movementDistance, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (playerPosition > posCarriles[0])
                {
                    transform.Translate(-movementDistance, 0, 0);
                }

            }
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);

            if (transform.position.x > limitX)
            {
                transform.position = new Vector3(limitX, transform.position.y);
            }
            else if (transform.position.x < -limitX)
            {
                transform.position = new Vector3(-limitX, transform.position.y);
            }

            if (puedeVolar)
            {
                float verticalInput = Input.GetAxis("Vertical");
                transform.Translate(Vector2.up * speed * verticalInput * Time.deltaTime);

                if (transform.position.y > 0)
                {
                    transform.position = new Vector2(transform.position.x, 0);
                }
                else if (transform.position.y < limitY)
                {
                    transform.position = new Vector2(transform.position.x, limitY);
                }
            }


        }

        if (autoPilot)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            Debug.Log(speed);
        }
    }
    void OnTriggerEnter(Collider obj){
        
        Debug.Log("choqué con algo y su tag es: "+ obj.gameObject.tag);

    }
    public void Damage(int _dmg)
    {
        if (inmunity == false)
        {
            if (life > 0)
            {
                life -= _dmg;
                if (life <= 0)
                {
                    config.perdiste = true;
                    Destroy(this.gameObject);
                }
            }
            else if (life <= 0)
            {
                config.perdiste = true;
                Destroy(this.gameObject);
            }
        } else
        {
            inmunity = false;
        }
        //Actualizamos la variable de vida de la configuracion general.
        config.vidas = life;

    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canShoot)
        {
            StartCoroutine(ShootDelay());
            if (Configuracion_General.runner3D == false)
            {
                Instantiate(bullets[bulletType], new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            } else
            {
                Instantiate(bullets[bulletType], new Vector3(transform.position.x, -1f, transform.position.z), Quaternion.identity);
            }

        }
    }

    public IEnumerator ShootDelay()
    {
        if (Configuracion_General.runner3D == false)
        {
            Instantiate(bullets[bulletType], new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(bullets[bulletType], new Vector3(transform.position.x, -1f, transform.position.z), Quaternion.identity);
        }
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public void moveOSC (float _x) {
        transform.Translate(Vector3.right * speed * _x * Time.deltaTime);
        if (transform.position.x > limitX)
        {
            transform.position = new Vector3(limitX, transform.position.y);
        }
        else if (transform.position.x < -limitX)
        {
            transform.position = new Vector3(-limitX, transform.position.y);
        }
    }
}
