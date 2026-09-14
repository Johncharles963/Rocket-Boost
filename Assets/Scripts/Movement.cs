using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustForce = 100f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ApplyThrust();
        ProcessRotation();
    }

    private void ApplyThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }
    }


    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        if (!mainThrusterParticles.isPlaying)
        {
            mainThrusterParticles.Play();
        }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        mainThrusterParticles.Stop();
    }

    private void ProcessRotation()
    {
        StartRotation();
    }

    private void StartRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            RotateLeft();
        }
        else if (rotationInput > 0)
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }
    private void RotateLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!rightThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Play();
        }
    }
    private void RotateRight()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Play();
        }
    }
    private void StopRotation()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }
    
    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
