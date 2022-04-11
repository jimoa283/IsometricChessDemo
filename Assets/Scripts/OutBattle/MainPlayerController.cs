using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainPlayerController :MSingleton<MainPlayerController>
{
    private Animator anim;
    private Rigidbody2D rb;
    public bool canControll;
    public float speed;
    private Vector2 movement;
    public InteractiveObject interactiveObject;
    private UnityAction exitAction;
    public string mainName;
    public int passWord;

    public UnityAction ExitAction { set => exitAction = value; }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(canControll&&UIManager.Instance.PanelStack.Count == 0)
        {
            PlayerMoveAnimSwitch();

            if(Input.GetKeyDown(KeyCode.L))
            {
                UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.BattleReadyUI));
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                exitAction?.Invoke();
                exitAction = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (canControll && UIManager.Instance.PanelStack.Count == 0)
        {
            PlayerMove();
        }
    }

    private void PlayerMove()
    {       
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);      
    }

    private void PlayerMoveAnimSwitch()
    {
        if (movement != Vector2.zero)
        {
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
        }
        anim.SetFloat("Speed", movement.magnitude);
    }

    public void SetIdle()
    {
        anim.SetFloat("Speed", 0);
    }

    public void SetIdleDirection(LookDirection lookDirection)
    {
        switch (lookDirection)
        {
            case LookDirection.Up:
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", 1);
                break;
            case LookDirection.Down:
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", -1);
                break;
            case LookDirection.Left:
                anim.SetFloat("Horizontal", -1);
                anim.SetFloat("Vertical", 0);
                break;
            case LookDirection.Right:
                anim.SetFloat("Horizontal", 1);
                anim.SetFloat("Vertical", 0);
                break;
            default:
                break;
        }
        anim.SetFloat("Speed", 0);
    }

    public void WakeInteractiveObject()
    {
        canControll = true;
        if(interactiveObject!=null)
        {
            if (interactiveObject.gameObject.activeInHierarchy)
                interactiveObject.ShowTipUI();
            else
                interactiveObject = null;
        }          
    }

    public void PlayAnim(string animName)
    {
        anim.Play(animName);
    }

    /*public void MoveAutomatic(Vector3 pos,LookDirection lookDirection,UnityAction action)
    {
        StartCoroutine(DoMoveAutomatic(pos, lookDirection, action));
    }

    IEnumerator DoMoveAutomatic(Vector3 pos,LookDirection lookDirection,UnityAction action)
    {
        Vector2 direction;
        if (pos.x > transform.position.x)
            direction.x = 1;
        else if (pos.x < transform.position.x)
            direction.x = -1;
        else
            direction.x = 0;

        if (pos.y > transform.position.y)
            direction.y = 1;
        else if (pos.y < transform.position.y)
            direction.y= -1;
        else
            direction.y = 0;


        while (Vector2.Distance(transform.position,pos)<0.01f)
        {
            yield return null;
            rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);
            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", direction.y);
            anim.SetFloat("Speed", direction.magnitude);
        }

        SetIdleDirection(lookDirection);
        action?.Invoke();
    }*/

}
