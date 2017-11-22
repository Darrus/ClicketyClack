/**
 * @file : HumanAI.cs
 * @author : Tokiya Ogaki
 * @date : 17/11/2017
 * @brief : 盗賊のアニメーション制御
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @brief : 盗賊のアニメーションを制御するクラス
 */
public class HumanAI : HumanController
{
    private float delayTime = 2f;           ///< 時間
    private bool onState = true;            ///< アニメーション中


/**
*   @brief   毎フレーム呼び出される関数
*   @param  nothing
*   @return nothing 
*/
    protected override void Update()
    {
        // 基底クラスのUpdate
        base.Update();

        // 時間計測
        delayTime -= Time.deltaTime;

        // アニメーションが終わったら次のアニメーション
        if (delayTime < 0 && _isEnd)
        {
            _isEnd = false;
            onState = true;
        }

        //  アニメーション開始
        if (onState)
        {
            RandomState();
            onState = false;
            delayTime = 2f;
        }
    }


/**
*   @brief   ランダム座標を取得する関数
*   @param  nothing
*   @return Vector3 
*/
    private Vector3 RandomPosition()
    {
        float levelSize = 0.500f;
        return new Vector3(transform.position.x + Random.Range(-levelSize, levelSize), transform.position.y, transform.position.z + Random.Range(-levelSize, levelSize));
    }

/**
*   @brief   ランダムステート関数
*   @param  nothing
*   @return nothing 
*/
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

        // ランダムにステートを取得
        CharacterStates randState = availableStates[Random.Range(0, availableStates.Length)];

        switch (randState)
        {
            case CharacterStates.IDLE:
                ChangeState(CharacterStates.IDLE);
                break;

            case CharacterStates.WALK:
                WalkTo(RandomPosition());
                break;

            case CharacterStates.RUN:
                RunTo(RandomPosition());
                break;

            case CharacterStates.CHEER:
                ChangeState(CharacterStates.CHEER);
                break;

            case CharacterStates.JUMP:
                ChangeState(CharacterStates.JUMP);
                break;
        }
    }
}