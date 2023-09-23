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
    private Animator animator;
    public UIManager uIManager;
    private float scrollSpeed = 8f;
    private float jumpPower = 12f;
    private float gravityScale = -15f;
    private Rigidbody rb = null;
    private float fuelAmount;
    private const float jumpFuelAmount = 40f;
    private const float glideFuelAmount = 15f;

    private PlayerState state = PlayerState.Dash;
    private bool isJump = false;
    private bool isDashing = true;    //�i�ނ��̃t���O

    /* ��������v���p�e�B */
    public float FuelAmount
    {
        get => fuelAmount;
        set
        {
            fuelAmount = value;
            uIManager.updateUI(UI.FuelBar, value);
        }
    }


    private void Start()
    {
        FuelAmount = 100f;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
    }
    private void FixedUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 1, -15f);
        rb.AddForce(0, gravityScale, 0, ForceMode.Acceleration);  //�������x���v�Z
        if(isDashing) rb.velocity = new Vector3(scrollSpeed , rb.velocity.y, rb.velocity.z);//x�����ړ�
        isDashing = true;

    }

    private void Update()
    {
        Jump();
        Glide();
    }

    private void Jump()
    {

        if (jumpFuelAmount <= FuelAmount && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("�W�����v");
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            animator.SetTrigger("jump");
            FuelAmount -= jumpFuelAmount;
            isJump = true;
        }
    }

    private void Glide()
    {
        //�W�����v��
        if (isJump && Input.GetKey(KeyCode.Space))
        {
            if(rb.velocity.y < 0 && FuelAmount >0f)
            {
                animator.SetBool("isGlide", true);
                FuelAmount -= glideFuelAmount*Time.deltaTime;
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y/1.1f, rb.velocity.z);
            }
        }
        if(!Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("isGlide",false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject.CompareTag("Player"))
        {
            Debug.Log("true");
        }

        if (collision.gameObject.CompareTag("ToRight"))
        { 
            scrollSpeed = Mathf.Abs(scrollSpeed);
            transform.rotation = new Quaternion(0, 180f, 0, 0);
        }

        if (collision.gameObject.CompareTag("ToLeft"))
        {
            scrollSpeed = -Mathf.Abs(scrollSpeed);
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //�ǂ̕��ɓ����������i�s���~�߂鏈��
        if (isDashing)
        {
            if (collision.gameObject.CompareTag("FloorSide"))
            {
                isDashing = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        //�W�����v��
        if (isJump)
        {
            //���ɐڏ������Ȃ�
            if (other.gameObject.CompareTag("Floor"))
            {
                isJump = false;
                FuelAmount = 100f;
            }
        }

    }
}
