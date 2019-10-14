# SALTE_Unity
This is the VR listening test interface of SALTE framwork.
Built with Unity 2019.2.3f1.

**This part of the framework is still under development.** <br/>
**We are aiming to publish version 1.0 early next year (Please Watch out project for latest update).** <br/>
**We would like to have your feedback and contribution to this project.** <br/>


# Motivation
**1. Building VR listening tests without prior knowledge of VR game engines (e.g. Unity or Unreal)** <br/>
- No prior knowledge in Unity or C# code
- Drag and drop interface
- Well documented with templates and tutorial available

**2. Open Source** <br/>
- Know the back-end
- Flexible (easy to modify)
- Everyone is welcome to contribute

**3. Standardise work-flow** <br/>
- Standardise data
- Stable and easy to deploy
- Repeatable test = Good science
- Comparable results
- Easy to expand the scale of the test

**4. Share tests and results** <br/>
- One person's noise is another person's data
- No need to build or repeat similar tests
- Share test easily (with a single .JSON file)
- Big data era for machine learning

**5. Easy to use and ergonomic interface** <br/>
- Intuitive design (reduce training time)
- Improve test speed
- Better interface = longer test

# Demo
**Make sure you have setup your VR headset.** <br/>
(The interface have been tested with **Oculus Rift** and **Oculus Rift S.** )

**Open demo scene**
1. Download and start the [SALTE renderer](https://github.com/AudioLabYork/SALTE-audio-renderer)
2. Open Unity (make sure it is on Unity 2019.2.3f1)
3. Load the SALTE-VR-interface/Listening_TESTS_VR/Assets/Scenes/**NYC.unity** scene
4. Press the **start** button in the renderer
5. Have fun

**Control**
- You will see a ray cast from the right hand controller.
- Index trigger to “press” any button.
- Move the joystick up or down to adjust the slider.
- More control options will come later.

# Builder (Beta)
**Key features**
1. Create a .JSON that consolidate all the data for the listening test
2. For people who are not familiar with coding or editing .JSON file
3. Drag and drop interface
4. Add flexibility (can change background, interfave and control)
5. A safer way to create listening test

# Future work 
**1. Video tutorials** <br/>
**2. Localisation tests** <br/>
3. 360 videos playback <br/>
4. 6 degrees of freedom (6 DoF) <br/>
5. More options for the VR interface and control method <br/>

**1** *and* **2** *will be included in version 1.0 early next year.*

# Sneak peek: Localsation test
- Demoed in the 2019 AES International Conference in Immersive and Interactive audio (March 27-29, 2019) Localisation test with head pointing
- Game like interface (easy to embed into game in the future)
- Competitive gaming experience 
- Externalisation response (optional)
- Researchers can collect participants localisation externalisation response with head movement data

**Head pointing localisation test demo (Demoed in AES IIA 2019)** <br/>
![Localisation test demo (head pointing method)](https://github.com/AudioLabYork/SALTE-VR-interface/blob/master/head_pointing.gif) <br/>
The right-hand side is the participant's view in VR.
The left-hand side is of researchers fo observe the participant's response, the purple dot is the sound source location which is hidden in VR. 

**Hand pointing method with head tracked grid (New)** <br/>
![Localisation test demo (hand pointing method)](https://github.com/AudioLabYork/SALTE-VR-interface/blob/master/hand_pointing.gif) <br/>

# Our poster presented in AES New York Convention 2019
![SALTE Pt. 1: A Virtual Reality Tool for Streamlined and Standardised Spatial Audio Listening Tests (e-Brief 536)](https://github.com/AudioLabYork/SALTE-VR-interface/blob/master/AES_NY_poster_landscape_2019%20copy.jpg)
