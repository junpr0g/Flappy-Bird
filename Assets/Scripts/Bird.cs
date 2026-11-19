using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    public float jumpForce = 6f;
    public Text scoreText;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public Text finalScoreText;

    private Rigidbody2D rb;
    private int score;
    private bool isStarted;
    private bool isDead;

    private void Start()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        if (isDead) return;

        bool jumpPressed =
            (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) ||
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame);

        if (jumpPressed)
        {
            if (!isStarted)
            {
                isStarted = true;
                rb.gravityScale = 2f;
            }
            rb.linearVelocity = Vector2.up * jumpForce;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.gameObject.name == "ScoreZone")
        {
            score++;
            if (scoreText != null)
            {
                scoreText.text = score.ToString();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        isDead = true;
        Time.timeScale = 0f;

        if (gameOverText != null)
        {
            gameOverText.text = collision.gameObject.name == "Ceiling" ? "BONK" : "GAME OVER";
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "Score: " + score;
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
