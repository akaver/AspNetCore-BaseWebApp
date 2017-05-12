using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Domain
{
    public class MultiLangString
    {
        public int MultiLangStringId { get; set; }

        // default value, when no translation is found
        public string Value { get; set; }

        public string Owner { get; set; }

        public virtual List<Translation> Translations { get; set; } = new List<Translation>();

        public static string DefaultCulture { get; set; } = "en";

        #region constructors
        public MultiLangString()
        {
            
        }

        public MultiLangString(string defaultValue) : base()
        {
            Value = defaultValue;
        }

        public MultiLangString(string value, string culture) : this(value)
        {
            SetTranslation(value, culture);
        }
        public MultiLangString(string value, string culture, string owner) : this(value, culture)
        {
            Owner = owner;
        }

        #endregion

        public void SetTranslation(string value, string culture)
        {
            // force it to be just language
            culture = culture.Split('-')[0];

            // should we overwrite default value
            if (culture == DefaultCulture.Split('-')[0])
            {
                Value = value;
            }

            var found = Translations
                .FirstOrDefault(a => a.Culture.ToLower().StartsWith(culture.ToLower()));
            if (found == null)
            {
                Translations.Add(new Translation()
                {
                    Culture = culture,
                    Value = value
                });
            }
            else
            {
                found.Value = value;
            }
        }

        public void SetTranslation(string value)
        {
            SetTranslation(value, CultureInfo.CurrentCulture.Name);
        }


        public string TranslateToCulture(CultureInfo culture = null)
        {
            culture = culture ?? CultureInfo.CurrentCulture;
            var translation = Translations
                .FirstOrDefault(a => culture.Name.ToLower().StartsWith(a.Culture.ToLower()));
            return translation == null ? Value : translation.Value;
        }

        public override string ToString()
        {
            return TranslateToCulture();
        }
    }
}
