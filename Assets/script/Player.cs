using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("jUMP down");
        //}
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    Debug.Log("jUMP");
        //}
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    Debug.Log("jUMP up");
        //}
        Vector2 position = transform.position;
        Vector2 scale = transform.localScale;
        if (Input.GetKey(KeyCode.A)) {
            position.x -= 1*Time.deltaTime;       
            scale.x = 1;
            
        } 
        if(Input.GetKey(KeyCode.D)) {
            position.x += 1 * Time.deltaTime;
            scale.x = -1;
        }
        transform.localScale = scale;
        transform.position = position;

    }
}
