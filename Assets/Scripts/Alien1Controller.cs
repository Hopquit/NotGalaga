using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien1Controller : MonoBehaviour
{
    public float speed = 4;
    public GameObject player = null;
    SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 direction;
            direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.Translate(speed * direction * Time.deltaTime);
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

}
