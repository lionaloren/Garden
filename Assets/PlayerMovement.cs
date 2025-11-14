using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;
    public Animator hairAnimator;
    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        bool isWalking = movement.sqrMagnitude > 0;

        animator.SetBool("IsWalking", isWalking);
        animator.SetFloat("MoveX", movement.x);

        if (hairAnimator != null)
        {
            hairAnimator.SetBool("IsWalking", isWalking);
            hairAnimator.SetFloat("MoveX", movement.x);
        }

        if (movement.x != 0)
        {
            float flip = movement.x > 0 ? 1 : -1;
            transform.localScale = new Vector3(flip, 1, 1);
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = movement * moveSpeed;
        }
    }

    public void DisableMovementForDuration(float duration)
    {
        if (canMove)
        {
            canMove = false;
            // Memulai Coroutine untuk mengaktifkan gerakan kembali setelah durasi
            StartCoroutine(EnableMovementAfterTime(duration));
        }
    }

    IEnumerator EnableMovementAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        canMove = true;
    }
}
