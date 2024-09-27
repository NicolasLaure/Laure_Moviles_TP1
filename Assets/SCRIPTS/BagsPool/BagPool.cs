namespace BagsPool
{
    public class BagPool : ObjectPool.ObjectPool
    {
        public static BagPool instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
        }
    }
}