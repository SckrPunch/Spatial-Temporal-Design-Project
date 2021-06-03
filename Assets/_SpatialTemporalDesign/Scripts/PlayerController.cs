using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    [Range(-30,30)]
    public float forwardBias;
    
    public float cameraSpeed;
    public bool invertHorizontal;
    public bool invertVertical;
    
    private CinemachineFreeLook freeCam;
    private Vector3 steering;
    private Vector3 moveDir;
    private Rigidbody rb;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        freeCam = GetComponentInChildren<CinemachineFreeLook>();
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        moveDir = Quaternion.AngleAxis(forwardBias, Vector3.up) * 
                  (Quaternion.FromToRotation(Vector3.forward, forward) * steering);

        if (moveDir.magnitude >= 0.01f)
        {
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveDir, Vector3.up), rotationSpeed));
            rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
        }
    }
    
    public void OnLook(InputValue ctx)
    {
        Vector2 delta = ctx.Get<Vector2>();
        Debug.Log(delta);
        freeCam.m_YAxis.Value += delta.y * cameraSpeed * Time.deltaTime * (invertVertical   ? -1 : 1);
        freeCam.m_XAxis.Value += delta.x * 180f * cameraSpeed * Time.deltaTime * (invertHorizontal ? -1 : 1);
        if (delta.magnitude > 0.1f)
            freeCam.m_YAxisRecentering.CancelRecentering();
    }

    public void OnMove(InputValue ctx)
    {
        steering = new Vector3(ctx.Get<Vector2>().x, 0f, ctx.Get<Vector2>().y);
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.up + transform.position, transform.forward);
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.up + transform.position, moveDir);
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.up + transform.position, Camera.main.transform.forward);
    }
}
