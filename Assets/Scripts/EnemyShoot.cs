using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] public float LowerBoundsShootInterval, UpperBoundsShootInterval;
    [SerializeField] public GameObject Bullet;
	// Use this for initialization
	void Start () {
        StartCoroutine(Shooty());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Shooty()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(LowerBoundsShootInterval, UpperBoundsShootInterval));
            GameObject bullet = Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            //bullet.transform.localScale = new Vector3(1, .35f, 1);
        }
    }
}
