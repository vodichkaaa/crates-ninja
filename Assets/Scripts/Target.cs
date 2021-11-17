using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private Rigidbody _playerRb;
    private GameManager _gameManager;
    public ParticleSystem explosionParticle;

    [SerializeField]
    private float minSpeed = 15f;
    [SerializeField]
    private float maxSpeed = 20f;

    [SerializeField]
    private float torque = 10f;

    [SerializeField]
    private float xStartPos = 4f;
    [SerializeField]
    private float yStartPos = -6f;

    public int pointValue;

    // Start is called before the first frame update
    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        _playerRb.AddForce(RandomForce(), ForceMode.Impulse);
        _playerRb.AddTorque(RandomRotation(), RandomRotation(), RandomRotation(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xStartPos, xStartPos), yStartPos);
    }

    private float RandomRotation()
    {
        return Random.Range(-torque, torque);
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private void OnMouseDown()
    {
        if(_gameManager.isGameActive)
        {
            _gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sensor"))
        {
            Destroy(gameObject);
            if (!gameObject.CompareTag("Bad"))
            {
                _gameManager.health--;
            }
            
        }
    }

    public void DestroyTarget()
    {
        if (_gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            _gameManager.UpdateScore(pointValue);
        }
    }
}
