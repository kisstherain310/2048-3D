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
    public void PlayFX (Vector3 position, int index) {
        EditPositionFX(index, position);
        cubeExplosionFX[index].Play () ;
    }
    public void PlayFX (Vector3 position, Color color, int index) {
        cubeExplosionFXMainModule[index].startColor = new ParticleSystem.MinMaxGradient (color) ;
        EditPositionFX(index, position);
        cubeExplosionFX[index].Play () ;
    }
    // ------- Edit Position FX ----------------
    private void EditPositionFX (int index, Vector3 position) {
        if (index == 0 || index == 1 || index == 3) {
            cubeExplosionFX[index].transform.position = position + Vector3.up ;
        }
        else if (index == 2) {
            cubeExplosionFX[index].transform.position = position + new Vector3 (0, 1, -2) ;
        }
    }
}