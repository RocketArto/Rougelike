using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    public static NewMovement Instance;

    public float dashRange;
    public float dashSpeed;
    public bool isDash;
    public float speed;
    public Vector2 direction;
    private Animator animator;
    public enum FaceDir {DOWN, UP, LEFT, RIGHT}
    public FaceDir faceDir;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start(){
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        TakeInput();
        Move();
    }

    private void Move(){
        if (isDash == false)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            SetAnimatorMovement(direction);
        }
        else
        {
            transform.Translate(direction * dashSpeed * Time.deltaTime);
            SetAnimatorMovement(direction);
        }
    }

    private void TakeInput(){
        direction = Vector2.zero;

        if(Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector2.up;
            faceDir = FaceDir.UP;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector2.left;
            faceDir = FaceDir.LEFT;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector2.down;
            faceDir = FaceDir.DOWN;
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;
            faceDir = FaceDir.RIGHT;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isDash = true;
        }
        else isDash = false;


        //if( Input.GetKeyDown(KeyCode.Space)&&count<=0) 
        //{ 
        //    Vector2 targetPos = transform.position;
        //    if (faceDir == FaceDir.UP) {
        //        targetPos.y += dashRange;
        //    }
        //    else if (faceDir == FaceDir.DOWN)
        //    {
        //        targetPos.y -= dashRange;
        //    }
        //    else if (faceDir == FaceDir.RIGHT) { 
        //        targetPos.x += dashRange;
        //    }
        //    else if (faceDir == FaceDir.LEFT)
        //    {
        //        targetPos.x -= dashRange;
        //    }
        //    transform.position= targetPos;
        //    count = dashDelay;
        //}
        //else
        //{
        //    count -= Time.deltaTime;
        //}
    }

    private void SetAnimatorMovement(Vector2 direction){
        if(direction != Vector2.zero){
            animator.SetBool("Run", true);
            animator.SetFloat("xDir", direction.x);
            animator.SetFloat("yDir", direction.y);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }  

}
