using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HumanController : MonoBehaviour
{
    public enum CharacterStates
    {
        IDLE,
        WALK,
        RUN,
        CHEER,
        DEFEAT,
        UNTIE,
        SIT,
        JUMP,
    };

    struct HumanCommand
    {
        public CharacterStates state;
        public Vector3 target;
    };

    Queue<HumanCommand> commandQueue;

    public float walkSpeed;
    public float runSpeed;

    Animator anim;
    Rigidbody rigid;

    [SerializeField]
    CharacterStates startState = CharacterStates.IDLE;

    CharacterStates characterState = CharacterStates.IDLE;
    public CharacterStates CurrentState
    {
        get
        {
            return characterState;
        }
        private set
        {
            characterState = value;
            anim.SetInteger("State", (int)characterState);
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        commandQueue = new Queue<HumanCommand>();
        characterState = startState;
        anim.SetInteger("State", (int)characterState);
    }

    private void Update()
    {
        if(commandQueue.Count != 0)
        {
            if(HandleCommand(commandQueue.Peek()))
            {
                commandQueue.Dequeue();
            }
        }
    }

    public void RunTo(Vector3 position)
    {
        HumanCommand command = new HumanCommand();
        transform.LookAt(position);

        // Forced fixed due to models not facing the right direction, due to time constraint.
        transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));

        command.state = CharacterStates.RUN;
        command.target = position;
        commandQueue.Enqueue(command);
    }

    public void WalkTo(Vector3 position)
    {
        HumanCommand command = new HumanCommand();
        transform.LookAt(position);

        // Forced fixed due to models not facing the right direction, due to time constraint.
        transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));

        command.state = CharacterStates.WALK;
        command.target = position;
        commandQueue.Enqueue(command);
    }

    public void ChangeState(CharacterStates state)
    {
        HumanCommand command = new HumanCommand();
        command.state = state;
        commandQueue.Enqueue(command);
    }

    bool HandleCommand(HumanCommand command)
    {
        CurrentState = command.state;

        switch (command.state)
        {
            case CharacterStates.WALK:
            case CharacterStates.RUN:
                Vector3 dir = command.target - transform.position;
                float dist = dir.sqrMagnitude;

                if (CurrentState == CharacterStates.WALK)
                    rigid.MovePosition(transform.position + dir.normalized * walkSpeed * Time.deltaTime);
                else
                    rigid.MovePosition(transform.position + dir.normalized * runSpeed * Time.deltaTime);

                if (dist > 0.01f)
                    return false;
                break;
        }
        return true;
    }
}
