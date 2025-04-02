using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float rotationStrenght = 10f;
    [SerializeField] float thurstStrenght = 100f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightThurstParticles;
    [SerializeField] ParticleSystem leftThurustParticles;

    Rigidbody rb;
    AudioSource audioSource;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() // karakterimiz olduğunde tekrar doğsun ya da oyun dursun diye kullandığımız bir metod
                            // OnEnable() → Oyun başladığında girişleri (InputAction) etkinleştiriyor
    {
        thrust.Enable();
        rotation.Enable();
    }
    private void FixedUpdate()
    {
        ForceToPlayer();
        RotationPlayer();
    }

    void ForceToPlayer()
    {
        if (thrust.IsPressed()) // Eğer thrust tuşu basılı tutuluyorsa (IsPressed()), karaktere yukarı doğru bir kuvvet uyguluyor.
        {
            StartForce();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartForce()
    {
        rb.AddRelativeForce(Vector3.up * thurstStrenght * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

   
    void RotationPlayer()
    {
        rb.freezeRotation = true;
        float rotationInput = rotation.ReadValue<float>(); // Kullanıcının girişini okuyor (örneğin, A veya D tuşlarına basıyorsa).
        if (rotationInput < 0)
        {
            RotateRight();
        }
        else if (rotationInput > 0)
        {
            RotateLeft();

        }
        else
        {
            StopRotation();
        }
        rb.freezeRotation = false;


    }

    private void StopRotation()
    {
        rightThurstParticles.Stop();
        leftThurustParticles.Stop();
    }

    private void RotateLeft()
    {
        NegativeRotation();

        if (!leftThurustParticles.isPlaying)
        {
            rightThurstParticles.Stop();
            leftThurustParticles.Play();
        }
    }

    private void RotateRight()
    {
        PositiveRotation();
        leftThurustParticles.Stop();
        rightThurstParticles.Play();
    }

    private void PositiveRotation()
    {
        transform.Rotate(Vector3.forward * rotationStrenght * Time.fixedDeltaTime);
    }
   private void NegativeRotation()
    {
        transform.Rotate(Vector3.forward * -rotationStrenght * Time.fixedDeltaTime);
    }
}
