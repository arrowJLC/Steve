using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //public Transform playerTransform;

    public float minXValue;
    public float maxXValue;


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.PlayerInstance) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(GameManager.Instance.PlayerInstance.transform.position.x, minXValue, maxXValue);
        transform.position = pos;
    }
}
