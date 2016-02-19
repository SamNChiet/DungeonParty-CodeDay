using UnityEngine;
using System.Collections;

public class PlaceholderObject : MonoBehaviour {
	public string prefabName;
	// Use this for initialization
	void Start () {
		GameObject go = Instantiate(Resources.Load(prefabName), transform.position, Quaternion.identity) as GameObject;
		go.transform.parent = transform.parent;
		Destroy (gameObject);
	}
}
