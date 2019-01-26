public interface IPlayerWatcher {
    void Colliding(float timeDelta, PlayerBehaviour player);
    void StopColliding(PlayerBehaviour player);
}
