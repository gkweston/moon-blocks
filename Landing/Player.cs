using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour    // in reality "Player" is a alias for the LM & CSM game object(s)
{
    

    public float gravity = -1.6f;    // allows gravity to be changed for testing purposes
    private float _appliedGravity;    //calculated based on set gravity to define acceleration each frame
    
    private float _ascentModuleMass = 2445f;    // these floats will be updated to reflect historically accurate values
    private float _massBasedThrust = 7040f;
    private float _sustainedThrust;
    public float powerThrust = 5f;
    public float abortThrust = 5500f;
    
    public Vector3 velocity;
    public Vector3 landerPosition;    // this will be updated to a Transform, allows LM & CSM position to be shared between the two
    Controller2D controller;
    
    private ParticleSystem _downwardThrust;    // only private ParticleSystem (PS) as it is defined in direct relation to game object
    public ParticleSystem LHSParticleSystem;    // public to allow PS's to be attached to game object and update their position
    public ParticleSystem RHSParticleSystem;
    public ParticleSystem UpperParticleSystem;
    
    public Camera mainCamera;    // allows camera to track game object
    private Vector3 cameraPosition;
    
    private bool _powerThrustMode;    // for easy definition of extra thrust (currently only two powers)

    
    void Start() 
    {
        controller = GetComponent<Controller2D> ();
        _downwardThrust = GetComponent<ParticleSystem>();
        _sustainedThrust = (_massBasedThrust / (_ascentModuleMass)) * Time.deltaTime;    // used to allow reflect of historically accurate parameters
        _appliedGravity = gravity * Time.deltaTime;
    }

    public void Update()
    {
        
        Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));    // currently unused, useful for defining axis

        
        var lowerEmission = _downwardThrust.emission;    // allow seperate game object PS's to be controlled by game object
        var LHSEmission = LHSParticleSystem.emission;
        var LHSSpeed = LHSParticleSystem.main.startSpeed;
        var RHSEmission = RHSParticleSystem.emission;
        var RHSSpeed = RHSParticleSystem.main.startSpeed;
        var UpperEmission = UpperParticleSystem.emission;
        var UpperSpeed = UpperParticleSystem.main.startSpeed;
        
        _downwardThrust.Stop();    // defaults PS status as stopped
        LHSParticleSystem.Stop();
        RHSParticleSystem.Stop();
        UpperParticleSystem.Stop();

        Vector3 landerPosition = transform.position;    // transform for updating position given thrust and gravity
        
        _downwardThrust.startLifetime = landerPosition.y < 20 ? 5 : 0.4f;    // changes characteristics of PS below game object to emulate lunar dust upon landing
        
        
        if (controller.collisions.below || controller.collisions.above)    // calls collision info to bring game object to rest if colliding w/ lunar surface
        {
            velocity.x = 0;
            velocity.y = 0;
        }
        
        _powerThrustMode = Input.GetKey(KeyCode.Space);    // defines the extra thrust power which alters acceleration and PS output
        
        if (Input.GetKeyDown((KeyCode.RightShift)))    // abort thrust which will be converted into ascent thrust when LM leaves lunar surface
        {
            velocity.y += (abortThrust + gravity) * Time.deltaTime;
            lowerEmission.rateOverTime = 10000f;
        }
        
        if (Input.GetKey((KeyCode.UpArrow)))    // (###) identical thrust and PS control process across all directions
        {
            _downwardThrust.Play();    // show PS when thrust is applied
            lowerEmission.rateOverTime = 500f;    // define default PS intensity
            
            if (_powerThrustMode)
            {
                velocity.y += (powerThrust + gravity) * Time.deltaTime;    // increase acceleration from thrust and PS intensity if power thrust is active
                lowerEmission.rateOverTime = 1500.0f;
            }
            else
            {
                velocity.y += _sustainedThrust + _appliedGravity;    // default non-power thrust values
            }
        }

        if (Input.GetKey((KeyCode.DownArrow)))    // refer to (###)
        {
            
            UpperParticleSystem.Play();
            
            if (_powerThrustMode)
            {
                velocity.y -= (powerThrust + gravity) * Time.deltaTime;
                UpperEmission.rateOverTime = 1000f;
                UpperSpeed = 150f;    // tertiary PS characteristic; likely to be removed for simplicity

            }
            else
            {
                velocity.y -= _sustainedThrust + _appliedGravity;
                UpperEmission.rateOverTime = 500f;
                UpperSpeed = 77f;
            }
        }

        if (Input.GetKey((KeyCode.RightArrow)))    // refer to (###)
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

        if (Input.GetKey((KeyCode.LeftArrow)))    // refer to (###)
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
        
        velocity.y += _appliedGravity;    // update velocity
        controller.Move (velocity * Time.deltaTime);    // utilize Move void defined in Controller2D to update transform

        LHSParticleSystem.transform.position = landerPosition;    // ensures PSs translate as LM & CSM translate
        RHSParticleSystem.transform.position = landerPosition;
        UpperParticleSystem.transform.position = landerPosition;
        mainCamera.transform.position = landerPosition;
        mainCamera.transform.Translate(0, 0, -5);    // ensures camera tracks LM & CSM
    }
}
