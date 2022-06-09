namespace Scene
{
    public class GameScene : BaseScene
    {
        public override void Clear()
        {
            
        }

        protected override void Initialize()
        {
            _scene = Define.Scene.Game;
        }
    }
}