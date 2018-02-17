using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NF.StateMachine;
using Sample01;
using Sample01.States;
using UnityEngine.Assertions;


public class test_statemachine : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StateManager sm = new StateManager();
		sm.StateChanged += OnStateChanged;
		sm.Completed += OnStateCompleted;
		sm.RegisterState(new Sample01AState());
		sm.RegisterState(new Sample01BState());
		sm.RegisterState(new Sample01C1State());
		sm.RegisterState(new Sample01C2State());
		sm.RegisterState(new Sample01DState());
		sm.SetInitialState(Sample01StateTokens.A, null);
		sm.PerformAction(Sample01ActionTokens.Initialized, 51);
		Debug.Log(sm.CurrentState);
		Assert.IsTrue(true);
	}

	private void OnStateCompleted(object sender, EventArgs e)
	{
		Debug.Log("Completed");
	}

	private void OnStateChanged(object sender, StateChangedEventArgs e)
	{
		Debug.Log($"{e.OldState} -> {e.NewState}");
	}
}
