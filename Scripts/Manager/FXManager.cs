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
    public ParticleSystem GetFX (int number) {
        return cubeExplosionFX[number];
    }
    // ------- Play Cube Explosion FX ----------------
    public void PlayCubeExplosionFX (Vector3 position, Color color, int number) {
        EditPositionFX(number, position);
        cubeExplosionFXMainModule[number].startColor = new ParticleSystem.MinMaxGradient (color) ;
        cubeExplosionFX[number].Play () ;
    }
    // ------- Edit Position FX ----------------
    private void EditPositionFX (int number, Vector3 position) {
        if (number == 0) {
            cubeExplosionFX[number].transform.position = position + Vector3.up ;
        }
        else if (number == 1) {
            cubeExplosionFX[number].transform.position = position + Vector3.up ;
        }
        else if (number == 2) {
            cubeExplosionFX[number].transform.position = position + new Vector3 (0, 1, -2) ;
        }
    }
}