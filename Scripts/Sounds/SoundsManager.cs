using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsManager : MonoBehaviour
{
    #region SingleTon
    private static SoundsManager instance=null;
    public static SoundsManager Instance
    {
        get
        {
            if(instance==null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    AudioSource audioSource;
    public AudioClip audioSound;

    [SerializeField] AudioClip ctrlKeySound;
    [SerializeField] AudioClip startKeySound;
    [SerializeField] AudioClip selectKeySound;
    [SerializeField] AudioClip maleAttackSound;
    [SerializeField] AudioClip manAttackSound;
    [SerializeField] AudioClip hitPlayerSound;
    [SerializeField] AudioClip hitEnemySound;
    [SerializeField] AudioClip postionSound;
    [SerializeField] AudioClip[] swordSound;
    [SerializeField] AudioClip[] skillSounds;
    public float voluem = 1;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        voluem = 1;
        audioSource.volume = 1;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.clip = audioSound;
        audioSource.Play();
    }

    public void SetBackSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.Play();
    }

    public void OnCtrlKeySound()
    {
        audioSource.PlayOneShot(ctrlKeySound);
    }

    public void OnStrartKeySound()
    {
        audioSource.PlayOneShot(startKeySound);
    }

    public void OnSelectKeySound()
    {
        audioSource.PlayOneShot(selectKeySound);
    }
    
    public void OnHitPlayer()
    {
        audioSource.PlayOneShot(hitPlayerSound);
    }

    public void OnHitEnemy()
    {
        audioSource.PlayOneShot(hitEnemySound);
    }

    public void OnPostionSound()
    {
        audioSource.PlayOneShot(postionSound);
    }

    public void OnSkillSound(int _nSelect)
    {
        switch (_nSelect)
        {
            case 0:
                audioSource.PlayOneShot(skillSounds[_nSelect]);
                break;
            case 1:
                audioSource.PlayOneShot(skillSounds[_nSelect]);
                break;
        }
    }

    public void OnSwordSound(int _nSelect)
    { 
        switch(_nSelect)
        {
            case 0:
                audioSource.PlayOneShot(swordSound[_nSelect]);
                break;
            case 1:
                audioSource.PlayOneShot(swordSound[_nSelect]);
                break;
        }

    }

    public void OnMaleAttackSound(int _nSelect) // 0 여자 1 남성
    {
        switch(_nSelect)
        {
            case 0:
                audioSource.PlayOneShot(maleAttackSound);
                break;
            case 1:
                audioSource.PlayOneShot(manAttackSound);
                break;
        }
    }

    public Slider slider;
    public void OnSoundVolume()
    {
        if (slider != null)
        {
            voluem = slider.value;
            audioSource.volume = voluem;
        }
    }

    public void SoundVolumeSet()
    {
        audioSource.volume = voluem;
    }
}
