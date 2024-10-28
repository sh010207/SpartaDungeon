using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;
    private Vector2 curMonvementInput;
    public float jumpForce;
    public LayerMask layerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float mixXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody> ();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMonvementInput = context.ReadValue<Vector2>();
            
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMonvementInput = Vector2.zero;
        }
    }
    private void Move()
    {
        Debug.Log($"curMonvementInput {curMonvementInput} !!!!!!!!!");
        Vector2 dirY = Vector3.forward * curMonvementInput.y;
        Vector2 dirX= Vector3.right * curMonvementInput.x;
        Vector2 dir = dirX + dirY;

        Debug.Log($"Vector2 dirY {dirY} = {transform.right} * {curMonvementInput.y}");
        Debug.Log($"Vector2 dirX {dirX} = {transform.forward} * {curMonvementInput.x}");

        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, mixXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot,0,0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    //bool IsGrounded()
    //{
    //    Ray[] rays = new Ray[4]
    //    {
    //        new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
    //        new Ray(transform.position + (transform.forward * 0.2f) + (-transform.up * 0.01f), Vector3.down),
    //        new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
    //        new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
    //    };

    //    for(int i = 0; i < rays.Length; i++)
    //    {
    //        if (Physics.Raycast(rays[i], 0.1f, layerMask))
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    //public void ToggleCursor(bool toggle)
    //{
    //    Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
    //    canLook = !toggle;
    //}
}
