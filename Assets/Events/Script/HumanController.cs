/** 
 *  @file    HumanController.cs
 *  @author  Darrus
 *  @date    17/11/2017  
 *  @brief   Contains the controller class for human AIs
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 *  @brief   Human Controller class that controls the animator and state changes.
 */
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

    /** 
     *  @brief   Human command struct that's only used in this class
     */
    struct HumanCommand
    {
        public CharacterStates state;
        public Vector3 target;
    };

    Queue<HumanCommand> commandQueue;

    public bool _isEnd = false;
    public float walkSpeed;
    public float runSpeed;

    Animator anim;
    Rigidbody rigid;

    [SerializeField]
    CharacterStates startState = CharacterStates.IDLE;
    CharacterStates characterState = CharacterStates.IDLE;

    /** 
     *  @brief   Getter and Setter for the current state of the AI, Upon setting it'll change the animator state as well.
     */
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

    /** 
     *  @brief   Initializes the variables as well as setting the start state of the AI
     */
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        commandQueue = new Queue<HumanCommand>();
        characterState = startState;
        anim.SetInteger("State", (int)characterState);
    }

    /** 
     *  @brief   Checks if the queue is empty, if not updates the commands
     */
    protected virtual void Update()
    {
        if (commandQueue.Count != 0)
        {
            if (HandleCommand(commandQueue.Peek()))
            {
                commandQueue.Dequeue();
                _isEnd = true;
            }
        }
    }

    /** 
     *  @brief   Run to the designated position stated, will also change the state of the AI to Run
     *  @param   position, the designated position for the AI to run to
     */
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

    /** 
     *  @brief   Walk to the designated position stated, will also change the state of the AI to Walk
     *  @param   position, the designated position for the AI to walk to
     */
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

    /** 
     *  @brief   Change the state of the AI to the desired state
     *  @param   state, the state to change into
     */
    public void ChangeState(CharacterStates state)
    {
        HumanCommand command = new HumanCommand();
        command.state = state;
        commandQueue.Enqueue(command);
    }

    /** 
     *  @brief   Handles the given command
     *  @param   command, the command to handle
     *  @return  returns true when the command is done, and return false when the command is still not done
     */
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

                if (dist <= 0.01f)
                    return true;
                break;
            default:
                return true;
        }
        return false;
    }
}
