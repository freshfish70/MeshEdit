using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragme : MonoBehaviour
{
	private Vector3 screenPoint;
	private Vector3 offset;

	private MeshStudy meshStudy;

	private int vertindex;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;

		this.meshStudy.PullOneVertex(vertindex, this.transform.localPosition);
	}
	public void setReference(MeshStudy me, int verticee)
	{
		this.meshStudy = me;
		this.vertindex = verticee;
	}
}
