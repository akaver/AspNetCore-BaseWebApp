using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Translation
    {
        public int TranslationId { get; set; }

        public string Value { get; set; }

        private string _culture;
        [MaxLength(length: 12)]
        public string Culture
        {
            get => _culture;
            // et-EE-võru => et,  foobar => foobar
            set => _culture = value.Split('-')[0];
        }
    }
}
