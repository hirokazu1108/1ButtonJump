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
    private AudioManager audioManager;
    private Animator animator;
    public UIManager uIManager;
    public GameController gameController;
    private float scrollSpeed = 7.5f;
    private float jumpPower = 12f;
    private float boostPower = 20f;
    private float gravityScale = -15f;
    private Rigidbody rb = null;
    private float fuelAmount;
    private const float jumpFuelAmount = 40f;
    private const float glideFuelAmount = 8f;

    private bool isJump = false;
    private bool isDashing = true;    //�i�ނ��̃t���O

    private float boostTime = 00;   //�u�[�X�g����

    private Vector3 preVelocity;    //�|�[�Y�O�̑��x
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
    public float BoostTime
    {
        get => boostTime;
        set
        {
            boostTime = value;
            uIManager.updateUI(UI.BoostBar, value);
        }
    }


    private void Start()
    {
        var obj = GameObject.Find("AudioManager");
        if(obj != null) audioManager = obj.GetComponent<AudioManager>();

        FuelAmount = 100f;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
    }
    private void FixedUpdate()
    {

        //�����邩�̃t���O
        if (!GameController.canMove)
        {
            return;
        }

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 1, -15f);
        rb.AddForce(0, gravityScale, 0, ForceMode.Acceleration);  //�������x���v�Z
        if(isDashing) rb.velocity = new Vector3(scrollSpeed , rb.velocity.y, rb.velocity.z);//x�����ړ�
        isDashing = true;

    }

    private void Update()
    {
        
        //�����邩�̃t���O
        if (!GameController.canMove)
        {
            return;
        }
        //�u�[�X�g���[�h
        if (BoostTime > 0)
        {
            Boost();
        }
        else  //�ʏ탂�[�h
        {

            Jump();
            Glide();
        }
    }

    private void Jump()
    {
        
        if (jumpFuelAmount <= FuelAmount && Input.GetKeyDown(KeyCode.Space))
        {
            
            if (audioManager != null) audioManager.playSeOneShot(AudioKinds.SE_Boost);
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
                //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y/1.1f, rb.velocity.z);
                rb.AddForce(0, -rb.velocity.y, 0, ForceMode.Acceleration);  //�������x���v�Z
            }
        }
        if(!Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("isGlide",false);
        }
    }

    private void Boost()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (audioManager != null) audioManager.playSeOneShot(AudioKinds.SE_Boost);
            rb.AddForce(transform.up * boostPower, ForceMode.Impulse);
            BoostTime -= 20f;
            animator.SetTrigger("boost");
        }
        else
        {
            if (rb.velocity.y < 0)
            {
                //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 1.f, rb.velocity.z);
                rb.AddForce(0, -rb.velocity.y*1.2f, 0, ForceMode.Acceleration);  //�������x���v�Z
                BoostTime -= Time.deltaTime;
                animator.SetBool("isGlide", true);
            }

        }

        if (BoostTime <= 0)
        {
            BoostTime = 0f;
            FuelAmount = 100f;
            return;
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

        if (other.gameObject.CompareTag("Drink"))
        {
            if(audioManager != null) audioManager.playSeOneShot(AudioKinds.SE_Drink);
            BoostTime = 100f;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Goal"))
        {
            if (audioManager != null) audioManager.playSeOneShot(AudioKinds.SE_Goal);
            GameController.endState = EndState.Clear;
            StartCoroutine(gameController.finishGame());
        }

        if (other.gameObject.CompareTag("DeathBlock"))
        {
            if (audioManager != null) audioManager.playSeOneShot(AudioKinds.SE_Death);
            GameController.endState = EndState.Death;
            StartCoroutine(gameController.finishGame());
        }

    }

    public void stopMove()
    {
        GameController.canMove = false;
        preVelocity = rb.velocity;
        rb.velocity = Vector3.zero;
    }

    public void reMove()
    {
        GameController.canMove = true;
        rb.velocity = preVelocity;
    }
}
