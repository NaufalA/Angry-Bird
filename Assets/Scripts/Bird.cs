using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    public enum BirdState
    {
        Idle,
        Thrown
    };

    public GameObject parent;
    public Rigidbody2D rigidBody;
    public CircleCollider2D circleCollider;

    private BirdState _state;
    private float _minVelocity = 0.05f;
    private bool _flagDestroy = false;
    
    public UnityAction OnBirdDestroyed = delegate {  };

    private void Start()
    {
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
        circleCollider.enabled = false;
        _state = BirdState.Idle;
    }

    private void FixedUpdate()
    {
        if (_state == BirdState.Idle && rigidBody.velocity.sqrMagnitude >= _minVelocity)
        {
            _state = BirdState.Thrown;
        }

        if (_state == BirdState.Thrown && rigidBody.velocity.sqrMagnitude < _minVelocity && !_flagDestroy)
        {
            _flagDestroy = true;
            StartCoroutine(DestroyAfter(2f));
        }
    }

    private void OnDestroy()
    {
        OnBirdDestroyed();
    }

    private IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    public void MoveTo(Vector2 target, GameObject parentObject)
    {
        GameObject o = gameObject;
        o.transform.SetParent(parentObject.transform);
        o.transform.position = target;
    }

    public void Shoot(Vector2 velocity, float distance, float speed)
    {
        circleCollider.enabled = true;
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        rigidBody.velocity = velocity * speed * distance;
    }
}
