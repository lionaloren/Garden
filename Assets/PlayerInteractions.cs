using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public int cropsCollected = 0;
    public Animator bodyAnimator;   // assign Player_Body animator
    public Animator hairAnimator;   // assign Player_Hair animator
    public float recoilForce = 10f;
    public PlayerMovement playerMovement;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            // Recoil
            Vector2 recoilDir = (transform.position - collision.transform.position).normalized;

            if (collision.contacts.Length > 0)
            {
                // Menggunakan normal dari kontak tabrakan
                recoilDir = collision.contacts[0].normal;
            }
            else
            {
                // Fallback (seperti kode lama Anda)
                recoilDir = (transform.position - collision.transform.position).normalized;
            }

            rb.velocity = Vector2.zero;
            rb.AddForce(recoilDir * recoilForce, ForceMode2D.Impulse);

            if (playerMovement != null)
            {
                playerMovement.DisableMovementForDuration(0.2f); // 0.2 detik (sesuaikan)
            }

            // Play hurt animation
            bodyAnimator.SetTrigger("Hurt");
            if (hairAnimator != null)
                hairAnimator.SetTrigger("Hurt");

            Debug.Log("Player is hurt");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crop"))
        {
            Destroy(other.gameObject);
            cropsCollected++;
            Debug.Log("Crop harvested: " + cropsCollected);
        }
        else if (other.CompareTag("Animal"))
        {
            string animalName = other.name.ToUpper();
            string sound;

            switch (animalName)
            {
                case "COW": sound = "MOO"; break;
                case "CHICKEN": sound = "CLUCK"; break;
                case "PIG": sound = "OINK"; break;
                case "SHEEP": sound = "BAA"; break;
                default: sound = "ANIMAL SOUND"; break;
            }

            Debug.Log(sound);
        }
    }
}