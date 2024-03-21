using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;

    // public TextMeshProUGUI timerText;

    public GameObject winTextObject;

    public GameObject loseTextObject;

    public GameObject restartButton;

    public SpriteRenderer backgroundSpriteRenderer;

    public GameObject menuButton;

    public GameObject enemy;

    public GameObject gameManager;

    private float timeRemaining;

    // public GameObject cronometroTextObject;

    public float jumpForce = 1.0f;

    private bool isGrounded;
    private int count;

    private bool gameOver = false;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // private void IniciarCronometro()
    // {
    //     cronometroTextObject.GetComponent<Cronometro>().IniciarCronometro();
    // }

    // private void PararCronometro()
    // {
    //     cronometroTextObject.GetComponent<Cronometro>().PararCronometro();
    // }

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        // SetTimerText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        restartButton.SetActive(false);
        menuButton.SetActive(false);
        // IniciarCronometro();

        // Atualize o cronômetro a cada segundo
        // InvokeRepeating("SetTimerText", 1f, 1f);
        
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    // Update is called once per frame
    // void Update()
    // {
    //     SetTimerText();
    // }


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        // Debug.Log("Move: " + movementVector);

        movementX = movementVector.x;
        movementY = movementVector.y;
    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12 && !gameOver)
        {
            gameManager.GetComponent<TimeManager>().SetActiveFalse();
            winTextObject.SetActive(true);
            gameManager.SetActive(false);
            restartButton.SetActive(true);
            menuButton.SetActive(true);
            enemy.SetActive(false);

            // PararCronometro();
        }
        
    }

    void SetLoseText()
    {
        loseTextObject.SetActive(true);
        restartButton.SetActive(true);
        menuButton.SetActive(true);
    }

    // void SetTimerText()
    // {
    //     timerText.text = "Time: " + cronometroTextObject.GetComponent<Cronometro>().TempoDecorrido().ToString("F2");
    // }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if (isGrounded && Keyboard.current.spaceKey.isPressed && rb.position.y < 1.0f)
        {
            rb.AddForce(Vector3.up * jumpForce / 2.0f, ForceMode.Impulse);
            isGrounded = false;
        }

        // SetTimerText();
    }

    void Update()
    {
        // Acessar atributos e métodos do meu GameObject
        timeRemaining = gameManager.GetComponent<TimeManager>().timeRemaining;

        // Debug.Log("timeRemaining: " + timeRemaining.ToString());
        if (timeRemaining <= 0 || backgroundSpriteRenderer.color == Color.green)
        {
            gameManager.GetComponent<TimeManager>().SetActiveFalse();
            loseTextObject.SetActive(true);
            restartButton.SetActive(true);
            menuButton.SetActive(true);
            enemy.SetActive(false);
            // Set active false to player
            gameOver = true;
            // PararCronometro();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            count++;
            other.gameObject.SetActive(false);
            SetCountText();
        }
    }
}
