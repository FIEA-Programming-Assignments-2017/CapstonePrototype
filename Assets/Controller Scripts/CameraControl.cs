using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] public GameObject PlayerAnchor;
    [SerializeField] public GameObject InputAnchor;
    [SerializeField] public GameObject Player;
    [SerializeField] public float MoveSpeed;
    [SerializeField] public float JumpHeight = 10f;
    [SerializeField] public float GravityScale = -15f;

    [SerializeField] public GameObject Camera;
    [SerializeField] public GameObject CameraRotationCore, CameraRotationPosition, CameraHeightCore,CameraHeightPosition;

    [SerializeField] [Range(0,1)] public float CameraHeight;
    [SerializeField] public float CameraDistance;
    [SerializeField] [Range(0,1)] public float CameraRotatedPosition;
    [SerializeField] public float CameraRotationSpeed;

    [SerializeField] public GameObject CameraLookAnchor;
    [SerializeField] public GameObject CameraPositionAnchor;

	void Start ()
    {
        m_CameraRotationValue = 0;
        m_IsTraveling = false;
	}
	
	void Update ()
    {

        m_IsMoving = false;

        if (Input.GetAxis("RightStickHorizontal") != 0)
        {
            CameraRotationCore.transform.Rotate(Vector3.up * Time.deltaTime * CameraRotationSpeed * Input.GetAxis("RightStickHorizontal"));
        }
        if (Input.GetAxis("RightStickVertical") != 0 )
        {
            if((Input.GetAxis("RightStickVertical")>0 && CameraHeightCore.transform.localRotation.x<.2f) || (Input.GetAxis("RightStickVertical") < 0 && CameraHeightCore.transform.localRotation.x > -.6f))
            CameraHeightCore.transform.Rotate(Vector3.right * Time.deltaTime * CameraRotationSpeed * -Input.GetAxis("RightStickVertical"));
        }

        if(Input.GetAxis("LeftStickVertical") != 0)
        {
            PlayerAnchor.transform.Translate(InputAnchor.transform.forward * Input.GetAxis("LeftStickVertical") * MoveSpeed);
            m_IsMoving = true;
        }
        if (Input.GetAxis("LeftStickHorizontal") != 0)
        {
            PlayerAnchor.transform.Translate(-InputAnchor.transform.right * Input.GetAxis("LeftStickHorizontal") * MoveSpeed);
            m_IsMoving = true;
        }

        if(m_IsTraveling)
        {
            float moveStep = m_GravConst * Time.deltaTime;
            Transform rootTrans = m_TravelTarget.transform.root;
            float grabObjectMass = rootTrans.GetComponent<GrabableMass>().ObjectMass;

            float distance = Vector3.Distance(rootTrans.position, PlayerAnchor.transform.position);

            float tempStep = grabObjectMass / (m_CharMass + grabObjectMass);
            tempStep = tempStep < m_StepEPS ? 0 : tempStep;
            float stepDistancePlayer = moveStep * tempStep / (distance * distance);

            tempStep = m_CharMass / (m_CharMass + grabObjectMass);
            tempStep = tempStep < m_StepEPS ? 0 : tempStep;
            float stepDistanceObject = moveStep * tempStep / (distance * distance);
            Debug.Log("Distance: " + distance);
            Debug.Log("Object step: " + stepDistanceObject);
            Debug.Log("Player step: " + stepDistancePlayer);

            PlayerAnchor.transform.position = Vector3.MoveTowards(PlayerAnchor.transform.position, rootTrans.position, stepDistancePlayer);
            rootTrans.position = Vector3.MoveTowards(rootTrans.position, PlayerAnchor.transform.position, stepDistanceObject);

            if(Vector3.Distance(PlayerAnchor.transform.position, m_TravelTarget.transform.position)<10f)
            {
                m_IsTraveling = false;
                PlayerAnchor.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                PlayerAnchor.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpHeight/2, ForceMode.Impulse);
                GravityScale = m_TempGravHolder;
            }
            else
            {
                return;
            }
        }
        /*
        Vector3 DirectionVector = new Vector3(Input.GetAxis("LeftStickVertical"), 0, Input.GetAxis("LeftStickHorizontal"));
        float step = CameraRotationSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(Player.transform.forward, DirectionVector, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        Player.transform.rotation = Quaternion.LookRotation(newDir);
        */

        Vector3 cameraForward = Vector3.Scale(Camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(-Camera.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 moveTranslation = Input.GetAxis("LeftStickVertical") * cameraForward;
        Vector3 moveLateral = Input.GetAxis("LeftStickHorizontal") * cameraRight;
        Quaternion desiredRotation = Player.transform.rotation;


        if (moveTranslation != Vector3.zero || moveLateral != Vector3.zero)
        {
            desiredRotation = Quaternion.LookRotation(moveTranslation + moveLateral);
        }

        Player.transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 200f);

        //if (m_IsMoving)
        //{
        InputAnchor.transform.rotation = Quaternion.Slerp(new Quaternion(), CameraRotationCore.transform.rotation, Time.deltaTime * CameraRotationSpeed * .000001f);
        //}

        Vector3 gravity = GravityScale * Vector3.up;
        PlayerAnchor.GetComponent<Rigidbody>().AddForce(gravity, ForceMode.Acceleration);

        if (Input.GetButtonDown("Jump"))
        {
            PlayerAnchor.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
        }
    }

    public void TravelTo(GameObject target)
    {
        m_TempGravHolder = GravityScale;
        GravityScale = 0;

        m_IsTraveling = true;
        m_TravelTarget = target;
    }

    [SerializeField]
    private Vector3 CameraPosition;
    [SerializeField]
    private float m_CameraRotationValue;
    [SerializeField]
    private bool m_IsMoving;
    [SerializeField]
    private bool m_IsTraveling;
    [SerializeField]
    private GameObject m_TravelTarget;

    [SerializeField]
    private float m_TempGravHolder;

    [SerializeField]
    private float m_CharMass = 1000.0f;

    [SerializeField]
    private float m_GravConst = 100.0f;

    [SerializeField]
    private float m_StepEPS = 1 / 10.0f;
}
