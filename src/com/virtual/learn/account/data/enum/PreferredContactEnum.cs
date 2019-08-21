using System;
using System.Collections.Generic;

namespace compte.Models.Accounts
{
    public sealed class PreferredContactEnum {

        public string Text {get; private set;}
        public string Value {get; private set;}

        public static readonly PreferredContactEnum PHONE = new PreferredContactEnum ("PHONE", "Téléphone");
        public static readonly PreferredContactEnum MAIL = new PreferredContactEnum ("MAIL", "Mail");
        public static readonly PreferredContactEnum WEB = new PreferredContactEnum ("WEB", "Site web");        
    
        public PreferredContactEnum(string value, string text) {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>Retourne la liste de tous les elements de l'enum</summary>
        public static List<PreferredContactEnum> ToList()
        {
            return new List<PreferredContactEnum> {
                PHONE,
                MAIL,
                WEB
            };
        }

        public static PreferredContactEnum ValueOf(string contact)
        {
            foreach(PreferredContactEnum c in ToList())
            {
                if (c.Value.Equals(contact))
                {
                    return c;
                }
            }
            return null;
        }
        
    }
}