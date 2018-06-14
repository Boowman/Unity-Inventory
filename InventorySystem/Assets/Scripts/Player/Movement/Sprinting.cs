using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sprinting : MonoBehaviour
{
    //Get a reference of the current script
    public static Sprinting SprintScript;

    public Text SprintText;

    //The speed the player will run with
    public float runSpeed = 10;

    //The minimum sprint value after after going down to 0 from 100
    public float minSprintValue = 20f;

    //The starting sprint value
    public float startSpringValue = 100f;

    //The stop value when he won't be able to run anymore
    public float stopSprintValue = 0.0f;

    //The sprint remaining
    public float SprintValue = 100f;

    //The amount the sprint will be reduce when holding the sprint key down
    public float sprintTransitionValue = 0.1f;

    //The recovery amount when he isn't running anymore
    public float sprintRecoveryValue = 0.5f;

    //Checking when he can start recovering
    public bool startRecovering = false;

    //Checking if he is allowed to run
    public bool allowRunning = true;

    //Checking if he is running
    public bool isRunning = false;

    //Displaying the sprint on the screen
    Text sprintText;

    //Creating a timerFor the reacharging
    public float timeBeforeRecovering = 3f;

    //Defining the PlayerMovement script
    PlayerMovement playerMovement;

    void Start()
    {
        //Accessing the script
        playerMovement = FindObjectOfType<PlayerMovement>();

        //Setting the sprint value for safety
        SprintValue = startSpringValue;
    }

    void Update()
    {
        SprintText.text = Mathf.Round(SprintValue).ToString();
        Sprint(Input.GetAxis("Sprint"));
    }

    public int IncreaseStamina
    {
        get { return (int)SprintValue; }
        set { if (SprintValue < 100) SprintValue += value;; }
    }

    //Creating the sprint function
    public void Sprint(float sprintKey)
    {
        if (allowRunning == true)
        {
            if (sprintKey != 0)
            {

                if (transform.position.x != 0 || transform.position.z != 0 && SprintValue > 0)
                {
                    if (playerMovement.isGrounded== false)
                    {
                        playerMovement.speed = runSpeed;
                        SprintValue -= sprintTransitionValue;
                        isRunning = true;
                    }

                    playerMovement.speed = runSpeed;
                    SprintValue -= sprintTransitionValue;
                    isRunning = true;

                    if (SprintValue <= 0)
                    {
                        sprintKey = 0;
                        isRunning = false;
                        allowRunning = false;
                    }
                }
            }
            else
            {
                isRunning = false;
            }
        }

        if (SprintValue < startSpringValue && SprintValue > stopSprintValue)
        {
            if (isRunning == false)
            {
                StartCoroutine("WaitSprintRecovery", timeBeforeRecovering - 1.5f);
            }

            if (isRunning == true)
            {
                StopCoroutine("WaitSprintRecovery");
                startRecovering = false;
            }
        }

        if (allowRunning != true)
        {
            sprintKey = 0;
        }

        if (sprintKey == 0 && isRunning == false)
        {
            playerMovement.speed = playerMovement.defaultSpeed;

            if (SprintValue <= stopSprintValue)
            {
                SprintValue = stopSprintValue;
                StartCoroutine("WaitSprintRecovery", timeBeforeRecovering);
            }

            if (startRecovering == true)
            {
                SprintValue += sprintRecoveryValue;

                if (SprintValue >= startSpringValue)
                {
                    SprintValue = startSpringValue;
                    startRecovering = false;
                }

                if (SprintValue >= minSprintValue)
                {
                    allowRunning = true;
                }
            }
        }
    }

    //The Enumerator that will enable the recovery after 3 seconds
    IEnumerator WaitSprintRecovery(float time)
    {
        yield return new WaitForSeconds(time);
        startRecovering = true;
    }
}