using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GerakPeluruScript : MonoBehaviour {

	private Transform myTransform;
	public float waktuTerbangPeluru;

	private TankBehaviorScript tankBehavior;
	private float _kecAwal;
	private float _sudutMeriam;
	private float _sudutTembak;
	private float _gravitasi;
	private Vector3 _posisiAwal;
	private AudioSource audioSource;

	public GameObject ledakan;
	public AudioClip audioLedakan;

	public GameManagerScript gameManager;
	private bool isLanded = true;

	// Use this for initialization
	void Start () {
		myTransform = transform;

		tankBehavior = GameObject.FindObjectOfType<TankBehaviorScript>();
		_kecAwal = tankBehavior.kecepatanAwalPeluru;
		_sudutTembak = tankBehavior.sudutTembak;
		_sudutMeriam = tankBehavior.sudutMeriam;

		_posisiAwal = myTransform.position;


		audioSource = GetComponent<AudioSource>();
		_gravitasi = GameObject.FindObjectOfType<TankBehaviorScript>().gravity;

		gameManager = GameObject.FindObjectOfType<GameManagerScript>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if( isLanded)
			waktuTerbangPeluru += Time.deltaTime;

		gameManager._lamaWaktuTerbangPeluru = this.waktuTerbangPeluru;

		myTransform.position = PosisiTerbangPeluru(myTransform.position, _kecAwal, waktuTerbangPeluru, _sudutTembak, _sudutMeriam);
	}

	private Vector3 PosisiTerbangPeluru(Vector3 _posisiAwal, float _kecAwal, float _waktu, 
		float _sudutTembak,float  _sudutMeriam)
    {
		float _x = _posisiAwal.x + (_kecAwal * _waktu * Mathf.Sin(_sudutMeriam * Mathf.PI / 180));
		float _y = _posisiAwal.y + ((_kecAwal * _waktu * Mathf.Sin(_sudutTembak * Mathf.PI / 180)) - (0.5f * 10 * Mathf.Pow(_waktu,2)));
		float _z = _posisiAwal.z +(_kecAwal * _waktu * Mathf.Cos(_sudutMeriam * Mathf.PI / 180));

		return new Vector3(_x,_y,_z);
    }

	 private void OnTriggerEnter(Collider other)
    {
		if( other.tag == "Land1")
        {
			//Debug.Log("KENAAA");
			Destroy(this.gameObject,2f);

			GameObject go = Instantiate(ledakan, myTransform.position,
				Quaternion.identity);
			Destroy(go, 3f);

			audioSource.PlayOneShot(audioLedakan);

			gameManager._jarakTembak = Vector3.Distance(_posisiAwal, myTransform.position);

			isLanded = false;
		}
    }

	
}
