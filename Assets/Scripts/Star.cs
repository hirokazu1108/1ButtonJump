using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private AudioManager audioManager;
    private void Start()
    {
        var obj = GameObject.Find("AudioManager");
        if (obj != null) audioManager = obj.GetComponent<AudioManager>();
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, Time.deltaTime * 100, 0);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "bodyCollider")
        {
            if (audioManager != null) audioManager.playSeOneShot(AudioKinds.SE_Goal);
            GameController.starNum += 1;
            Destroy(this.gameObject);
        }
    }
}
