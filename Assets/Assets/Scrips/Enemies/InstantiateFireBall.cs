using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFireBall : MonoBehaviour
{
    public GameObject spawnerBullet;

    public GameObject bullet;
    public void creatBullet()
    {
        Instantiate(bullet, spawnerBullet.transform.position, spawnerBullet.transform.rotation);
    }
}
