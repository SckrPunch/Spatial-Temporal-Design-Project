using System;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;


// TODO | This implementation assumes the track is perfectly straight.
// TODO | For turns and curves, some external script needs to alter the Cinemachine Camera state and the desired 
// TODO | forward direction so the character continues to control smoothly.
[RequireComponent(typeof(Rigidbody))]
public class SnowboardPlayerController : MonoBehaviour
{
    // Internal Constants
    private const float GROUND_DIST = 0.5f;
    
    
    // Determines camera follow direction and angular movement adjustments
    public Transform FollowCam;

    [SerializeField]
    private Transform frontAnchor;
    [SerializeField]
    private Transform backAnchor;
    
    // Is the character on the ground or airborne?
    public bool IsGrounded { get; private set; }

    public float DriftMultiplier;
    public float LateralForce;
    public float JumpImpulse;
    public float UpwardGravityMultiplier = 1;
    public float DownwardGravityMultiplier = 1;

    public float SpeedCap;

    public PhysicMaterial normal;
    public PhysicMaterial brake;

    // DEBUG TEXT
    public Text debug;
    
    // Input Values
    private Vector2 rawInput;
    private Vector2 camInput;
    private bool braking;

    private Rigidbody rb;
    private Vector3 steering;
    private Vector3 groundNormal; // Vector3.up if !IsGrounded
    // private Vector3 acceleration;
    
    #region Player Input

    public void OnBrake(InputValue value)
    {
        braking = value.isPressed;
        foreach (Collider c in GetComponentsInChildren<Collider>())
            c.material = braking ? brake : normal;
    }

    public void OnJump(InputValue value)
    {
        if (IsGrounded)
            rb.AddForce(groundNormal * JumpImpulse * (rb.velocity.y > 0 ? 1 : 2), ForceMode.VelocityChange); 
    }

    public void OnMoveSteering(InputValue value){ rawInput = value.Get<Vector2>(); }
    public void OnCameraTilt(InputValue value)  { camInput = value.Get<Vector2>(); }
    
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(JumpImpulse * transform.forward, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        CalculateSteering();
        
        rb.AddForce(steering * LateralForce, ForceMode.Acceleration);
                     
        if (!IsGrounded)
        {
            rb.useGravity = false;
            rb.AddForce(Physics.gravity * (rb.velocity.y > 0 ? UpwardGravityMultiplier : DownwardGravityMultiplier), 
                ForceMode.Acceleration);
        }
        else
            rb.useGravity = true;

        if (rb.velocity.magnitude > SpeedCap)
            rb.velocity = rb.velocity.normalized * SpeedCap;
        UpdateOrientation();
        
        //rb.MoveRotation(Quaternion.LookRotation(rb.velocity - Projection(rb.velocity, groundNormal),groundNormal));
    }

    private void CheckGrounded()
    {
        RaycastHit hitInfo;
        IsGrounded = Physics.Raycast(new Ray(frontAnchor.position, -groundNormal), out hitInfo, GROUND_DIST);
        if (!IsGrounded)
            IsGrounded = Physics.Raycast(new Ray(backAnchor.position, -groundNormal), out hitInfo, GROUND_DIST);
        groundNormal = IsGrounded ? hitInfo.normal : Vector3.up;
    }

    private void CalculateSteering()
    {
        Vector3 camVector = transform.position - FollowCam.position;
        Vector3 heading = (camVector - Projection(camVector,Vector3.up)).normalized;

        if (IsGrounded)
            heading = (heading - Projection(heading, groundNormal)).normalized;

        // UP X FORWARD = RIGHT
        Vector3 right = Vector3.Cross(groundNormal, heading) * (braking ? 2 : 1);

        if (rawInput.magnitude >= 1)
            rawInput = rawInput.normalized;

        steering = (heading.normalized * rawInput.y) + (right.normalized * rawInput.x * (braking ? DriftMultiplier : 1));
    }

    private void UpdateOrientation()
    {
        Quaternion targetRotation;
        Vector3 lateralVelocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        
        if (rb.velocity.magnitude <= .5f || lateralVelocity.sqrMagnitude < 0.5f)
            targetRotation = 
                Quaternion.LookRotation(transform.forward - Projection(transform.forward, groundNormal), groundNormal);
        else
        {
            targetRotation = Quaternion.LookRotation(rb.velocity, groundNormal);
            rb.angularVelocity = Vector3.zero;
        }
            
       
        
        rb.MoveRotation(targetRotation);
        
    }
    

    #region Math Shit

    private Vector3 Projection(Vector3 from, Vector3 to)
    {
        return (Vector3.Dot(from, to) / Vector3.Dot(to, to)) * to;
    }

    #endregion
}
