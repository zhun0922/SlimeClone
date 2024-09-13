
public interface IFactoryInterface {

    //public const float MaxSpawnDelay = 1.4f; //..?
    public float curSpawnDelay { get; set; }
    public void Spawn();
}
