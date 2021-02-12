using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Button rePlayButton;
    public AudioSource dingAudio;
    public AudioSource duangAudio;
    public AudioSource xiuAudio;
    public AudioSource collisionAudio;

    private Rigidbody rb;
    private int count;
    private Vector3 currentSize;

    void Start () 
    {
        rb = GetComponent<Rigidbody>();
        currentSize = transform.GetComponent<Renderer>().bounds.size;
        count = 0;
        winText.text = "";
        rePlayButton.gameObject.SetActive(false);
        SetCountText ();
    }

    void Update ()
    {
        if (transform.position.y < -20.0f) {
            Lose ();
        }
    }

    void FixedUpdate () 
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        // get Pick Up to get scores
        if (other.gameObject.CompareTag("Pick Up")) 
        {
            other.gameObject.SetActive (false);
            dingAudio.Play ();
            count = count + 1;
            SetCountText ();
        }

        // get Bigger Prop to become bigger
        if (other.gameObject.CompareTag("Bigger Prop"))
        {
            other.gameObject.SetActive (false);
            duangAudio.Play ();
            ExpandScale ();
        }

        // get Smaller Prop to become smaller
        if (other.gameObject.CompareTag("Smaller Prop"))
        {
            other.gameObject.SetActive (false);
            xiuAudio.Play ();
            ReduceScale ();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // lose when collide obstacle
        if (collision.collider.CompareTag("Obstacle"))
        {
            collisionAudio.Play ();
            Lose ();
        }
    }

    void SetCountText ()
    {
        countText.text = "To get 5 Scores to win the game!" + "\nScores: " + count.ToString ();
        if (count >= 5) {
            rePlayButton.gameObject.SetActive(true);
            winText.text = "You Win!";
        }
    }

    void ExpandScale ()
    {
        Vector3 newSize = new Vector3 (currentSize.x * 2, currentSize.y * 2, currentSize.z * 2);
        transform.localScale = newSize;
        currentSize = newSize;
    }

    void ReduceScale ()
    {
        Vector3 newSize = new Vector3 (currentSize.x * 0.5f, currentSize.y * 0.5f, currentSize.z * 0.5f);
        transform.localScale = newSize;
        currentSize = newSize;
    }

    void Lose ()
    {
        winText.text = "You Lose!";
        rePlayButton.gameObject.SetActive(true);
    }
}
