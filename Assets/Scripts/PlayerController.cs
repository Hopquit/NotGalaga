using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 4;
    int currentHealth;
    public float speed = 2.0f;
    public Transform prefab;

    public Transform explosion;

    public float secondsPerBullet = 0.25f;
    float cooldown = 0;
    SpriteRenderer sprite;
    float invincibility = 0;
    public float maxInvincibility = 1.5f;
    bool hasDamageSource = false;
    public TMP_Text healthText;
    public Transform pickupSound;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        healthText.text = "Health: " + currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float velX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float velY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(velX, velY, 0);

        if (Input.GetKey("space") && cooldown <= 0)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            cooldown = secondsPerBullet;
        }

        cooldown -= Time.deltaTime;
        if (currentHealth <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy (gameObject);
            EventBus.TriggerEvent("PlayerDied");
        }
        invincibility -= Time.deltaTime;
        if (invincibility <= 0 && hasDamageSource)
        {
            invincibility = maxInvincibility;
            currentHealth -= 1;
            EventBus.TriggerEvent("PlayerHit");

            healthText.text = "Health: " + currentHealth;
        }
    }
    void LateUpdate()
    {
        Vector3 newPosition = transform.position;
        float xBounds = Camera.main.orthographicSize * Screen.width / Screen.height;
        float yBounds = Camera.main.orthographicSize;
        float xMax = xBounds - sprite.bounds.size.x / 2;
        float xMin = -xBounds + sprite.bounds.size.x / 2;
        float yMax = yBounds - sprite.bounds.size.y / 2;
        float yMin = -yBounds + sprite.bounds.size.y / 2;
        newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax);
        transform.position = newPosition;
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other);
        if (other.collider.tag == "AlienBullet")
        {
            Destroy(other.gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "AlienBullet")
        {
            Destroy(other.gameObject);
            currentHealth -= 1;
            EventBus.TriggerEvent("PlayerHit");

            healthText.text = "Health: " + currentHealth;
        }
        else if (other.tag == "Alien1" || other.tag == "Laser")
        {
            currentHealth -= 1;
            EventBus.TriggerEvent("PlayerHit");

            healthText.text = "Health: " + currentHealth;
            invincibility = maxInvincibility;
            hasDamageSource = true;
        }
        else if (other.tag == "MedicDrop")
        {
            Destroy(other.gameObject);
            currentHealth += 1;
            Instantiate (pickupSound);
            healthText.text = "Health: " + currentHealth;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Alien1" || other.tag == "Laser")
        {
            hasDamageSource = false;
        }
    }
    
}
