using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HumanAI : HumanController
{

    private float delayTime = 2f;
    private bool onState = true;

    protected override void Update()
    {
        base.Update();

        delayTime -= Time.deltaTime;

        if (delayTime < 0 && _isEnd)
        {
            _isEnd = false;
            onState = true;
        }


        if (onState)
        {
            RandomState();
            onState = false;
            delayTime = 2f;
        }
    }

    private Vector3 RandomPosition()
    {
        float levelSize = 0.500f;
        return new Vector3( transform.position.x + Random.Range(-levelSize, levelSize), transform.position.y,transform.position.z + Random.Range(-levelSize, levelSize));
    }

    private void RandomState()
    {
        CharacterStates[] availableStates =
        {
             CharacterStates.IDLE,
            CharacterStates.WALK,
            CharacterStates.RUN,
            CharacterStates.CHEER,
            CharacterStates.JUMP
        };
        CharacterStates randState = availableStates[Random.Range(0, availableStates.Length)];

        switch (randState)
        {
            case CharacterStates.IDLE:
                Debug.Log("IDLE");
                ChangeState(CharacterStates.IDLE);
                break;

            case CharacterStates.WALK:
                Debug.Log("WALK");
                WalkTo(RandomPosition());
                break;

            case CharacterStates.RUN:
                Debug.Log("RUN");
                RunTo(RandomPosition());
                break;

            case CharacterStates.CHEER:
                Debug.Log("CHEER");
                ChangeState(CharacterStates.CHEER);
                break;

            case CharacterStates.JUMP:
                Debug.Log("JUMP");
                ChangeState(CharacterStates.JUMP);
                break;
        }

    }
}
