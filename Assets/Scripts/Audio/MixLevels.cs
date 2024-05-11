using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour {

	public void SetMasterLvl(float masterLvl)
	{
		GameManager.Instance.masterMixer.SetFloat("masterVol", masterLvl);
		SetSfxLvl(Mathf.Max(masterLvl - 10, 0));
		SetMusicLvl(masterLvl);
	}

	public void SetSfxLvl(float sfxLvl)
	{
		GameManager.Instance.masterMixer.SetFloat("sfxVol", sfxLvl);
	}

	public void SetMusicLvl (float musicLvl)
	{
		GameManager.Instance.masterMixer.SetFloat ("musicVol", musicLvl);
	}
}
