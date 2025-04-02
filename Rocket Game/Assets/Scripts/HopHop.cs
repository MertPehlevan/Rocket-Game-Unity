using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopHop : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 movementVector;
    Vector3 startPosition;
    Vector3 endPosition;
    float movementUnit;

    void Start()
    {
        Position();
    }
    void Update()
    {
        MoveToPiston();
    }

    private void Position()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }



    private void MoveToPiston()
    {
        movementUnit = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementUnit);
    }
}
