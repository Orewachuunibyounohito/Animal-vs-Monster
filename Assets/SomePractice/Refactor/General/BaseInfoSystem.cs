using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace Refactoring
{
    public abstract class BaseInfoSystem
    {
        public int  Count => _info.Count;
        public bool Skip{ get; set; }

        protected List<string>    _info;
        protected TextMeshProUGUI _infoText;

        public BaseInfoSystem(TextMeshProUGUI infoText){
            _info     = new List<string>();
            _infoText = infoText;
            Skip      = false;
        }

        public void Add(string line)       => _info.Add(line);
        public void Add(List<string> info) => _info.AddRange(info);

        public abstract void DisplayInfo();

        // Can use Coroutine to print info
        protected abstract IEnumerator PlayTask();
    }
}
