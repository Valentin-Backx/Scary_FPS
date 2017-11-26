using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Door : MonoBehaviour,IInteractable {

    Action currentBehavior = () => { };

    public Renderer doorRenderer;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            currentBehavior = PlayerInZone;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentBehavior = () => { };

        }
    }

    bool _currentlyDisplayed;
    public Collider doorCollider;
    

    void PlayerInZone()
    {
        if(!_currentlyDisplayed&&InteractableDetector.CurrentlyDetected==doorCollider)
        {
            InteractableDoorUI.Instance.DisplayDoor();
            _currentlyDisplayed = true;
            Interactor.SetInteract(this);
            return;
        }
        if(_currentlyDisplayed&&!InteractableDetector.CurrentlyDetected == doorCollider)
        {
            InteractableDoorUI.Instance.HideDoor();
            _currentlyDisplayed = false;
            Interactor.SetInteract(null);
        }
    }



    void Update () {
        currentBehavior();	
	}

    public HingeJoint joint;

    public float doorClosedTarget = -70f;

    public void Activate(bool t)
    {
        //JointSpring spring = new JointSpring();
        //spring.spring = joint.spring.spring;
        //spring.damper = joint.spring.damper;
        //spring.targetPosition = doorClosedTarget;

        //joint.spring=spring;
        joint.useMotor = true;

    }
}
