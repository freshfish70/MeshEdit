/*
 * Copyright (c) 2019 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// [ExecuteInEditMode]
public class MeshStudy : MonoBehaviour
{
	Mesh oMesh;
	Mesh cMesh;
	MeshFilter oMeshFilter;
	int[] triangles;

	[HideInInspector]
	public Vector3[] vertices;

	[HideInInspector]
	public bool isCloned = false;

	// For Editor
	public float radius = 0.2f;
	public float pull = 0.3f;
	public float handleSize = 0.03f;
	public List<int>[] connectedVertices;
	public List<Vector3[]> allTriangleList;
	public bool moveVertexPoint = true;

	public dragme sph;



	private Transform handleTransform;
	private Quaternion handleRotation;
	string triangleIdx;

	void Start()
	{
		InitMesh();
	}

	public void InitMesh()
	{
		oMeshFilter = GetComponent<MeshFilter>();
		oMeshFilter.sharedMesh.RecalculateBounds();
		oMeshFilter.sharedMesh.RecalculateNormals();
		oMeshFilter.sharedMesh.RecalculateTangents();

		oMesh = oMeshFilter.sharedMesh;

		cMesh = new Mesh();
		cMesh.name = "clone";
		cMesh.vertices = oMesh.vertices;
		cMesh.triangles = oMesh.triangles;
		cMesh.normals = oMesh.normals;
		cMesh.uv = oMesh.uv;
		cMesh.RecalculateNormals();
		oMeshFilter.mesh = cMesh;

		vertices = oMesh.vertices;
		triangles = cMesh.triangles;
		isCloned = true;
		Debug.Log("Init & Cloned");

		EditMesh1();

	}

	// private void OnDrawGizmos()
	// {
	// 	Gizmos.color = Color.black;
	// 	for (int i = 0; i < vertices.Length; i++)
	// 	{
	// 		Gizmos.DrawSphere(vertices[i], 0.1f);
	// 	}
	// }


	void EditMesh1()
	{
		handleTransform = this.transform;
		// handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

		for (int i = 0; i < vertices.Length; i++)
		{
			ShowPoint(i);
		}
	}

	private void ShowPoint(int index)
	{
		Debug.Log(index);
		Debug.Log(vertices[index]);


		Vector3 point = handleTransform.TransformPoint(vertices[index]);
		// Gizmos.color = Color.red;
		// Gizmos.DrawSphere(vertices[index], 0.1f);
		var e = Instantiate(sph);
		e.transform.parent = this.transform;
		e.transform.localPosition = vertices[index];
		e.setReference(this, index);
		// if (mesh.moveVertexPoint)
		// {
		// 	//draw dot
		// 	Handles.color = Color.blue;
		// 	point = Handles.FreeMoveHandle(point, handleRotation, mesh.handleSize, Vector3.zero, Handles.DotHandleCap);

		// 	// //drag
		// 	// if (GUI.changed)
		// 	// {
		// 	// 	mesh.DoAction(index, handleTransform.InverseTransformPoint(point));
		// 	// }
		// }
		// else
		// {
		// 	Handles.color = Color.cyan;
		// }
	}

	public void Reset()
	{
		if (cMesh != null && oMesh != null)
		{
			cMesh.vertices = oMesh.vertices;
			cMesh.triangles = oMesh.triangles;
			cMesh.normals = oMesh.normals;
			cMesh.uv = oMesh.uv;
			oMeshFilter.mesh = cMesh;
			// update local vars..
			vertices = cMesh.vertices;
			triangles = cMesh.triangles;
		}
	}

	// Pulling only one vertex pt, results in broken mesh.
	public void PullOneVertex(int index, Vector3 newPos)
	{
		var pos = vertices[index];
		vertices[index] = new Vector3(newPos.x, newPos.y, pos.z);
		cMesh.vertices = vertices;
		cMesh.RecalculateNormals();
	}

}