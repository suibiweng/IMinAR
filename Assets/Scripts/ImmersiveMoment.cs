using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
public class ImmersiveMoment : MonoBehaviour
{
    public bool isActive;
    public Transform OriGinpos;
   public  MLControllerConnectionHandlerBehavior _controllerConnectionHandler = null;
    float amt;
    // Start is called before the first frame update
    void Start()
    {
        _controllerConnectionHandler = GameObject.FindObjectOfType<MLControllerConnectionHandlerBehavior>();
    }

    void OnTriggr() {

        if (isActive) {

            MLInput.Controller controller = _controllerConnectionHandler.ConnectedController;
            if (controller.TriggerValue > 0.2f)
            {

                isActive = false;
            }


        }


    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.T)) {
            isActive = !isActive;

        }


        if (isActive)
        {

            amt += 0.01f;

        }
        else {


            amt -= 0.01f;

        }

        if (amt > 1) amt = 1;
        if (amt < 0) amt = 0;

        transform.position = Vector3.Lerp(OriGinpos.position, Camera.main.transform.position, amt);
        transform.localScale = Vector3.Lerp(new Vector3(2f, 2f, 2f), new Vector3(8f, 8f, 8f), amt);
        OnTriggr();
    }

    void Select() {

        isActive = true;


        if (isActive)
        {
            amt = 0f;

        }
        else {

            amt = 1f;
        }
        //Destroy(gameObject, 3);
        //GetComponent<Renderer>().material.color = Color.red;


    } 
}
