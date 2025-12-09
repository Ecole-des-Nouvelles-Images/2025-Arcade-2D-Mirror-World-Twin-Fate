namespace Player
{
    public static class StaticData
    {
        private static float _health = 100f;
   
        public static void TakeDamage(int damage)
        {
            _health-= damage;
      
        }
        public static bool IsDead()
        {
            if (_health <= 0) return true;
            else return false;
        }
    }
}