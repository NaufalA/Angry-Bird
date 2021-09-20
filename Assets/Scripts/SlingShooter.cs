using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShooter : MonoBehaviour
{
    public CircleCollider2D collider;
    private Vector2 _startPos;

    [SerializeField] private float radius = 0.75f;
    [SerializeField] private float throwSpeed = 30f;

    private Bird _bird;
    private void Start()
    {
        _startPos = transform.position;
    }

    private void OnMouseUp()
    {
        collider.enabled = false;
        Vector2 velocity = _startPos - (Vector2) transform.position;
        float distance = Vector2.Distance(_startPos, transform.position);
        
        _bird.Shoot(velocity, distance, throwSpeed);

        gameObject.transform.position = _startPos;
    }

    private void OnMouseDrag()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = point - _startPos;
        if (direction.sqrMagnitude > radius)
        {
            direction = direction.normalized * radius;
        }

        transform.position = _startPos + direction;
    }

    public void InstantiateBird(Bird bird)
    {
        _bird = bird;
        _bird.MoveTo(transform.position, gameObject);
        collider.enabled = true;
    }
}
