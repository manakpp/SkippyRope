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

public class RopeGenerator : MonoBehaviour
{
	[SerializeField]
	private int numberOfBodies;

	[SerializeField]
	private Transform startPoint;

	[SerializeField]
	private Transform endPoint;

	[SerializeField]
	private GameObject pointPrefab;

	private List<Rigidbody> points;

	void Awake()
	{
		points = new List<Rigidbody>();

		Rigidbody prevBody = null;

		Vector3 deltaDistance = endPoint.position - startPoint.position;
		deltaDistance /= numberOfBodies;

		for(int i = 0; i < numberOfBodies; ++i)
		{
			GameObject newPoint = GameObject.Instantiate(pointPrefab, startPoint.position + deltaDistance * i, Quaternion.identity) as GameObject;

			newPoint.transform.parent = transform;

			HingeJoint newJoint = newPoint.GetComponent<HingeJoint>();

			if (i != 0)
			{
				newJoint.connectedBody = prevBody;
			}

			//if (i == 0 || i == numberOfBodies - 1)
			//{
			//	Rigidbody body = newJoint.GetComponent<Rigidbody>();

			//	body.isKinematic = true;
			//}

			if (i == 0)
			{
				Rigidbody body = newJoint.GetComponent<Rigidbody>();

				body.isKinematic = true;
			}

			prevBody = newJoint.GetComponent<Rigidbody>();

			points.Add(prevBody);
		}
	}

	void Update()
	{
		for(int i = 0; i < numberOfBodies; ++i)
		{
			if(i == 0)
			{
				continue;
			}

			Debug.DrawLine(points[i - 1].GetComponent<Rigidbody>().position, points[i].GetComponent<Rigidbody>().position, Color.cyan, 0.1f);
		}
	}
}