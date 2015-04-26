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

public class PlayerController : MonoBehaviour
{
	public float jumpForce = 200.0f;
	public float moveForce = 50.0f;
	public float randomTorque = 500.0f;
	public Vector3 vrandomTorque;

	bool isGrounded = true;
	float time = 0.0f;

	void Start()
	{
		// TODO: make the mesh jelly-like
		// Here's some code where I was trying to change the verts

		//Mesh mesh = GetComponent<MeshFilter>().mesh;
		//Vector3[] verts = mesh.vertices;

	
		//for(int i = 0; i < verts.Length; ++i)
		//{
		//	verts[i] *= 1.5f;
		//}

		//mesh.vertices = verts;
	}


	void Update()
	{
		if ((isGrounded && GetComponent<Rigidbody>().velocity.y <= 0.01f) &&
			(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
		{
			GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);

			//GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0.0f, randomTorque), Random.Range(0.0f, randomTorque), Random.Range(0.0f, randomTorque)));
			GetComponent<Rigidbody>().AddTorque(vrandomTorque);

			isGrounded = false;
		}

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		GetComponent<Rigidbody>().AddForce(Camera.main.transform.TransformDirection(new Vector3(horizontal, 0.0f, vertical)) * moveForce);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Rope"))
		{
			Application.LoadLevel(0);
		}
		else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Default"))
		{
			isGrounded = true;
		}
	}
}