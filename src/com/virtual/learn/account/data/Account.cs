using System;
using System.IO;
using lug.String.Encrypt;
using Microsoft.Extensions.Configuration;

namespace compte.Models.Accounts
{ 
    /// <summary>
    /// Classe parente des comptes
    /// </summary>
    public abstract class Account
    { 
        public string AccountType {get;set;}

        public Account()
        {
        }

        /// <summary>Decryption d'une adresse</summary>
        /// <param name="baseAdress">Adresse encryptee a dechiffrer</param>
        /// <param name="criteria">Criteres de decryptage</param>
        internal Adress DecryptAdress(Adress baseAdress, StringEncryptCriteria criteria)
        {
            Adress decryptedAdress = new Adress();
            decryptedAdress.Complement1 = DecryptPart(baseAdress.Complement1, criteria);
            decryptedAdress.Complement2 = DecryptPart(baseAdress.Complement2, criteria);
            decryptedAdress.Country = DecryptPart(baseAdress.Country, criteria);
            decryptedAdress.Number = DecryptPart(baseAdress.Number, criteria);
            decryptedAdress.Label = DecryptPart(baseAdress.Label, criteria);
            decryptedAdress.PostalCode = DecryptPart(baseAdress.PostalCode, criteria);
            decryptedAdress.Town = DecryptPart(baseAdress.Town, criteria);

            return decryptedAdress;
        }

        /// <summary>Decryption d'un contact</summary>
        /// <param name="baseAdress">Contact encryptee a dechiffrer</param>
        /// <param name="criteria">Criteres de decryptage</param>
        internal Contact DecryptContact(Contact baseContact, StringEncryptCriteria criteria)
        {
            Contact decryptedContact = new Contact();
            decryptedContact.MailAdress = DecryptPart(baseContact.MailAdress, criteria);
            decryptedContact.PhoneNumber = DecryptPart(baseContact.PhoneNumber, criteria);
            decryptedContact.WebPage = DecryptPart(baseContact.WebPage, criteria);

            decryptedContact.PreferredContact = baseContact.PreferredContact;

            return decryptedContact;
        }

        /// <summary>Decryptage d'une chaine de caracteres</summary>
        /// <param name="criteria"></param>
        /// <param name="elementToDecipher"></param>
        /// <returns>string : la chaine dechiffree</returns>
        internal string DecryptPart(string elementToDecipher, StringEncryptCriteria criteria)
        {
            if (!string.IsNullOrEmpty(elementToDecipher))
            {
                criteria.Text = elementToDecipher;
                return StringEncryptHelper.Decrypt(criteria);
            }
            return null;
        }


        /// <summary>Encryption d'une adresse</summary>
        /// <param name="baseAdress">Adresse a chiffrer</param>
        /// <param name="criteria">Criteres de chiffrage</param>
        internal Adress EncryptAdress(Adress baseAdress, StringEncryptCriteria criteria)
        {
            Adress encryptedAdress = new Adress();
            encryptedAdress.Complement1 = EncryptPart(baseAdress.Complement1, criteria);
            encryptedAdress.Complement2 = EncryptPart(baseAdress.Complement2, criteria);
            encryptedAdress.Country = EncryptPart(baseAdress.Country, criteria);
            encryptedAdress.Number = EncryptPart(baseAdress.Number, criteria);
            encryptedAdress.Label = EncryptPart(baseAdress.Label, criteria);
            encryptedAdress.PostalCode = EncryptPart(baseAdress.PostalCode, criteria);
            encryptedAdress.Town = EncryptPart(baseAdress.Town, criteria);

            return encryptedAdress;
        }

        /// <summary>Encryption d'un contact</summary>
        /// <param name="baseAdress">Contact a chiffrer</param>
        /// <param name="criteria">Criteres de chiffrage</param>
        internal Contact EncryptContact(Contact baseContact, StringEncryptCriteria criteria)
        {
            Contact encryptedContact = new Contact();
            encryptedContact.MailAdress = EncryptPart(baseContact.MailAdress, criteria);
            encryptedContact.PhoneNumber = EncryptPart(baseContact.PhoneNumber, criteria);
            encryptedContact.WebPage = EncryptPart(baseContact.WebPage, criteria);
            
            encryptedContact.PreferredContact = baseContact.PreferredContact;

            return encryptedContact;
        }

        /// <summary>Encryption d'une chaine de caracteres</summary>
        /// <param name="criteria"></param>
        /// <param name="elementToCipher"></param>
        /// <returns>string : la chaine chiffree</returns>
        internal string EncryptPart(string elementToCipher, StringEncryptCriteria criteria)
        {
            if (!string.IsNullOrEmpty(elementToCipher))
            {
                criteria.Text = elementToCipher;
                return StringEncryptHelper.Encrypt(criteria);
            }
            return null;
        }

        public abstract Account DecryptAccount(IConfiguration configuration);

        public abstract Account EncryptAccount(IConfiguration configuration);
    }
}