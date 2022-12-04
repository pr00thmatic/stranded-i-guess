using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour {
  [Header("Configuration")]
  public Vector3 currentVelocity;
  public float smoothTime = 0.5f;
  public float angularSpeed = 90;
  public InputAction switchMode;

  [Header("Information")]
  public Transform current;

  [Header("Initialization")]
  public Transform exploring;
  public Transform landing;
  public new Camera camera;

  void Awake () {
    current = exploring;
  }

  void OnEnable () {
    switchMode.Enable();
    switchMode.performed += ChangeMode;
  }

  void LateUpdate () {
    camera.transform.position = Vector3.SmoothDamp(camera.transform.position, current.position,
                                                   ref currentVelocity, smoothTime);
    camera.transform.rotation = Quaternion.RotateTowards(camera.transform.rotation, current.rotation,
                                                         angularSpeed * Time.deltaTime);
  }

  public void ChangeMode (InputAction.CallbackContext ctx) {
    current = current == landing? exploring: landing;
  }
}
