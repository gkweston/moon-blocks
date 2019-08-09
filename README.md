# moon-blocks
A simple 2D moon landing sim for learning C#, APIs and implementing automation

On the 50th anniversary of the Apollo 11 moon landings I started planning a 2D lunar lander as an academic exercise in
developing APIs and optimizing processes via various artificial intelligence methods. The final project will be broken into
three stages:

1. Take-off & pre-orbital maneuvers:

    a. Fuel/weight ratio
    
    b. Atmospheric escape
    
    c. Final stage seperation into command/service module (CM) & lunar module (LM)
    
2. Navigation and lunar orbit:

    a. Translunar injection
    
    b. Course correction
    
    c. Lunar orbital insertion
    
3. Lunar landing & escape:

    a. Undocking LM from CM
    
    b. Maintaining CM orbit during LM descent
    
    c. LM descent, touchdown & ascent
    
    d. LM & CM docking
    
    e. Earthbound Navigation
    
    
Each of the stages are being devloped in reverse, with each sub-stage developed in no particular order. Once stage 3 is
complete, various automation scripts will be written and tested to complete & optimize each sub-stage. Then stage 2
be completed and automation techniques implemented to complete it. So on, until stages 1-3 can be automated & optimized. Each
stage can be played like a mini-game by a user, or the can be run together for a rudimentary lunar landing simulator.

Upon completion of stage 3, specific parameters required w/i the Unity Engine so a user can run the stage in its entirety
will be layed out here, or it will be published as a stand-alone application. (08/07/2019)

Controller2D script, (collisions etc.) is derived from work done by Sebastian Lague (https://github.com/SebLague/2DPlatformer-Tutorial)

