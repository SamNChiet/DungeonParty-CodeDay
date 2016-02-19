#pragma strict

public var countdownText : TextMesh;
public var countdownSound : AudioClip;

var audioSource : AudioSource;

function Start () {
	StartAnim();
	//audioSource = GetComponent(AudioSource);
}

function StartAnim() {
	audioSource.PlayOneShot(countdownSound);
	countdownText.text = "5";
	yield WaitForSeconds(1);
	audioSource.PlayOneShot(countdownSound);
	countdownText.text = "4";
	yield WaitForSeconds(1);
	audioSource.PlayOneShot(countdownSound);
	countdownText.text = "3";
	yield WaitForSeconds(1);
	audioSource.PlayOneShot(countdownSound);
	countdownText.text = "2";
	yield WaitForSeconds(1);
	audioSource.PlayOneShot(countdownSound);
	countdownText.text = "1";
	yield WaitForSeconds(1);
	countdownText.text = "0";
	Application.LoadLevel(Application.loadedLevel+1);
}