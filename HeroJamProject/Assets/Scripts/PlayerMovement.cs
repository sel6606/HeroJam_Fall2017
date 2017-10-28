using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    #region Variables
    public float speed;
    private CharacterController character;
    private Vector3 movementDirection;

    float horizontalView;
    float verticalView;

    #endregion

    // Use this for initialization
    
    void Start ()
    {
        character = GetComponent<CharacterController>();
        horizontalView = 0;
        verticalView = 0;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (character.isGrounded)
        {
            movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            movementDirection = transform.TransformDirection(movementDirection);
            movementDirection *= speed;
        }

        movementDirection.y -= 9.8f * Time.deltaTime;
        character.Move(movementDirection * Time.deltaTime);

        PlayerRotation();
	}

    private void PlayerRotation()
    {
        horizontalView = Input.GetAxis("Mouse X") * 6;
        verticalView -= Input.GetAxis("Mouse Y") * 6;

        verticalView = Mathf.Clamp(verticalView, -80f, 80f);

        character.transform.Rotate(0, horizontalView, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalView, 0, 0);
    }
}
