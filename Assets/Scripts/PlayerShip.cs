using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerShip : MonoBehaviour
{

    [SerializeField] float movementSpeed = 100f;
    [SerializeField] float yMovementRange = 12f;
    [SerializeField] float zMovementRange = 18f;
    [SerializeField] float xRotationFactorDueToPos = 1.5f;
    [SerializeField] float yRotationFactorDueToPos = 1f;

    
    [SerializeField] float xRotationFactorDueToInput = 20f;
    [SerializeField] float zRotationFactorDueToInput = 40f;

    [SerializeField] GameObject[] lazers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void ProcessInputs()
    {

        float zInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        MoveShip(zInput, yInput);
        RotateShip(zInput, yInput);

        ProcessLazersInput();
    }

    void MoveShip(float zInput, float yInput)
    {


        float zMovement = zInput * Time.deltaTime * movementSpeed;
        float newZ = Mathf.Clamp(transform.localPosition.z + zMovement, -zMovementRange, zMovementRange);

        float yMovement = yInput * Time.deltaTime * movementSpeed;
        float newY = Mathf.Clamp(transform.localPosition.y + yMovement, -yMovementRange, yMovementRange);

        transform.localPosition = new Vector3(transform.localPosition.x, newY, newZ);
    }

    void RotateShip(float zInput, float yInput)
    {
        float xRotationDueToInput = -yInput * xRotationFactorDueToInput;
        float xRotationDueToPos = -transform.localPosition.y * xRotationFactorDueToPos;
        float xRotation = xRotationDueToInput + xRotationDueToPos;

        float yRotationDueToPos = transform.localPosition.z * yRotationFactorDueToPos;

        float zRotationDueToInput = -zInput * zRotationFactorDueToInput;

        transform.localRotation = Quaternion.Euler(xRotation, 270 + yRotationDueToPos, zRotationDueToInput);
    }

    void ProcessLazersInput()
    {
        if (Input.GetButton("Fire1"))
        {
            print("FFFFFF");
            foreach (GameObject lazer in lazers)
            {
                var emission = lazer.GetComponent<ParticleSystem>().emission;
                emission.enabled = true;
            }
        }
        else
        {
            foreach (GameObject lazer in lazers)
            {
                var emission = lazer.GetComponent<ParticleSystem>().emission;
                emission.enabled = false;
            }
        }
    }
}
