using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickp : MonoBehaviour
{

    public int value;

    public int soundToPlay;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.AddCoins(value);
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}
