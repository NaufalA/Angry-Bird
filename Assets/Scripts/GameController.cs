using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter slingShooter;
    public List<Bird> birds;

    private void Start()
    {
        slingShooter.InstantiateBird(birds[0]);

        for (int i = 0; i < birds.Count; i++)
        {
            birds[i].OnBirdDestroyed += ChangeBird;
        }
    }

    public void ChangeBird()
    {
        birds.RemoveAt(0);

        if (birds.Count > 0)
        {
            slingShooter.InstantiateBird(birds[0]);
        }
    }
}
