using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleMovement : MonoBehaviour
{   
    // The speed of movement
    public float TranslateSpeed;

    // The time of movement
    private float TranslateTime;

    // Start is called before the first frame update
    void Start()
    {
        TranslateTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        TranslateTime = TranslateTime + 0.1f;

        transform.Translate(Vector3.right * TranslateSpeed);

        if (TranslateTime > 10.0f) 
        {
            transform.Rotate(0, 180, 0);
            TranslateTime = 0.0f;
        }
    }
}
