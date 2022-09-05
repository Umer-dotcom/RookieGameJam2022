using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class IntEvent : UnityEvent<int>
{

}
public class IntEventListener : MonoBehaviour
{
	public IntEventChannelSO _channel = default;

	public IntEvent OnEventRaised;

	private void OnEnable()
	{
		if (_channel != null)
			_channel.OnEventRaised += Respond;
	}

	private void OnDisable()
	{
		if (_channel != null)
			_channel.OnEventRaised -= Respond;
	}

	private void Respond(int value)
	{
		
		if (OnEventRaised != null)
		{
			Debug.Log("Respond" + value);
			OnEventRaised.Invoke(value);
		}
	}
}
