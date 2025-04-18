using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    // Instancia �nica del AudioManager (porque es una clase Singleton) STATIC
    public static AudioController instance;

    // Se crean dos AudioSources diferentes para que se puedan
    // reproducir los efectos y la m�sica de fondo al mismo tiempo
    public AudioSource sfxSource; // Componente AudioSource para efectos de sonido
    public AudioSource musicSource; // Componente AudioSource para la m�sica de fondo

    // En vez de usar un vector de AudioClips (que podr�a ser) vamos a utilizar un Diccionario
    // en el que cargaremos directamente los recursos desde la jerarqu�a del proyecto
    // Cada entrada del diccionario tiene una string como clave y un AudioClip como valor
    public Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> musicClips = new Dictionary<string, AudioClip>();

    // M�todo Awake que se llama al inicio antes de que se active el objeto. �til para inicializar
    // variables u objetos que ser�n llamados por otros scripts (game managers, clases singleton, etc).
    private void Awake()
    {

        // ----------------------------------------------------------------
        // AQU� ES DONDE SE DEFINE EL COMPORTAMIENTO DE LA CLASE SINGLETON
        // Garantizamos que solo exista una instancia del AudioManager
        // Si no hay instancias previas se asigna la actual
        // Si hay instancias se destruye la nueva
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        // ----------------------------------------------------------------

        // No destruimos el AudioManager aunque se cambie de escena
        DontDestroyOnLoad(gameObject);

        // Cargamos los AudioClips en los diccionarios
        LoadSFXClips();
        LoadMusicClips();

    }

    // M�todo privado para cargar los efectos de sonido directamente desde las carpetas
    private void LoadSFXClips()
    {
        // Los recursos (ASSETS) que se cargan en TIEMPO DE EJECUCI�N DEBEN ESTAR DENTRO de una carpeta denominada /Assets/Resources/SFX
        sfxClips["ShootPlayer"] = Resources.Load<AudioClip>("SFX/ShootPlayer");
        sfxClips["DieEnemy"] = Resources.Load<AudioClip>("SFX/DieEnemy");
        sfxClips["PlayerDead"] = Resources.Load<AudioClip>("SFX/PlayerDead");
    }

    // M�todo privado para cargar la m�sica de fondo directamente desde las carpetas
    private void LoadMusicClips()
    {
        // Los recursos (ASSETS) que se cargan en TIEMPO DE EJECUCI�N DEBEN ESTAR DENTRO de una carpeta denominada /Assets/Resources/Music
        musicClips["MainTheme"] = Resources.Load<AudioClip>("Music/MainTheme");
        musicClips["BossTheme"] = Resources.Load<AudioClip>("Music/BossTheme");
    }

    // M�todo de la clase singleton para reproducir efectos de sonido
    public void PlaySFX(string clipName)
    {
        if (sfxClips.ContainsKey(clipName))
        {
            sfxSource.clip = sfxClips[clipName];
            sfxSource.Play();
        }
    }

    // M�todo de la clase singleton para reproducir m�sica de fondo
    public void PlayMusic(string clipName)
    {
        if (musicClips.ContainsKey(clipName))
        {
            musicSource.clip = musicClips[clipName];
            musicSource.Play();
        }
    }

    // -----------------------------------------
    // EJEMPLOS DE USO �DESDE CUALQUIER SCRIPT!
    // -----------------------------------------
    // AudioManager.instance.PlayMusic("MainTheme");
    // AudioManager.instance.PlaySFX("Jump");

}
