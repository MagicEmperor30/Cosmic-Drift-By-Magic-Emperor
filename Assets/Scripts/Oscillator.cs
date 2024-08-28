using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField][Range(0,1)]float movementFactor; 
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        OscillateObstacle();
    }

    void OscillateObstacle()
    {
        if(period < Mathf.Epsilon){return;}
        float cycles = Time.time / period; //continually growing over time
        const float tau = Mathf.PI * 2; //constant at 6.28
        float rawSinWave = MathF.Sin(cycles * tau);//going from -1 to 1
        movementFactor = (rawSinWave + 1f) / 2f;//recalculated to go from 0 to 1
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
