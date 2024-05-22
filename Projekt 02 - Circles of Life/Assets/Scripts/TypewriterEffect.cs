using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


	public class TypewriterEffect: MonoBehaviour{

		private TextMeshProUGUI Target;
		private string Text;
		private float Speed;

		private int currentPosition = -1;

		public bool HasFinished {get; private set;}

		private TypewriterEffect(TextMeshProUGUI target, string text, float speed){
			Target = target;
			Text = text;
			Speed = speed;
		}

		public static TypewriterEffect Start (TextMeshProUGUI target, string text, float speed = 0.03f){
			var effect = new TypewriterEffect(target, text, speed);
			
			target.StartCoroutine(effect.Run());
			
			return effect;
		}

		private IEnumerator Run(){
			Target.text = "";

			var textLength = Text.Length;

			while(!HasFinished && currentPosition + 1 < textLength){
				Target.text += GetNextToken();

				yield return new WaitForSeconds(Speed);
			}

			HasFinished = true;
		}

		private String GetNextToken(){
			currentPosition++;

			var NextToken = Text[currentPosition].ToString();

			return NextToken;
		}
	}