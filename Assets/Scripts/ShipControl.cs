using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class ShipControl : MonoBehaviour {
  [Header("Configuration")]
  public float speed = 8;
  public InputAction motion;
  public float turbinePower = 8.5f;
  public float smoothTime = 1;

  [Header("Information")]
  public Vector3 currentVelocity;
  public Vector2 input;

  [Header("Initialization")]
  public Transform pivot;
  public Transform theActualShip;

  void OnEnable () {
    motion.Enable();
  }

  void Update () {
    input = motion.ReadValue<Vector2>();
    MoveThePivot();
    MoveTheShip();
  }

  void MoveThePivot () {
    Vector3 pos = pivot.position + speed * Time.deltaTime *
      (input.x * pivot.right + input.y * pivot.forward);
    RaycastHit hit;
    Vector3 g = (Vector3.zero - pos).normalized;
    Physics.Raycast(pos, g, out hit, Mathf.Infinity, LayerMask.GetMask("Planet"));
    pos = hit.point - g * turbinePower;

    pivot.up = -g;
    pivot.position = pos;
  }

  void MoveTheShip () {
    theActualShip.position = Vector3.SmoothDamp(theActualShip.position, pivot.position,
                                                ref currentVelocity, smoothTime, speed);
    theActualShip.rotation = pivot.rotation;
  }
}
