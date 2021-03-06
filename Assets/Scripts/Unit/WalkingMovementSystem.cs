﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class WalkingMovementSystem : ComponentSystem
{
    /// <summary>
    /// Components
    /// </summary>
    struct Components
    {
        public Transform transform;
        public Rigidbody rigidbody;
        public WalkingMovementData movement;
        public Animator animator;
        public SpriteRenderer sprite;
    }
    
    /// <summary>
    /// Update
    /// </summary>
	protected override void OnUpdate ()
    {
        float deltaTime = Time.deltaTime;

        foreach(var entity in GetEntities<Components>())
        {
            UpdateWalking(entity.transform, entity.rigidbody, entity.movement, entity.animator, entity.sprite, deltaTime);
        }
	}

    /// <summary>
    /// Walk.
    /// </summary>
    public void UpdateWalking(
        Transform transform,
        Rigidbody rigidbody,
        WalkingMovementData movement,
        Animator animator,
        SpriteRenderer sprite,
        float deltaTime)
    {

        // Handle walking.
        if (movement.CanWalk && movement.Walking)
        {
            rigidbody.MovePosition(transform.position + (movement.CurrentDirection * movement.CurrentWalkSpeed) * deltaTime);
        }

        // Flip the sprite if needed.
        sprite.flipX = movement.CurrentDirection.x < 0;

        // Toggle between resting and walking.
        movement.CurrentTimer -= deltaTime;
        if (movement.CurrentTimer <= 0)
        {
            movement.Walking = !movement.Walking;
            animator.SetTrigger(movement.Walking ? "Run" : "Dance");

            // Set the new random values.
            SetRandomValues(movement);
        }
    }

    /// <summary>
    /// Set random values for walking.
    /// </summary>
    private void SetRandomValues(WalkingMovementData movement)
    {
        // Set the timer.
        movement.CurrentTimer = movement.Walking ? Random.Range(movement.MinWalkTime, movement.MaxWalkTime) : Random.Range(movement.MinRestTime, movement.MaxRestTime);

        // Set the direction.
        float angle = Random.Range(movement.MinAngle, movement.MaxAngle);
        movement.CurrentDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

        // Set the speed.
        movement.CurrentWalkSpeed = Random.Range(movement.MinWalkSpeed, movement.MaxWalkSpeed);
    }
}
