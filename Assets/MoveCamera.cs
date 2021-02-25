using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Camera camera;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get player position in screen space
        Vector2 screenPos = camera.WorldToScreenPoint(player.position);
        float relativHeight = screenPos.y / Screen.height;
        
        if (relativHeight < 0.2)
        {
            transform.Translate(new Vector3(0, -4 * Time.deltaTime, 0));
        } else
        {
            transform.Translate(new Vector3(0, -1 * Time.deltaTime, 0));
        }        
    }
}
