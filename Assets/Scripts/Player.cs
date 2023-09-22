using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    Dash,
    Jump,
}
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    private float scrollSpeed = 8f;
    private float jumpPower = 12f;
    private float gravityScale = -15f;
    private float gliderDragScale = 0f;
    private Rigidbody rb = null;

    private PlayerState state = PlayerState.Dash;
    private int jumpCount = 0;
    private int maxJumpCount = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        camera.transform.position = new Vector3(transform.position.x, transform.position.y + 1, -15f);
        rb.AddForce(0, gravityScale + gliderDragScale, 0, ForceMode.Acceleration);  //落下速度を計算
        rb.velocity = new Vector3(scrollSpeed , rb.velocity.y, rb.velocity.z);//x方向移動

    }

    private void Update()
    {

        Jump();
        Glide();
    }

    private void Jump()
    {
        if (jumpCount < maxJumpCount && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("ジャンプ");
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            jumpCount++;
        }
    }

    private void Glide()
    {
        //ジャンプ中
        if (jumpCount>0 && Input.GetKey(KeyCode.Space))
        {
            //rb.drag = 50f;
            if(rb.velocity.y < 0)
            {
                //gliderDragScale = 12f;
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y/1.5f, rb.velocity.z);
            }
        }
        else
        {
            gliderDragScale = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //ジャンプ中
        if (jumpCount > 0)
        {
            //床に接所したなら
            if (collision.gameObject.CompareTag("Floor"))
            {
                jumpCount = 0;
            }
        }

        if (collision.gameObject.CompareTag("TurnWall")){
            scrollSpeed = -scrollSpeed;
            Debug.Log(scrollSpeed);
        }
    }
}
