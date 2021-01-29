using System;

/// <summary>
/// The DataScene class, which gets uploaded to the Firebase Database
/// </summary>

[Serializable] // This makes the class able to be serialized into a JSON
public class DataScene
{
    public double CreationTime;
    public int[] ParticlesOnBuffetTable;
    public ParticleOnSceneClass[] ParticlesInScene;

    public DataScene(double CreationTime, int[] BuffetTableParticles, ParticleOnSceneClass[] Particles)
    {
        this.CreationTime = CreationTime;
        this.ParticlesOnBuffetTable = BuffetTableParticles;
        this.ParticlesInScene = Particles;
    }
}

[Serializable]
public class ParticleOnSceneClass
{
    public int ID;
    public float x;
    public float y;

    public ParticleOnSceneClass(int ID, float x, float y)
    {
        this.ID = ID;
        this.x = x;
        this.y = y;
    }
}