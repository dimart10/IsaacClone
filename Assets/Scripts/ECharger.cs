using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECharger : MonoBehaviour
{
    public float idleSpeed = 3;
    public float chargeSpeed = 10;
    public float idleDirChangeTime = 2f;
    public float chargeDuration = 3f;
    public float chargeRange = 2f;
    public float chargeDelay = 0.5f;


    public Dir direction = Dir.UP;
    private Vector2[] directions = { new Vector2(0, 1), new Vector2(0, -1), new Vector2(-1, 0), new Vector2(1, 0) };


    public enum ChargerState {IDLE, PRECHARGE, CHARGE};
    public ChargerState state = ChargerState.IDLE;

    private GameObject player;
    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeIdleDir();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == ChargerState.CHARGE) ChargingUpdate();
        else if(state == ChargerState.IDLE) IdleUpdate();
    }

    private void ChargingUpdate()
    {
        // Colisiones con paredes?
    }

    private void IdleUpdate()
    {
        float xDist = transform.position.x - player.transform.position.x;
        float yDist = transform.position.y - player.transform.position.y;
        if(Mathf.Abs(xDist) < chargeRange)
        {
            if(yDist > 0) direction = Dir.DOWN;
            else direction = Dir.UP;
            PrepareCharge();
        }
        else if (Mathf.Abs(yDist) < chargeRange)
        {
            if(xDist > 0) direction = Dir.LEFT;
            else direction = Dir.RIGHT;
            PrepareCharge();
        }
    }

    private void ChangeIdleDir()
    {
        direction = (Dir)Random.Range((int)0, directions.Length);
        Invoke("ChangeIdleDir", idleDirChangeTime);
        rb.velocity = directions[(int)direction] * idleSpeed;
        anim.SetInteger("Direction", (int)direction);
    }

    private void PrepareCharge()
    {
        state = ChargerState.PRECHARGE;
        rb.velocity = Vector2.zero;
        anim.SetBool("Charging", true);
        anim.SetInteger("Direction", (int)direction);
        CancelInvoke();
        Invoke("StartCharge", chargeDelay);
    }

    private void StartCharge()
    {
        state = ChargerState.CHARGE;
        rb.velocity = directions[(int)direction] * chargeSpeed;
        Invoke("EndCharge", chargeDuration);
    }

    private void EndCharge()
    {
        state = ChargerState.IDLE;
        anim.SetBool("Charging", false);
        ChangeIdleDir();
    }
}
