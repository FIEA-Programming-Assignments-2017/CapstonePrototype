using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField] [Range(0, 200)] public float BulletSpeed = 0;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * BulletSpeed);
        if (Time.time - spawnTime > bulletLife)
        {
            Destroy(this.gameObject);
        }
    }

    private GameObject Player;

    private float spawnTime;
    private float bulletLife = 3f;
}
