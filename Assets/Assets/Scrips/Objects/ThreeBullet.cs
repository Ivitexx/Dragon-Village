using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeBullet : MonoBehaviour
{
    public GameObject spawnerBullet;
    public GameObject spawnerBullet1;
    public GameObject spawnerBullet2;

    public GameObject bullet;
    public void creatBullet()
    {
        Instantiate(bullet, spawnerBullet.transform.position, spawnerBullet.transform.rotation);
        Instantiate(bullet, spawnerBullet1.transform.position, spawnerBullet1.transform.rotation);
        Instantiate(bullet, spawnerBullet2.transform.position, spawnerBullet2.transform.rotation);
    }
}
