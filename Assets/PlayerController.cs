using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;
    public Transform prefab;

    public float secondsPerBullet = 0.25f;
    float cooldown = 0;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
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
    
    void OnTriggerEnter(Collider other) {
        Debug.Log(other);
    }
}
