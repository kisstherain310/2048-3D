using UnityEngine ;

public class FXManager : MonoBehaviour {
    [SerializeField] private ParticleSystem[] cubeExplosionFX ;

    ParticleSystem.MainModule[] cubeExplosionFXMainModule ;

    //singleton class
    public static FXManager Instance ;
    // ------- Initialization ----------------
    private void Awake () {
        Instance = this ;
        cubeExplosionFXMainModule = new ParticleSystem.MainModule[cubeExplosionFX.Length];
    }

    private void Start () {
        for (int i = 0; i < cubeExplosionFX.Length; i++)
        {
            cubeExplosionFXMainModule[i] = cubeExplosionFX[i].main;
        }
    }

    //  ------- Get FX ----------------
    public GameObject GetFX () {
        return this.gameObject;
    }
    // ------- Play Cube Explosion FX ----------------
    public void PlayCubeExplosionFX (Vector3 position, Color color) {
        for (int i = 0; i < cubeExplosionFX.Length; i++)
        {
            cubeExplosionFXMainModule[i].startColor = new ParticleSystem.MinMaxGradient (color) ;
            cubeExplosionFX[i].transform.position = position ;
            cubeExplosionFX[i].Play () ;
        }
        cubeExplosionFX[1].transform.position = position + Vector3.up;
    }
}