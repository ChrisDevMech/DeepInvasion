using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

//Player Input -> messages
public class PlayerController : MonoBehaviour {
    #region Controller Actions
    //EVENTO (DELEGADO)   --> disparar
    public delegate void PlayerFire();
    public static event PlayerFire onPlayerFire;  //(EVENTO)
    //EVENTO (DELEGADO)   --> Pausar
    public delegate void Pause();
    public static event Pause onPause;  //(EVENTO)

    [Header("Movement")]
    //[SerializeField] float speed;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float forceMagnitude = 300f;
    [SerializeField] float accuracy = 10f;

    private Rigidbody2D rb;
    private float lastValidAngle = 0f; // Almacena la última rotación válida

    private float horizontal;
    private float vertical;
    #endregion Controller Actions

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        // Visualizar el rayo en la escena
        Debug.DrawRay(transform.position, new Vector3(horizontal, vertical, 0) * 2f, Color.green);
    }

    #region Input Handler
    public void OnMove(InputValue value) {
        Debug.Log("Move");
        Vector2 movement = value.Get<Vector2>();
        horizontal = movement.x;
        vertical = movement.y;
        //Debug.Log($"Walk{movement}");
    }
    public void OnFire() {
        Debug.Log("Fire");
        // Evento Disparar
        if (onPlayerFire != null) {
            onPlayerFire();
        }
        AudioManager.instance.PlaySFX("BulletShootEmpty");
    }
    public void OnPause() {
        // Evento Pausar
        if (onPause != null)
            onPause();
        //Debug.Log("Pause");
    }
    #endregion

    #region Controller Actions
    private void FixedUpdate() {
        // Movimiento
        if (horizontal != 0 || vertical != 0) {

            // Calcular ángulo de rotación
            float targetAngle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
            // Ajustar el rango de ángulos a 0-360 grados
            targetAngle = (targetAngle + 360) % 360;

            // Rotar hacia el ángulo calculado con velocidad constante
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, targetAngle), rotationSpeed * Time.fixedDeltaTime);

            // Calcular la diferencia angular
            float angleDifference = Mathf.Abs(transform.eulerAngles.z - targetAngle);
            // Ajustar la diferencia angular al rango de 0-180 grados
            angleDifference = Mathf.Min(angleDifference, 360 - angleDifference);

            // Almacenar la �ltima rotación válida
            lastValidAngle = transform.eulerAngles.z;

            //Debug.Log("angleDifference: " + angleDifference);

            // Aplicar fuerza si la diferencia angular es pequeña
            if (angleDifference < accuracy) {
                Vector2 forceDirection = new Vector2(horizontal, vertical).normalized;
                rb.AddForce(forceDirection * forceMagnitude * Time.fixedDeltaTime);
            }
        } else {
            // Si no hay entrada del joystick, mantener la última rotación válida
            transform.rotation = Quaternion.Euler(0, 0, lastValidAngle);
        }
    }
    #endregion Controller Actions
}
