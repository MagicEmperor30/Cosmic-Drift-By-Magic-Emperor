using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 600f; 
    [SerializeField] float rotateThrust = 50f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    Rigidbody rb;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust(){
        if(Input.GetKey(KeyCode.Space))
        {
            ProcessThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    void ProcessRotation(){
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void ProcessThrusting()
    {
        //Debug.Log("Space bar Pressed");
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainThrustParticle.isPlaying)
        {
            mainThrustParticle.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticle.Stop();
    }
    void ApplyRotation(float rotateThisFrame){
        rb.freezeRotation = true; //freeze rotation when manually rotating
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateThisFrame);
        rb.freezeRotation = false; // unfreeze rotation
    }    

    void RotateLeft()
    {
        //Debug.Log("A pressed rotate Left");
        ApplyRotation(rotateThrust);
        if (!leftThrustParticle.isPlaying)
        {
            leftThrustParticle.Play();
        }
    }
    void RotateRight()
    {
        //Debug.Log("D Pressed rotate rigth");
        ApplyRotation(-rotateThrust);
        if (!rightThrustParticle.isPlaying)
        {
            rightThrustParticle.Play();
        }
    }
    void StopRotating()
    {
        rightThrustParticle.Stop();
        leftThrustParticle.Stop();
    }
}
