/* Copyright (c) 2015 Mana Khamphanpheng

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveOnCircle : MonoBehaviour
{
	public float radius = 10.0f;
	public float speed = 10.0f;
	public float timeAccum = 0.0f;

	public float phaseTimeAccum = 0.0f;

	public List<Phase> m_phases = new List<Phase>();

	List<Phase> m_phaseShuffleBag;

	Phase currentPhase;

	[System.Serializable]
	public class Phase
	{
		public float speed;
		public float duration;
	}

	Vector3 startPos;

	void Start()
	{
		startPos = transform.position;

		FillBag();
		currentPhase = m_phaseShuffleBag[0];
		m_phaseShuffleBag.Remove(currentPhase);
	}

	void Update()
	{
		phaseTimeAccum += Time.deltaTime;
		if(phaseTimeAccum >= currentPhase.duration)
		{
			phaseTimeAccum = 0.0f;
			SetNewPhase();
		}
		
		Vector3 newPos = GetComponent<Rigidbody>().position;

		timeAccum += Time.deltaTime * speed;
		newPos.z = startPos.z + Mathf.Sin(timeAccum) * radius;
		newPos.y = (startPos.y) + Mathf.Cos(timeAccum) * radius;

		GetComponent<Rigidbody>().MovePosition(newPos);
	}

	void FillBag()
	{
		m_phaseShuffleBag = new List<Phase>();
		m_phaseShuffleBag.AddRange(m_phases);
	}

	void SetNewPhase()
	{
		if (m_phaseShuffleBag.Count == 0)
		{
			FillBag();
		}

		currentPhase = m_phaseShuffleBag[Random.Range(0, m_phaseShuffleBag.Count)];

		m_phaseShuffleBag.Remove(currentPhase);

		speed = currentPhase.speed;
	}
}