using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawnpointController : MonoBehaviour
{
    float direction = 1;
    public float speed = 4;
    void Start()
    {
        
    }

    // Update is called once per frame
    
       void Update()
    {
        transform.Translate(speed * direction * Time.deltaTime, 0, 0);
    }
    void LateUpdate()
    {
        Vector3 newPosition = transform.position;
        float xBounds = Camera.main.orthographicSize * Screen.width / Screen.height;
        float yBounds = Camera.main.orthographicSize;
        float xMax = xBounds;
        float xMin = -xBounds;
        float yMax = yBounds;
        float yMin = -yBounds;
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
}
