using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooty : MonoBehaviour
{
    [SerializeField] public GameObject Bullet;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButtonDown("Shooty"))
        {
            print(Random.Range(0f, 1f) * 10);
            GameObject newBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            newBullet.transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        }
	}
}
