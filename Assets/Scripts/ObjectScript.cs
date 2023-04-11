using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    private GameSystem gameSystem;
    public bool isGravity;
    public float gravityScale = 1f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        isGravity = false;
        gameSystem = GameObject.FindGameObjectWithTag("GameSystem").GetComponent<GameSystem>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isGravity)
            rb.AddForce(Physics.gravity * gravityScale, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            gameSystem.GameOver();
        }
        else
        {
            gameSystem.AddScore();
        }
    }
}
