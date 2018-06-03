using System.Globalization;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Range(0,100)]
    // This will be the players health
    public int _health;

    // It will be used to check if the player is alive or not
    public bool _isAlive;

    public Text HealthText;

    /// <summary>
    /// The below variables are only going to be used for fall damage
    /// </summary>
    // We create a boolean to allow the player to set if he wants realistic fall damamge or not
    public bool RealisticDamage = true;

    //The amount of damage the player will take from based on the distance he fell from
    private int DamageTaken;

    // The minimum height he will not take any damage
    // We also have to consider that the player is 1 unit tall which makes it 4
    private float MinimumHeight = 4f;

    // The maximum height he will die instantly if he falls from
    private float MaximumHeight = 20f;

    // We use this to determine the distance from the player to the ground
    private float _fallDistance;

    // The number we will devide the distance from the player to the ground so we get the damage number
    // that we will apply to the player
    private float _damageDivisionNR = 0.3f;

    //Storing the players start position from the moment he starts falling
    private float _startPositionY;

    private Vector3 startPosition;

    //Defining some scripts so they can be used later on
    PlayerMotor _Player;
    PlayerMovement _PlayerMovement;

    void Awake()
    {
        _Player = GetComponent<PlayerMotor>();
        _PlayerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        //Apply the fall damage to the player
        ApplyFallDamage();

        //Checking if the player is alive or not
        if (_health > 0)
            _isAlive = true;
        else
            _isAlive = false;

        HealthText.text = "+ " + _health;

        if (_health <= 0)
        {
            Kill();
            Respawn();
        }
        
        if(_health >= 100)
            _health = 100;
    }

    public int GiveHealth
    {
        set {if (_health < 100)  _health += value; }
    }

    public int TakeHealth
    {
        set { if (_health > 0)  _health -= value; }
    }

    public void Kill()
    {
        _health = 0;
        _isAlive = false;
    }

    public void Respawn()
    {
        transform.position = startPosition;
        _health = 100;
        _isAlive = true;
    }

    public int GetFallDamage
    {
        get { return DamageTaken = (int)(_fallDistance / _damageDivisionNR) + (int)transform.localScale.y; }
    }

    public void ApplyFallDamage()
    {

        if (_startPositionY > transform.position.y)
        {
            _fallDistance += _startPositionY - transform.position.y;
        }

        _startPositionY = transform.position.y;
            
        if (_fallDistance >= MinimumHeight && _PlayerMovement.isGrounded)
        {
            _health -= GetFallDamage;

            _fallDistance = 0f;
            _startPositionY = 0f;
        }
        
        if (_fallDistance <= MinimumHeight && _PlayerMovement.isGrounded)
        {
            _fallDistance = 0f;
            _startPositionY = 0f;
        }
    }
}