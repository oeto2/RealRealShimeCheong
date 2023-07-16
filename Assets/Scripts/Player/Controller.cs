using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    int direction; // direction
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    //벽이 감지되었음 WallDetect에서 관리
    public bool detectWall;

    
    // ray
    GameObject scanObject;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("move!!");
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                //spriteRenderer.flipX = true;
                direction = -1;
            }
            else
            {
                //spriteRenderer.flipX = false;
                direction = 1;

            }
        }

        //scan object
        if (Input.GetButtonDown("Jump") && scanObject != null) // space
        {
            Debug.Log("오 스페이스 누름! This is :" + scanObject.name);
        }
    }

    public float detect_range = 1.5f;
    public float moveSpeed = 5.0f;

    private void FixedUpdate() // move
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //Player의 이동조건
        if(!detectWall)
        {
            transform.position += new Vector3(h, 0, v) * moveSpeed * Time.deltaTime;
        }

        //Debug용 Ray 그리기
        Debug.DrawRay(GetComponent<Rigidbody2D>().position, new Vector3(direction * detect_range, 0, 0), new Color(1, 0, 0));

        //RaycasDetect
        RaycastHit2D rayHit_detect = Physics2D.Raycast(GetComponent<Rigidbody2D>().position, new Vector3(direction, 0, 0), detect_range, LayerMask.GetMask("obj_NPC"));

        if (rayHit_detect.collider != null)
        {
            scanObject = rayHit_detect.collider.gameObject;
            Debug.Log(scanObject.name);
        }
        else
        {
            scanObject = null;
        }
    }

    void OnCollisionEnter(Collision npc_collider)
    {
        if (npc_collider.gameObject.name == "NPC")
            Debug.Log("Touch!");
    }
}