using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Vector2 rawInput;

    void Update()
    {
        MovePlayer();

    }

    private void MovePlayer()
    {
        Vector3 delta = rawInput * moveSpeed * Time.deltaTime;
        transform.position += delta;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }
}
