using UnityEngine;
using System.Collections;

namespace Completed
{

    public class Wall : MonoBehaviour
    {

        public int hp = 3;

        private SpriteRenderer spriteRenderer;

        // Use this for initialization
        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void DamageWall(int loss)
        {
            hp -= loss;

            if (hp <= 0)
                gameObject.SetActive(false);

        }
    }
}
