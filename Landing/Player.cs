using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour
{
    
    private float _appliedGravity;
    public float gravity = -1.6f;
    
    private float _ascentModuleMass = 2445f;
    private float _massBasedThrust = 7040f;
    private float _sustainedThrust;
    public float powerThrust = 5f;
    private bool _powerThrustMode;
    public float abortThrust = 5500f;
    
    public Vector3 velocity;
    public Vector3 landerPosition;
    Controller2D controller;
    
    private ParticleSystem _downwardThrust;
    public ParticleSystem LHSParticleSystem;
    public ParticleSystem RHSParticleSystem;
    public ParticleSystem UpperParticleSystem;
    public Camera mainCamera;
    private Vector3 cameraPosition;
    
    void Start() 
    {
        controller = GetComponent<Controller2D> ();
        _downwardThrust = GetComponent<ParticleSystem>();
        _sustainedThrust = (_massBasedThrust / (_ascentModuleMass)) * Time.deltaTime;    // sans propellant mass
        _appliedGravity = gravity * Time.deltaTime;
    }

    public void Update()
    {
        
        Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

        
        var lowerEmission = _downwardThrust.emission;
        var LHSEmission = LHSParticleSystem.emission;
        var LHSSpeed = LHSParticleSystem.main.startSpeed;
        var RHSEmission = RHSParticleSystem.emission;
        var RHSSpeed = RHSParticleSystem.main.startSpeed;
        var UpperEmission = UpperParticleSystem.emission;
        var UpperSpeed = UpperParticleSystem.main.startSpeed;
        
        _downwardThrust.Stop();
        LHSParticleSystem.Stop();
        RHSParticleSystem.Stop();
        UpperParticleSystem.Stop();

        Vector3 landerPosition = transform.position;
        
        //  if landerPosition < 10 particle emission startlifetime = 5
        _downwardThrust.startLifetime = landerPosition.y < 20 ? 5 : 0.4f;
        //
        
        if (controller.collisions.below || controller.collisions.above)
        {
            velocity.x = 0;
            velocity.y = 0;
        }
        
        _powerThrustMode = Input.GetKey(KeyCode.Space);
        
        if (Input.GetKeyDown((KeyCode.RightShift)))    
        {
            velocity.y += (abortThrust + gravity) * Time.deltaTime;
            lowerEmission.rateOverTime = 10000f;
        }
        
        if (Input.GetKey((KeyCode.UpArrow))) 
        {
            _downwardThrust.Play();
            lowerEmission.rateOverTime = 500f;
            
            if (_powerThrustMode)
            {
                velocity.y += (powerThrust + gravity) * Time.deltaTime;
                lowerEmission.rateOverTime = 1500.0f;
            }
            else
            {
                velocity.y += _sustainedThrust + _appliedGravity;
            }
        }

        if (Input.GetKey((KeyCode.DownArrow))) 
        {
            
            UpperParticleSystem.Play();
            
            if (_powerThrustMode)
            {
                velocity.y -= (powerThrust + gravity) * Time.deltaTime;
                UpperEmission.rateOverTime = 1000f;
                UpperSpeed = 150f;

            }
            else
            {
                velocity.y -= _sustainedThrust + _appliedGravity;
                UpperEmission.rateOverTime = 500f;
                UpperSpeed = 77f;
            }
        }

        if (Input.GetKey((KeyCode.RightArrow))) 
        {
            LHSParticleSystem.Play();
            
            if (_powerThrustMode)
            {
                velocity.x += (powerThrust + gravity) * Time.deltaTime;
                LHSEmission.rateOverTime = 1000f;
                LHSSpeed = 150f;
            }
            else
            {
                velocity.x += _sustainedThrust + _appliedGravity;
                LHSEmission.rateOverTime = 300f;
                LHSSpeed = 10f;
            }
        }

        if (Input.GetKey((KeyCode.LeftArrow))) 
        {
            
            RHSParticleSystem.Play();
            
            if (_powerThrustMode)
            {
                velocity.x -= (powerThrust + gravity) * Time.deltaTime;
                RHSEmission.rateOverTime = 1000f;
                RHSSpeed = 150f;
            }
            else
            {
                velocity.x -= _sustainedThrust + _appliedGravity;
                RHSEmission.rateOverTime = 300f;
                RHSSpeed = 77f;
            }
        }
        
        velocity.y += _appliedGravity;
        controller.Move (velocity * Time.deltaTime);

        LHSParticleSystem.transform.position = landerPosition;
        RHSParticleSystem.transform.position = landerPosition;
        UpperParticleSystem.transform.position = landerPosition;
        mainCamera.transform.position = landerPosition;
        mainCamera.transform.Translate(0, 0, -5);
        
    }
}
