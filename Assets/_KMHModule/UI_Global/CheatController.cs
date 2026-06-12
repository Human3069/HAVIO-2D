using _KMH_Framework;
using _KMH_Framework.Pool;
using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace MGFramework
{
    /// <summary>
    /// ~ 키를 눌러서 CheatConsole를 열어 cheatCommand를 입력하여 Cheat를 사용할 수 있습니다. 혹은 binding된 키를 눌러서 사용할 수 있습니다.
    /// </summary>
    public class CheatController : MonoSingleton<CheatController>
    {
        [SerializeField, SerializedDictionary("KeyCode", "Command")]
        private SerializedDictionary<KeyCode, string> bindedCommandDic = new SerializedDictionary<KeyCode, string>();

        private bool _isOn = false;
        public bool IsOn
        {
            get
            {
                return _isOn;
            }
            set
            {
                if (_isOn != value)
                {
                    _isOn = value;
                    OnCheatConsoleStateChanged?.Invoke(value);
                }
            }
        }

        private bool _isNoclip = false;
        private bool IsNoclip
        {
            get
            {
                return _isNoclip;
            }
            set
            {
                if (_isNoclip != value)
                {
                    _isNoclip = value;
                    if (value == true)
                    {
                        NoclipAsync().Forget();
                    }

                    OnNoclipStateChanged?.Invoke(value);
                }
            }
        }

        private async UniTaskVoid NoclipAsync()
        {
            while (IsNoclip == true)
            {
                await UniTask.Yield();
            }
        }

        public delegate void CheatConsoleDelegate(bool isOn);
        public event CheatConsoleDelegate OnCheatConsoleStateChanged;

        public delegate void SubmitCheatCommandDelegate(string command);
        public event SubmitCheatCommandDelegate OnSubmitCheatCommand;

        public delegate void NoclipDelegate(bool isOn);
        public event NoclipDelegate OnNoclipStateChanged;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote) == true)
            {
                IsOn = !IsOn;
            }

            else if (Input.GetKeyDown(KeyCode.Insert))
            {
                Time.timeScale += 1f;
            }
            else if (Input.GetKeyDown(KeyCode.Delete))
            {
                Time.timeScale = 1f;
            }

            if (IsOn == false)
            {
                foreach (KeyValuePair<KeyCode, string> pair in bindedCommandDic)
                {
                    if (Input.GetKeyDown(pair.Key) == true)
                    {
                        if (TrySubmit(pair.Value) == true)
                        {
                            break;
                        }
                        else
                        {
                            Debug.LogError("Cheat command not found : " + pair.Value);
                        }
                    }
                }
            }
        }

        public bool TrySubmit(string inputText)
        {
#if false
            Regex giveGoldPatternRegex = new Regex(@"^give_gold_([0-9]+)$");
            Regex singlePatternRegex = new Regex(@"^give_([a-zA-Z0-9]+)_([0-9]+)$");
            Regex multiplePatternRegex = new Regex(@"^give_([a-zA-Z0-9]+)_([0-9]+)_([0-9]+)$");
        
            Match giveGoldMatch = giveGoldPatternRegex.Match(inputText);
            Match singlesMatch = singlePatternRegex.Match(inputText);
            Match multiplesMatch = multiplePatternRegex.Match(inputText);
      
            if (giveGoldMatch.Success == true)
            {
                int gaveAmount = int.Parse(giveGoldMatch.Groups[1].Value);

                GivePlayerGold(gaveAmount);
                OnSubmitCheatCommand?.Invoke(inputText);
                return true;
            }
            else if (multiplesMatch.Success == true)
            {
                string gaveTarget = multiplesMatch.Groups[1].Value;
                int gaveType = int.Parse(multiplesMatch.Groups[2].Value);
                uint gaveCount = uint.Parse(multiplesMatch.Groups[3].Value);

                OnSubmitCheatCommand?.Invoke(inputText);
                return TryIntermediator(gaveTarget, gaveType, gaveCount);
            }
            else if (singlesMatch.Success == true)
            {
                string gaveTarget = singlesMatch.Groups[1].Value;
                int gaveType = int.Parse(singlesMatch.Groups[2].Value);

                OnSubmitCheatCommand?.Invoke(inputText);
                return TryIntermediator(gaveTarget, gaveType, 1);
            }
            else if (inputText.Equals("noclip") == true)
            {
                IsNoclip = !IsNoclip;
                return true;
            }
            else if (inputText.Equals("enqueue_customer") == true)
            {
                GameManager.Instance.CustomerManager.EnqueueCustomer();
                OnSubmitCheatCommand?.Invoke(inputText);
                return true;
            }
            else if (inputText.Equals("dequeue_customer") == true)
            {
                RemoveCustomer();
                OnSubmitCheatCommand?.Invoke(inputText);
                return true;
            }
            else
            {
                Debug.LogError("잘못된 Command입니다 : " + inputText);
                return false;
            }
#endif

            return false;
        }
    }
}
