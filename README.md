# moon-blocks
A simple 2D moon landing sim for learning C#, APIs and implementing automation (Unity Engine)

On the 50th anniversary of the Apollo 11 moon landings I started planning a 2D lunar lander as an academic exercise in
developing APIs and optimizing processes via various automation & AI methods. The final project will be broken into
three stages, all of which utilize the Unity Engine:

__1. Take-off & pre-orbital maneuvers (planned):__

    a. Fuel/weight ratio
    
    b. Atmospheric escape
    
    c. Final stage seperation into command/service module (CM) & lunar module (LM)
    
__2. Navigation and lunar orbit (planned):__

    a. Translunar injection
    
    b. Course correction
    
    c. Lunar orbital insertion
    
__3. Lunar landing & escape (in progress):__

    a. Undocking LM from CM
    
    b. Maintaining CM orbit during LM descent
    
    c. LM descent, touchdown & ascent
    
    d. LM & CM docking
    
    e. Earthbound Navigation
    
    
Each of the stages are being devloped in reverse, with each sub-stage developed in no particular order. Once stage 3 is
complete, automation scripts will be written and tested to complete & optimize each sub-stage. Then stage 2 will
be completed and automation techniques implemented. So on, until stages 1-3 can all be automated & optimized. Each
stage can be played like simple a mini-game by a user, or run together for a rudimentary lunar mission simulator.

Without publishing the simulator as a standalone application, various parameters must be set in Unity in order to run the
program. Once stage 3 is sufficiently established these parameters will be published, along with a standalone application.
(Updated comments 08/23/2019)

Controller2D script, is derived from [Sebastian Lague's identically named script found here.](https://github.com/SebLague/2DPlatformer-Tutorial)

