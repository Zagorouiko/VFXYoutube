using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class AudioSyncParticleSize : AudioSyncer
{
	public VisualEffect visualEffect;
	public float beatScale;
	public float restScale;
	public Gradient gradient;

	public override void OnUpdate()
	{
		base.OnUpdate();		

		if (m_isBeat) return;

		visualEffect.SetFloat("Intensity", Mathf.Lerp(visualEffect.GetFloat("Intensity"), restScale, restSmoothTime * Time.deltaTime));
	}

	public override void OnBeat()
	{
		base.OnBeat();

		StopCoroutine("ChangeToScale");
		StartCoroutine("ChangeToScale", beatScale);
	}

	private IEnumerator ChangeToScale(float _target)
	{
		var _curr = visualEffect.GetFloat("Intensity");
		Debug.Log(_curr);
		var _initial = _curr;
		float _timer = 0;

		while (_curr != _target)
		{
			_curr = Mathf.Lerp(_initial, _target, _timer / timeToBeat);
			_timer += Time.deltaTime;

			visualEffect.SetFloat("Intensity", _curr);

			yield return null;
		}

		m_isBeat = false;
	}
}
