using System;
using System.Collections.Generic;

namespace compte.Models.Accounts.Enum
{
    public sealed class AccountTypeEnum {

        public string Value {get; private set;}

        public static readonly AccountTypeEnum AGENCY = new AccountTypeEnum ("agency");
        public static readonly AccountTypeEnum CANDIDATE = new AccountTypeEnum ("candidate");
        public static readonly AccountTypeEnum COMPANY = new AccountTypeEnum ("company");        
        public static readonly AccountTypeEnum COLLABORATOR = new AccountTypeEnum ("collaborator");        
    
        public AccountTypeEnum(string value) {
            this.Value = value;
        }

        /// <summary>Retourne la liste de tous les elements de l'enum</summary>
        public static List<AccountTypeEnum> ToList()
        {
            return new List<AccountTypeEnum> {
                AGENCY,
                CANDIDATE,
                COMPANY,
                COLLABORATOR
            };
        }

        public static AccountTypeEnum ValueOf(string contact)
        {
            foreach(AccountTypeEnum c in ToList())
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