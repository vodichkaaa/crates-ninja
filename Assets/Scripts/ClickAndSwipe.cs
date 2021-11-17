using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]

public class ClickAndSwipe : MonoBehaviour
{
    private GameManager _gameManager;
    private Camera _cam;

    private Vector3 _mousePos;

    private TrailRenderer _trail;
    private BoxCollider _col;

    private bool _isSwiping = false;

    
    void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
        _col = GetComponent<BoxCollider>();

        _trail.enabled = false;
        _col.enabled = false;

        _cam = Camera.main;

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (_gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isSwiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isSwiping = false;
                UpdateComponents();
            }
            if (_isSwiping)
            {
                UpdateMousePosition();
            }
        }
    }


    private void UpdateMousePosition()
    {
        _mousePos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = _mousePos;
    }

    private void UpdateComponents()
    {
        _trail.enabled = _isSwiping;
        _col.enabled = _isSwiping;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Target>())
        {
            //Destroy the target
            other.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }
}
