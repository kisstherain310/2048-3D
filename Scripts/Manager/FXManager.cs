using UnityEngine ;

public enum FXType {
    ConfettiVFX = 0,
    BlastVFX = 1,
    JokerEffect = 2,
    BombEffect = 3,
    ReplayEffect = 4,
}
public class FXManager : MonoBehaviour 
{
    //singleton class
    public static FXManager Instance ;
    // ------- Initialization ----------------
    [SerializeField] private ParticleSystem[] cubeExplosionFX ;
    ParticleSystem.MainModule[] cubeExplosionFXMainModule ;
    void Awake () {
        Instance = this;
        cubeExplosionFXMainModule = new ParticleSystem.MainModule[cubeExplosionFX.Length];
        for (int i = 0; i < cubeExplosionFX.Length; i++)
            cubeExplosionFXMainModule[i] = cubeExplosionFX[i].main;
    }
    //  ------- Get FX ----------------
    public ParticleSystem GetFX (int number) {
        return cubeExplosionFX[number];
    }
    // ------- Play Cube Explosion FX ----------------
    public void PlayFX (Vector3 position, FXType type) {
        EditPositionFX((int)type, position);
        cubeExplosionFX[(int)type].Play () ;
    }
    public void PlayFX (Vector3 position, Color color, FXType type) {
        cubeExplosionFXMainModule[(int)type].startColor = new ParticleSystem.MinMaxGradient (color) ;
        EditPositionFX((int)type, position);
        cubeExplosionFX[(int)type].Play () ;
    }
    // ------- Edit Position FX ----------------
    private void EditPositionFX (int index, Vector3 position) {
        if (index == 0 || index == 1 || index == 3 || index == 4) {
            cubeExplosionFX[index].transform.position = position + Vector3.up * 0.4f;
        }
        else if (index == 2) {
            cubeExplosionFX[index].transform.position = position + new Vector3 (0, 1, -2) ;
        } else if (index == 4){
            cubeExplosionFX[index].transform.position = position + Vector3.up * 4;
        }
    }
}