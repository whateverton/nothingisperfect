using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimaDialogo : MonoBehaviour {
	
	public Text textNarrador;
	public int falaAtual;
	public int CanvasAtual;
	public string[] Falas;
	public Image[] ExemplosImage;
	public Animation[] ExemplosAnimation;
	public GameObject[] Canvas;
	public bool estaFalando = false;
	public bool terminouFala = false;
	public int TamanhoDoVetor;
	public GameObject QuadroBranco;

	void Start () {
		falaAtual = 0;
		CanvasAtual = 0;
		Falas = new string[] {
			"Hello there young student , i'm your new professor ! i'm going to teach you some lessons about the Present Perfect!",
			"The present perfect is one of the English tenses to know about it doesn't have an equivalent in portuguese, so it can be a little difficult in the beginning, so watch and learn! ",
			"The present perfect have three knows uses, for example: \n \n 1 - Actions that have started on the past and keeps going",
			"Here are some words of Present Perfect adverbs (Time words)",
			"Here some sentences with more time words adverbs",
			"The Present Perfect have three known uses, for example: \n \n 2 - Actions that occur in an undertermined time in the past",
			"The Present Perfect have three known uses, for example: \n \n 3 - Actions that have just happened",
			"I Hope you do everything just fine, if you forget anything just press (button) to come back to this lesson"
		};
	}

	void Update () {
		if (!estaFalando && falaAtual < Falas.Length) {
			StartCoroutine (AnimateText (Falas[falaAtual]));
			falaAtual ++;
			estaFalando = true;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (terminouFala) {
				QuadroBranco.GetComponent<Animation> ().Play ();
			}
		}

		if (QuadroBranco.transform.position.y == 0) {
			QuadroBranco.transform.position += new Vector3 (0, -10, 0);
			apagaTela ();
			terminouFala = false;
		}

		ExemplosImage = Canvas [CanvasAtual].GetComponentsInChildren<Image> ();
		ExemplosAnimation = Canvas [CanvasAtual].GetComponentsInChildren<Animation> ();

		if (CanvasAtual == 0) {
			terminouFala = true;
		}

		for (int i = 1; i < ExemplosImage.Length; i++) {
			if (ExemplosImage [i - 1].fillAmount == 1 && ExemplosImage [i].fillAmount == 0) {
				ExemplosAnimation [i].Play ();
			} else if (ExemplosImage [ExemplosImage.Length - 1].fillAmount == 1) {
				terminouFala = true;
			}
		}
	}
		
	public void AnimaSprites(){
		ExemplosAnimation[0].Play ();

		for (int i = 0; i < ExemplosImage.Length; i++) {
			if (i < ExemplosImage.Length) {
				ExemplosImage[i].fillAmount = 0;
			}
		}
	}

	public void apagaTela(){

		for (int i = 1; i < ExemplosImage.Length; i++) {
			ExemplosImage [0].fillAmount = 0;
			ExemplosImage [i].fillAmount = 0;
		}
		estaFalando = false;
		CanvasAtual++;
	}

	IEnumerator AnimateText(string FalaCompleta){
		int i = 0;
		textNarrador.text = "";

		while( i < FalaCompleta.Length ){
			textNarrador.text += FalaCompleta[i++];
			yield return new WaitForSeconds(0.0F);
		}

		if (i >= FalaCompleta.Length) {
			AnimaSprites ();
		} 
	}
}
