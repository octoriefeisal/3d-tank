using Boo.Lang.Environments;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehaviorScript : MonoBehaviour {
	
	private Transform myTransform;
	private GameObject selongsong;
	private GameObject titikTembakan;
	private AudioSource audioSource;
	private string stateRotasiVertikal;

	

	public float kecepatanRotasi = 20;
	public float kecepatanAwalPeluru = 20;
	public float gravity = 10;
	[HideInInspector]
	public float sudutMeriam;

	[HideInInspector]
	public float sudutTembak;

	public GameObject objekTembakan;
	public GameObject objekLedakan;
	public GameObject peluruMeriam;
	public AudioClip audioTembakan;
	public AudioClip audioLedakan;
	
	
	// Use this for initialization
	
	void Start () {
		myTransform = transform;
		
		//inisialisasi selongsong
		selongsong = myTransform.Find("selongsong").gameObject;
		
		//inisialisasi titk tembakan
		titikTembakan = selongsong.transform.Find("titiktembakan").gameObject;

		//inisialisasi komponen audiosource
		audioSource = selongsong.GetComponent<AudioSource>();
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

		sudutMeriam = myTransform.localEulerAngles.z;
		//region menentukan state
		sudutTembak = 360-selongsong.transform.localEulerAngles.x;
		
		if (sudutTembak == 0 || sudutTembak == 360)
		{
			stateRotasiVertikal="aman";
		}
		else if (sudutTembak > 0 && sudutTembak < 15)
		{
			stateRotasiVertikal="aman";
		}
		else if (sudutTembak > 15 && sudutTembak < 16)
		{
			stateRotasiVertikal="atas";
		}
		else if (sudutTembak > 350)
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
		
		
		//region Penembakan
		if( Input.GetKeyDown(KeyCode.Space))
		{
			#region Init Peluru
			 GameObject peluru = Instantiate(peluruMeriam, titikTembakan.transform.position,
			                                     Quaternion.Euler(
												 selongsong.transform.localEulerAngles.x,
			                                     myTransform.localEulerAngles.z,
			                                     0)); 
			#endregion

			#region Init Objek tembakan
			GameObject efekTembakan = Instantiate(objekTembakan, titikTembakan.transform.position,
				Quaternion.Euler(
					selongsong.transform.localEulerAngles.x,
					myTransform.localEulerAngles.z, 0));
			Destroy(efekTembakan, 1f) ;
			#endregion

			#region Init audio objek tembakan
			audioSource.PlayOneShot(audioTembakan);

            #endregion
        }

	}

}

	
