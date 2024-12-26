using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI scoreText;
    public GameObject winText; 
    
    private Rigidbody _rb;
    private float _movementX;
    private float _movementY;
    private int _count = 0;

    void Start() {
        setCountText();
        winText.SetActive(false);
        _rb = GetComponent<Rigidbody>();
    }
    void OnMove(InputValue movementValue) {
        Vector2 movementVec = movementValue.Get<Vector2>();
        _movementX = movementVec.x;
        _movementY = movementVec.y;
    }
    void FixedUpdate() { 
        Vector3 movement = new Vector3(_movementX, 0.0f, _movementY); 
        _rb.AddForce(movement * speed);       
    }
    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("PickUp")) return;
        _count++;
        setCountText();
        other.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            winText.SetActive(true);
            winText.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            Destroy(gameObject);
        };
    }

    void setCountText() {
        scoreText.text = "Count: " + _count.ToString();
        if (_count >= 8) {
            winText.SetActive(true);
            Destroy(GameObject.FindWithTag("Enemy"));
        }
    }
}
