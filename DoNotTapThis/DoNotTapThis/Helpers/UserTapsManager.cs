using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNotTapThis.Helpers
{
    public class UserTapsManager
    {
        private readonly int _limit;

        public UserTapsManager(int limit = int.MaxValue)
        {
            _limit = limit;
        }

        private int _tapCount;

        public int TapCount
        {
            get { return _tapCount; }
            set
            {
                if (value < _limit)
                {
                    _tapCount = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Value cannot be greater than limit.");
                }
            }
        }

        public void AddTap()
        {
            TapCount++;
        }

        public string GetFormattedTaps()
        {
            return TapCount.ToString("##,###");
        }
    }
}
