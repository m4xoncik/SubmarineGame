using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f; // Viteza de deplasare

    private Rigidbody2D rb;

    void Start()
    {
        // Obține Rigidbody2D asociat cu submarinul
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Preia input-ul pentru mișcare pe verticală (sus/jos: W/S sau săgeți sus/jos)
        float verticalInput = Input.GetAxis("Vertical");

        // Preia input-ul pentru mișcare pe orizontală (stânga/dreapta: A/D sau săgeți stânga/dreapta)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Aplica mișcarea
        MoveSubmarine(horizontalInput, verticalInput);
    }

    void MoveSubmarine(float horizontal, float vertical)
    {
        // Calculează direcția de mișcare
        Vector2 movement = new Vector2(horizontal, vertical) * speed;

        // Setează viteza pe Rigidbody2D
        rb.linearVelocity = movement;
    }
}
