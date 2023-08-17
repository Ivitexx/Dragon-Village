using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListEnemyBoss : MonoBehaviour
{
    public int enemyWave;

    public EnemyHealthManager enemy;

    void Awake()
    {
        switch (enemyWave)
        {
            case 1:

                BossManager.instance.enemisWave1.Add(this.gameObject);

                break;
            case 2:

                BossManager.instance.enemisWave2.Add(this.gameObject);

                break;
            case 3:

                BossManager.instance.enemisWave3.Add(this.gameObject);

                break;
        }
    }

    void Update()
    {
        if(enemy.currentHealth == 0)
        {
            switch (enemyWave)
            {
                case 1:

                    BossManager.instance.enemisWave1.Remove(this.gameObject);

                    break;
                case 2:

                    BossManager.instance.enemisWave2.Remove(this.gameObject);

                    break;
                case 3:

                    BossManager.instance.enemisWave3.Remove(this.gameObject);

                    break;
            }
        }
    }
}
