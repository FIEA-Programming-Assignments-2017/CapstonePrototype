using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasso : MonoBehaviour
{
    [SerializeField] public GameObject Reticle;

    [SerializeField] public GameObject ActiveReticle;

    [SerializeField] public CameraControl Player;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (ActiveReticle != null)
        {
            if(Input.GetButtonDown("Lasso"))
            {
                Player.TravelTo(ActiveReticle);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Grabbable"))
        {
            ActiveReticle = Instantiate(Reticle, other.transform.position, Quaternion.identity, other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            Destroy(ActiveReticle);
        }
    }
}
