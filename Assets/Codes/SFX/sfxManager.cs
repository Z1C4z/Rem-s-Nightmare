using UnityEngine;
using System.Collections.Generic;

public static class Sfx
{
    const int PoolSize = 16;

    static AudioSource[] pool;
    static int next;
    static readonly Dictionary<string, AudioClip> cache = new();

    public static float MasterVolume = 1f;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatics()
    {
        pool = null;
        next = 0;
        cache.Clear();
    }

    static void Init()
    {
        var go = new GameObject("[Sfx]");
        Object.DontDestroyOnLoad(go);

        pool = new AudioSource[PoolSize];
        for (int i = 0; i < PoolSize; i++)
        {
            var src = go.AddComponent<AudioSource>();
            src.playOnAwake = false;
            src.spatialBlend = 0f; // 2D
            pool[i] = src;
        }
    }

    public static void Play(AudioClip clip, float volume = 1f, float pitch = 1f, float pitchVariation = 0f)
    {
        if (clip == null) return;
        if (pool == null || pool[0] == null) Init();

        var src = pool[next];
        next = (next + 1) % PoolSize;

        src.Stop();
        src.clip = clip;
        src.volume = volume * MasterVolume;
        src.pitch = pitch + Random.Range(-pitchVariation, pitchVariation);
        src.Play();
    }

    public static void Play(string name, float volume = 1f, float pitch = 1f, float pitchVariation = 0f)
    {
        if (!cache.TryGetValue(name, out var clip))
        {
            clip = Resources.Load<AudioClip>("Sfx/" + name);
            if (clip == null)
            {
                Debug.LogWarning($"Sfx: clip '{name}' não encontrado em Resources/Sfx/");
                return;
            }
            cache[name] = clip;
        }
        Play(clip, volume, pitch, pitchVariation);
    }

    public static void PlayAt(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null) return;
        AudioSource.PlayClipAtPoint(clip, position, volume * MasterVolume);
    }

    public static void PlayAt(string name, Vector3 position, float volume = 1f)
    {
        if (!cache.TryGetValue(name, out var clip))
        {
            clip = Resources.Load<AudioClip>("Sfx/" + name);
            if (clip == null)
            {
                Debug.LogWarning($"Sfx: clip '{name}' não encontrado em Resources/Sfx/");
                return;
            }
            cache[name] = clip;
        }
        PlayAt(clip, position, volume);
    }
}