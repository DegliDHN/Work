using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EventAnalyticsTracker : Singleton<EventAnalyticsTracker>
{
	Dictionary<GaiaStates, float> timeSpentInAppStates = new Dictionary<GaiaStates, float>();

	private GaiaStates currState;
	private float currStateStartTime;

	protected override void InitTon()
	{
	}

	public void NotifyState_Start(GaiaStates state)
	{
		currStateStartTime = Time.time;
		currState = state;

		Debug.Log("State Start: "+state);

	}

	public void NotifyState_Finish(GaiaStates state)
	{
		Debug.Assert(currState == state);
		Debug.Log("State Finish: "+state);

		var elapsedTimeInCurrentState = Time.time - currStateStartTime;
		float tmp = timeSpentInAppStates.ContainsKey(state) ? timeSpentInAppStates[state] : 0f;
		timeSpentInAppStates[state] = tmp + elapsedTimeInCurrentState; 
	}

	[Button]
	public void SaveAnalyticsToDb(){
		foreach(var keyPair in timeSpentInAppStates){
			SaveTimeSpentInState(keyPair.Key, keyPair.Value);
		}
	}

	private void SaveTimeSpentInState(GaiaStates key, float value)
	{
		//Api call to DB 
		string user_id = AppDB.Instance.GetUser_ID() ?? "-1";
		Debug.Log($"[{key.ToString()}] = {value} - ID={user_id}");
		
	}
}
