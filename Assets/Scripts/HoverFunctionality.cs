using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HoverFunctionality : MonoBehaviour {

	void Start() {
		GetComponent<TextMesh> ().color = Color.black;
	}

	void OnMouseEnter() {
		GetComponent<TextMesh> ().color = Color.red;
	}

	void OnMouseExit() {
		GetComponent<TextMesh> ().color = Color.black;
	}

	void OnMouseUp() {
		if (GetComponent<TextMesh> ().color == Color.red)
			SceneManager.LoadScene (1, LoadSceneMode.Single);
	}

}
