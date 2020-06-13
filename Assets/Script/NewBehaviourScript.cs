using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	private Transform myTransform;
	private GameObject selongsong;
	private string stateRotasiVertikal;
	
	public float nilaiRotasiY;
	public float kecepatanRotasi = 20;
	
	// Use this for initialization
	
	void Start () {
		myTransform = transform;
		
		//inisialisasi selongsong
		selongsong = myTransform.Find("selongsong").gameObject;
	}
	
	void Update(){
		//rotasi horizontal
		if(Input.GetKey(KeyCode.T)) //horizontal lawan arah
		{
			myTransform.Rotate(Vector3.back * kecepatanRotasi * Time.deltaTime, Space.Self);			
		}	
		else if(Input.GetKey(KeyCode.U)) //horizontal searah jam

		{
			myTransform.Rotate(Vector3.forward * kecepatanRotasi * Time.deltaTime, Space.Self);
		}
		
		
		
		//region menentukan state
		nilaiRotasiY= 360-selongsong.transform.localEulerAngles.x;
		
		if (nilaiRotasiY ==0 ||nilaiRotasiY==360)
		{
			stateRotasiVertikal="aman";
		}
		else if (nilaiRotasiY > 0 && nilaiRotasiY<15)
		{
			stateRotasiVertikal="aman";
		}
		else if (nilaiRotasiY > 15 && nilaiRotasiY <16)
		{
			stateRotasiVertikal="atas";
		}
		else if (nilaiRotasiY > 350)
		{
			stateRotasiVertikal = "bawah";
		}
		
		//region arah rotasi vertikal berdasarkan state
		if( stateRotasiVertikal=="aman")
		{
			if(Input.GetKey(KeyCode.Y))
			{
				selongsong.transform.Rotate( Vector3.left * kecepatanRotasi * Time.deltaTime, Space.Self);
			}
			else if(Input.GetKey(KeyCode.H))
			{
				selongsong.transform.Rotate( Vector3.right * kecepatanRotasi * Time.deltaTime, Space.Self);
			}
		}
		else if( stateRotasiVertikal=="bawah")
		{
			selongsong.transform.localEulerAngles = new Vector3(-0.5f, selongsong.transform.localEulerAngles.y, selongsong.transform.localEulerAngles.z);
			                                              
		}
		else if( stateRotasiVertikal=="atas")
		{
			selongsong.transform.localEulerAngles = new Vector3(-14.5f, selongsong.transform.localEulerAngles.y, selongsong.transform.localEulerAngles.z);
			                                              
		}	

	}

}

	
