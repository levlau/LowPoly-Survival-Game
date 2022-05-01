using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    [SerializeField] [Range(0f, 0.5f)] float smoothTime;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;

    Vector2 targetDir;
    Vector3 velocity;

    Vector2 currentDir;
    Vector2 currentDirVelocity;

    Rigidbody rb;
    GravityAttractor planet;

    bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();    //muss bei mehreren planeten geändert werden
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //zielvektor setzen
        targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //die länge auf 1 setzen, damit man diagonal nicht schneller ist
        targetDir.Normalize();

        //den aktuellen vektor für das movement smoothen
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, smoothTime);

        //den movementvektor setzen
        velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * movementSpeed;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius);
        //gravity
        if (!isGrounded)
        {
            planet.GetAttracted(transform);
        }

        //am ende bewegen
        //controller.Move(velocity * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, planet.transform.position - transform.position);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, -transform.up);
    }

}
