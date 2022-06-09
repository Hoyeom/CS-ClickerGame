using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Manager
{
    public class SoundManager
    {
        private AudioSource[] _audioSources = new AudioSource[Enum.GetValues(typeof(Define.Sound)).Length];
        private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
        
        private GameObject Root { get; set; } = null;
        
        public void Initialize()
        {
            if (Root == null)
            {
                Root = GameObject.Find("@SoundRoot");
                if (Root == null)
                {
                    Root = new GameObject { name = "@SoundRoot" };
                    Object.DontDestroyOnLoad(Root);

                    string[] soundTypeNames = Enum.GetNames(typeof(Define.Sound));
                    for (int count = 0; count < soundTypeNames.Length; count++)
                    {
                        GameObject go = new GameObject { name = soundTypeNames[count] };
                        _audioSources[count] = go.AddComponent<AudioSource>();
                        go.transform.parent = Root.transform;
                    }

                    _audioSources[(int)Define.Sound.Bgm].loop = true;
                }
            }
        }

        public void Clear()
        {
            foreach (AudioSource audioSource in _audioSources)
                audioSource.Stop();
            _audioClips.Clear();
        }

        public void SetPitch(Define.Sound type, float pitch = 1.0f)
        {
            AudioSource audioSource = _audioSources[(int) type];
            if(audioSource == null)
                return;

            audioSource.pitch = pitch;
        }


        public bool Play(Define.Sound type, string path, float volume = 1.0f, float pitch = 1.0f)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            AudioSource audioSource = _audioSources[(int) type];
            if (path.Contains("Sound/") == false)
                path = $"Sound/path";

            audioSource.volume = volume;

            AudioClip audioClip = null;
            
            switch (type)
            {
                case Define.Sound.Bgm:
                    
                    audioClip = Managers.Resource.Load<AudioClip>(path);
                    if (audioClip == null)
                        return false;

                    if (audioSource.isPlaying)
                        audioSource.Stop();

                    audioSource.clip = audioClip;
                    audioSource.pitch = pitch;
                    audioSource.Play();
                    
                    return true;
                case Define.Sound.Effect:
                    
                    audioClip = GetAudioClip(path);
                    if (audioClip == null)
                        return false;

                    audioSource.pitch = pitch;
                    audioSource.PlayOneShot(audioClip);
                    return true;
                case Define.Sound.Speech:
                    
                    audioClip = GetAudioClip(path);
                    if (audioClip == null)
                        return false;

                    if (audioSource.isPlaying)
                        audioSource.Stop();

                    audioSource.clip = audioClip;
                    audioSource.pitch = pitch;
                    audioSource.Play();
                    return true;
            }

            return false;
        }

        public void Stop(Define.Sound type)
        {
            AudioSource audioSource = _audioSources[(int) type];
            audioSource.Stop();
        }
        
        private AudioClip GetAudioClip(string path)
        {
            AudioClip audioClip = null;
            if (_audioClips.TryGetValue(path, out audioClip))
                return audioClip;

            audioClip = Managers.Resource.Load<AudioClip>(path);
            _audioClips.Add(path, audioClip);
            return audioClip;
        }
    }
}