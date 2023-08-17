using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour {

	// Use this for initialization
	//[SerializeField] bool keyDown;
	//[SerializeField] int maxIndex;
	public AudioSource audioSource;

	private int _index;
	[SerializeField] private List<Animator> _listaBotones = new List<Animator>();
	[SerializeField] private MainMenu controller;
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			_listaBotones[_index].SetBool("SizeDown",true);
			_listaBotones[_index].SetBool("SizeUp", false);
			_index--;
			if (_index < 0)
				_index = _listaBotones.Count - 1;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			_listaBotones[_index].SetBool("SizeDown", true);
			_listaBotones[_index].SetBool("SizeUp", false);
			_index++;
			if (_index > _listaBotones.Count - 1)
				_index = 0;
		}

		_listaBotones[_index].SetBool("SizeUp", true);
		_listaBotones[_index].SetBool("SizeDown", false);

		if(Input.GetKeyDown(KeyCode.Return))
        {
            switch (_index)
            {
				case 0:
					controller.LoadScene();
					break;
				case 1:
					controller.OpenOptions();
					break;
				case 2:
					controller.QuitGame();
					break;
            }
        }
	}

}
