using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Vector3 touchPosition;

    public string levelName;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private AudioSource audioSource;
    private ParticleSystem dieParticles;
    private HardLight2D light;
    private SpriteRenderer fade;

    private Vector3 direction;
    private float moveSpeed = 10f;
    private bool allowInput = true;

    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        dieParticles = GameObject.Find("Player/DieEffect").GetComponent<ParticleSystem>();
        light = GetComponentInChildren<HardLight2D>();
        fade = GameObject.Find("Camera/Fade").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && allowInput)
        {
           
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;

            if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // We don't want to trigger when touching a wall.
        if (collision.gameObject.tag == "Wall")
        {
            return;
        }

        // Apply a little force indicating death.
        rb.velocity = new Vector2(1, 3);

        if (!dead)
        {
            audioSource.Play();
            rb.gravityScale = 1;
            bc.enabled = false;
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            allowInput = false;
            sprite.material.SetVector("_EmissionColor", Color.red);
            light.Color = Color.Lerp(light.Color, Color.red, 2.0f);
            fade.color = Color.Lerp(fade.color, Color.black, 60.0f);
            //dieParticles.Play();
        }

        dead = true;
    }
}
