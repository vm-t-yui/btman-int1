﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : struct
{
    /// <summary>
    /// ステート
    /// </summary>
    class State
    {
        readonly Action EnterAction;  // 開始時に呼び出されるデリゲート
        readonly Action UpdateAction; // 更新時に呼び出されるデリゲート
        readonly Action ExitAction;   // 終了時に呼び出されるデリゲート

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public State(Action enterAction = null, Action updateAction = null, Action exitAction = null)
        {
            // ジェネリックがenum型かどうか判断
            Type type = typeof(T);
            // 渡された型がenum以外だったら、Assertでとめる
            Debug.Assert(type.IsEnum);

            EnterAction = enterAction ?? delegate { };
            UpdateAction = updateAction ?? delegate { };
            ExitAction = exitAction ?? delegate { };
        }

        /// <summary>
        /// 開始
        /// </summary>
        public void Enter()
        {
            EnterAction();
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            UpdateAction();
        }

        /// <summary>
        /// 終了
        /// </summary>
        public void Exit()
        {
            ExitAction();
        }
    }

    Dictionary<T, State> stateMap = new Dictionary<T, State>();   // ステートのテーブル
    State currentState;                                             // 現在のステート
    T currentStateKey;                                              // 現在のステートキー

    /// <summary>
    /// ステートの追加
    /// </summary>
    public void Add(T key, Action enterAction = null, Action updateAction = null, Action exitAction = null)
    {
        stateMap.Add(key, new State(enterAction, updateAction, exitAction));
    }

    /// <summary>
    /// 現在のステートの設定
    /// </summary>
    public void SetState(T key)
    {
        currentState?.Exit();
        currentStateKey = key;
        currentState = stateMap[key];
        currentState.Enter();
    }

    /// <summary>
    /// 現在のステートの取得
    /// </summary>
    public T GetState()
    {
        return currentStateKey;
    }

    /// <summary>
    /// 現在のステートの更新
    /// </summary>
    public void Update()
    {
        currentState?.Update();
    }

    /// <summary>
    /// すべてのステートの削除
    /// </summary>
    public void Clear()
    {
        stateMap.Clear();
        currentState = null;
    }
}
