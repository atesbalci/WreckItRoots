using UnityEngine;
using WreckItRoots.Models;

namespace WreckItRoots.Behaviours
{
    public class RootTipInput : MonoBehaviour
    {
        private IRootTip _rootTip;

        private void Start()
        {
            _rootTip = GetComponent<IRootTip>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rootTip.RootDown();
            }
            
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _rootTip.Maneuver(-1f);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _rootTip.Maneuver(1f);
            }
        }
    }
}