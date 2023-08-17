using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    float time;

    public LayerMask piso;

    public float speed;

    public GameObject dangerZone;
    public GameObject dangerZoneDelete;

    public BoxCollider gridArea;

    public GameObject explosion;

    private void Awake()
    {
        gridArea = GameObject.Find("MeteoriteArea").GetComponent<BoxCollider>();
    }
    void Start()
    {
        RandomizePosition();

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, piso))
        {   
            if(hit.collider.tag == "meteorite")
            {
                Destroy(gameObject);
            }
            else
            {
                dangerZoneDelete = Instantiate(dangerZone, hit.point, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        time += Time.deltaTime;

        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(time > 6.3)
        {
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(dangerZoneDelete);
        }

        if(HealthManager.instance.currentHealth == 0f)
        {
            Destroy(gameObject);
            Destroy(dangerZoneDelete);
        }
    }

    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        this.transform.position = new Vector3(x, gridArea.transform.position.y, z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            PlayerController.instance.isDefend = false;
            HealthManager.instance.Hurt();
        }
    }
}
