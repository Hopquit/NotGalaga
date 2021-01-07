using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien2Controller : MonoBehaviour
{
    public float secondsPerBullet = 0.25f;
    float cooldown = 0;
    float direction = 1;
    public float speed = 4;
    SpriteRenderer sprite;
    public Transform prefab;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * direction * Time.deltaTime, 0, 0);
        if (cooldown <= 0)
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
        if(transform.position.x > newPosition.x){
            direction = -1;
        }
        if(transform.position.x < newPosition.x){
            direction = 1;
        }
        transform.position = newPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("other");
    }
}
