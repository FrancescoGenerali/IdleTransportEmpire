using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float camSpeed = 10f;
    public float boardTolerance = 10f;
    public float xlimit;
    public float zlimit;

    void Update()
    {
        Vector3 pos = transform.position;
        
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - boardTolerance)
        {
            pos.x += camSpeed * Time.deltaTime;
            pos.z += camSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= boardTolerance)
        {
            pos.x -= camSpeed * Time.deltaTime;
            pos.z -= camSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= boardTolerance)
        {
            pos.x -= camSpeed * Time.deltaTime;
            pos.z += camSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - boardTolerance)
        {
            pos.x += camSpeed * Time.deltaTime;
            pos.z -= camSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -xlimit, xlimit);
        pos.z = Mathf.Clamp(pos.z, -zlimit, zlimit);

        transform.position = pos;
    }
}
