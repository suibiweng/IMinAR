using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class MLControllerButton : MonoBehaviour
{
     public MLInput.Controller _controller;
    public GameObject _cube;
    private MLControllerConnectionHandlerBehavior _controllerConnectionHandler = null;
    #region Private Variables

    private Quaternion _originalOrientation;
    private Vector3 _rotation = new Vector3(0, 0, 0);
    private const float _rotationSpeed = 30.0f;

    #endregion

    #region Unity Methods
    /// <summary>
    /// Initializes the _cube GameObject and _controller object
    /// Captures the Cube's original rotation quaternion in _originalOrientation
    /// Starts receiving user input from the Control and sets up event handlers for input
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
         //_cube = GameObject.Find("Cube");
       // _originalOrientation = _cube.transform.rotation;
        /*
        MLInput.Start();
        MLInput.OnControllerButtonDown += OnButtonDown;
        MLInput.OnControllerButtonUp += OnButtonUp;
    */
        _controllerConnectionHandler= GetComponent<MLControllerConnectionHandlerBehavior>();
    }

    /// <summary>
    /// Cleans up event handlers and stops receiving input
    /// </summary>
    void OnDestroy()
    {
        MLInput.OnControllerButtonDown -= OnButtonDown;
        MLInput.OnControllerButtonUp -= OnButtonUp;
        MLInput.Stop();
    }

    /// <summary>
    /// Rotates the cube by the _rotation vector at _rotationSpeed speed
    /// Checks the state of the Trigger button
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        //  _cube.transform.Rotate(_rotation, _rotationSpeed * Time.deltaTime);
        //  CheckTrigger();

        UpdateTrigger();
    }
    #endregion

    #region Private Methods



    private void UpdateTrigger()
    {
        if (!_controllerConnectionHandler.IsControllerValid())
        {
            return;
        }

        MLInput.Controller controller = _controllerConnectionHandler.ConnectedController;
        if (controller.TriggerValue > 0.2f) {


            Touch();
        }

    }


    /// <summary>
    /// Updates the _rotation's y component to 90 when the Bumper button is pressed down
    /// </summary>
    /// <param name="controllerId"></param>
    /// <param name="button"></param>
    void OnButtonDown(byte controllerId, MLInput.Controller.Button button)
    {
        if (button == MLInput.Controller.Button.Bumper)
        {
            _rotation.y = 90;
        }
    }

    /// <summary>
    /// Updates the _rotation's y component to 0 when the Bumper button is released
    /// Resets the Cube's orientation to its original orientation, when the Home Button is pressed and released
    /// </summary>
    /// <param name="controllerId"></param>
    /// <param name="button"></param>
    void OnButtonUp(byte controllerId, MLInput.Controller.Button button)
    {
        if (button == MLInput.Controller.Button.Bumper)
        {
            _rotation.y = 0;
        }
        if (button == MLInput.Controller.Button.HomeTap)
        {
     //      _cube.transform.rotation = _originalOrientation;
        }
    }

    /// <summary>
    /// Updates the _rotation's x component to 90 when the Trigger is pressed down more than 20%
    /// Otherwise the _rotation's x component is set to 0
    /// </summary>
    void CheckTrigger()
    {
        if (_controller.TriggerValue > 0.2f)
        {

            Touch();
            _rotation = new Vector3(0, -10, 0);

        }
        else
        {
            
        }
    }


    void Touch()
    {

       

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            hit.collider.SendMessage("Select", null, SendMessageOptions.DontRequireReceiver);
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        else
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }



    
    }

    #endregion
}