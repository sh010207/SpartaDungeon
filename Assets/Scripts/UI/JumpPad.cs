using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public GameObject player;
    public float thrust = 100f;
    private Rigidbody rb;

    //private void FixedUpdate()
    //{
    //    AddForce();
    //}

    private void Start()
    {
        rb = CharacterManager.Instance.Player._rb;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            rb.AddForce(Vector3.up * thrust, ForceMode.Impulse);
        }   
    }
}
