using System;
using System.Collections;
using System.Collections.Generic;

public class StateMachine<T>
{
    /// <summary>
    /// ステート
    /// </summary>
    private class State
    {
        readonly Action EnterAction;  // 開始時に呼び出されるデリゲート
        readonly Action UpdateAction; // 更新時に呼び出されるデリゲート
        readonly Action ExitAction;   // 終了時に呼び出されるデリゲート

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public State(Action enterAction = null, Action updateAction = null, Action exitAction = null)
        {
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

    Dictionary<T, State> stateTable = new Dictionary<T, State>();   // ステートのテーブル
    State currentState;                                             // 現在のステート
    T currentStateKey;                                              // 現在のステートキー

    /// <summary>
    /// ステートの追加
    /// </summary>
    public void Add(T key, Action enterAction = null, Action updateAction = null, Action exitAction = null)
    {
        stateTable.Add(key, new State(enterAction, updateAction, exitAction));
    }

    /// <summary>
    /// 現在のステートの設定
    /// </summary>
    public void SetState(T key)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentStateKey = key;
        currentState = stateTable[key];
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
        if (currentState == null)
        {
            return;
        }
        currentState.Update();
    }

    /// <summary>
    /// すべてのステートの削除
    /// </summary>
    public void Clear()
    {
        stateTable.Clear();
        currentState = null;
    }
}
