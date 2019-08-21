using System;
using System.Collections.Generic;

namespace compte.Models.Accounts
{
    public sealed class StatusFiliationEnum {

        public string Text {get; private set;}
        public string Value {get; private set;}

        public static readonly StatusFiliationEnum CREATED = new StatusFiliationEnum ("CREATED", "Téléphone");
        public static readonly StatusFiliationEnum VALIDATED = new StatusFiliationEnum ("VALIDATED", "Mail");
        public static readonly StatusFiliationEnum REFUSED = new StatusFiliationEnum ("REFUSED", "Site web");        
        public static readonly StatusFiliationEnum DELETED = new StatusFiliationEnum ("DELETED", "Site web");        
    
        public StatusFiliationEnum(string value, string text) {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>Retourne la liste de tous les elements de l'enum</summary>
        public static List<StatusFiliationEnum> ToList()
        {
            return new List<StatusFiliationEnum> {
                CREATED,
                VALIDATED,
                REFUSED,
                DELETED
            };
        }

        public static StatusFiliationEnum ValueOf(string contact)
        {
            foreach(StatusFiliationEnum c in ToList())
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