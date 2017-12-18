using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] public GameObject Playeranchor;
    [SerializeField] public GameObject cameraAnchor, cameraAnchorVertical;
    [SerializeField] public GameObject camera;
    [SerializeField] public float RotationSpeed = 100;
    [SerializeField] public float VerticalRotationSpeed = 45;
    [SerializeField] public float JumpHeight = 10f;
    [SerializeField] public float MoveSpeed = .5f;

    [SerializeField] public float gravityScale = 10f;
    [SerializeField] public float maxRotUp, maxRotDown;

	// Use this for initialization
	void Start () {

        gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {

        Quaternion targetRotationA = Quaternion.LookRotation(cameraAnchor.transform.position - cameraAnchorVertical.transform.position);
        float strA = Mathf.Min(10f * Time.deltaTime, 1);
        Playeranchor.transform.rotation = Quaternion.Lerp(Playeranchor.transform.rotation, targetRotationA, strA);

        Playeranchor.transform.rotation = transform.localRotation;
        /*if(cameraAnchor.transform.localRotation.z!=0)
        {
            cameraAnchor.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z/2);
        }*/
		if(Input.GetAxis("RightStickHorizontal")!=0)
        {
            cameraAnchor.transform.Rotate(Vector3.up * Time.deltaTime * RotationSpeed * Input.GetAxis("RightStickHorizontal"));
        }
        /*if(Input.GetAxis("RightStickVertical")!=0)
        {
            cameraAnchor.transform.Rotate(Vector3.right * Time.deltaTime * VerticalRotationSpeed * Input.GetAxis("RightStickVertical"));
        }*/

        /*if (Input.GetAxis("RightStickHorizontal") != 0)
        {
            cameraAnchor.transform.RotateAround(cameraAnchor.transform.position, Vector3.up,  Time.deltaTime * RotationSpeed * Input.GetAxis("RightStickHorizontal"));
        }*/
        /*if (Input.GetAxis("RightStickVertical") != 0)
        {
            cameraAnchor.transform.RotateAround(cameraAnchorVertical.transform.position, Vector3.forward, Time.deltaTime * VerticalRotationSpeed * Input.GetAxis("RightStickVertical"));
        }*/

        /*if (Input.GetAxis("RightStickHorizontal") != 0)
        {
            cameraAnchor.transform.eulerAngles += new Vector3(Time.deltaTime * RotationSpeed * Input.GetAxis("RightStickHorizontal"), cameraAnchor.transform.eulerAngles.y, cameraAnchor.transform.eulerAngles.z);
        }
        if (Input.GetAxis("RightStickVertical") != 0)
        {
            cameraAnchor.transform.eulerAngles += new Vector3(cameraAnchor.transform.eulerAngles.x, cameraAnchor.transform.eulerAngles.y, Time.deltaTime * RotationSpeed * Input.GetAxis("RightStickVertical"));
        }*/
        if(Input.GetButtonDown("Jump"))
        {
            Playeranchor.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
        }

        Vector3 gravity = -gravityScale * Vector3.up;
        Playeranchor.GetComponent<Rigidbody>().AddForce(gravity, ForceMode.Acceleration);


        //if (Input.GetAxis("LeftStickHorizontal") != 0)
        //{
        //    GetComponent<Rigidbody>().AddForce(Vector3.forward * Input.GetAxis("LeftStickHorizontal") * MoveSpeed, ForceMode.Impulse);
        //}
        //if (Input.GetAxis("LeftStickVertical") != 0)
        //{
        //    GetComponent<Rigidbody>().AddForce(Vector3.right * Input.GetAxis("LeftStickVertical") * MoveSpeed, ForceMode.Impulse);
        //}

        if (Input.GetAxis("LeftStickHorizontal") != 0)
        {
            Playeranchor.transform.Translate(Vector3.forward * Input.GetAxis("LeftStickHorizontal") * MoveSpeed);

            Quaternion targetRotation = Quaternion.LookRotation(cameraAnchorVertical.transform.position - transform.position);
            float str = Mathf.Min(10f * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
            // GetComponent<Rigidbody>().AddForce(Vector3.forward * Input.GetAxis("LeftStickHorizontal") * MoveSpeed, ForceMode.Impulse);
        }
        if (Input.GetAxis("LeftStickVertical") != 0)
        {
            Playeranchor.transform.Translate(Vector3.right * Input.GetAxis("LeftStickVertical") * MoveSpeed);

            Quaternion targetRotation = Quaternion.LookRotation(cameraAnchorVertical.transform.position - transform.position);
            float str = Mathf.Min(10f * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
            //GetComponent<Rigidbody>().AddForce(Vector3.right * Input.GetAxis("LeftStickVertical") * MoveSpeed, ForceMode.Impulse);
        }
    }
}
