using System;
using System.Collections.Generic;
using System.Linq;
using GG.Signals;
using UnityEngine;
using Zenject;

namespace GG.Commands.SignalCommand
{
    public class StringChecker : IInitializable, IDisposable
    {
        [Inject]
        private SignalBus _signalBus;

        private TextAsset _texter;
        private List<string> _title;
        public bool _isvalid;


        private string _validstring;

        public StringChecker(TextAsset texter) => this._texter = texter;

        public void Initialize()
        {
            _title = new List<string>(Converter());
            _signalBus.Subscribe<OnStringComplate>(Callback);
        }


        private void Callback(OnStringComplate obj)
        {
            if (string.IsNullOrEmpty(obj.String))
            {
                _isvalid = false;
                return;

            }
            var gettedValue = obj.String.ToLowerInvariant();
            // _validstring = _title.Find(x => string.Equals(x, gettedValue, StringComparison.CurrentCultureIgnoreCase));
            var index = _title.BinarySearch(gettedValue, StringComparer.OrdinalIgnoreCase);
            _validstring = index >= 0 ? _title[index] : string.Empty;
            if (gettedValue == _validstring)
            {
                _isvalid = true;
            }
            else
            {
                _isvalid = false;
            }
        }

        private List<string> Converter()
        {
            var lines = _texter.text.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();
            return lines;
        }


        public bool IsValid()
        {
            return _isvalid;
        }


        public void Dispose()
        {
            _texter = null;
            _signalBus.Unsubscribe<OnStringComplate>(Callback);
        }
    }
}