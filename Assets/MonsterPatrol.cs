using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public float patrolDistance = 3f;
    public float idleTime = 0.5f;
    public bool startMovingRight = true;
    public bool flipOnTurn = true;

    [Header("Animation")]
    public Animator animator; // assign Goblin_Animator or Skeleton_Animator
    public string walkParam = "IsWalking"; // Animator bool parameter

    private Vector3 startPos;
    private int direction;
    private bool isIdle = false;
    private float idleTimer = 0f;

    void Start()
    {
        startPos = transform.position;
        direction = startMovingRight ? 1 : -1;
    }

    void Update()
    {
        if (isIdle)
        {
            idleTimer -= Time.deltaTime;
            animator.SetBool(walkParam, false); // idle animation
            if (idleTimer <= 0f)
                isIdle = false;
            return;
        }

        // Move
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        animator.SetBool(walkParam, true); // walking animation

        // Check patrol limits
        if (Mathf.Abs(transform.position.x - startPos.x) >= patrolDistance)
        {
            direction *= -1;
            if (flipOnTurn)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }

            // Start idle
            isIdle = true;
            idleTimer = idleTime;
        }
    }
}