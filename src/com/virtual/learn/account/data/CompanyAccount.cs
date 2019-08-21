using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;
using lug.String.Encrypt;
using Microsoft.Extensions.Configuration;

namespace compte.Models.Accounts
{ 
    /// <summary>
    /// ressource retournee pour un compte de type entreprise
    /// </summary>
    [BsonIgnoreExtraElements]
    public partial class CompanyAccount : Account
    { 
        /// <summary>
        /// raison sociale
        /// </summary>
        /// <value>raison sociale</value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Numéro SIRET
        /// </summary>
        /// <value>numéro Siret d'une entreprise</value>
        [Required]
        public string Siret { get; set; }
        
        /// <summary>
        /// identifiant
        /// </summary>
        /// <value>identifiant</value>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Secteur d&#39;activite de l&#39;entreprise
        /// </summary>
        /// <value>Secteur d&#39;activite de l&#39;entreprise</value>
        public string Sector { get; set; }

        /// <summary>
        /// Gets or Sets Adress
        /// </summary>
        [Required]
        public Adress Adress { get; set; }

        /// <summary>
        /// Gets or Sets Contact
        /// </summary>
        public Contact Contact { get; set; }

        /// <summary>
        /// Gets or Sets Partners
        /// </summary>
        public Partners Partners { get; set; }

        /// <summary>
        /// Gets or Sets Responsible
        /// </summary>
        public Responsible Responsible { get; set; }

        /// <summary>
        /// Gets or Sets Modules
        /// </summary>
        public CompanyModules Modules { get; set; }

        /// <summary>
        /// Gets or Sets Fund
        /// </summary>
        public Fund Fund { get; set; }

        public CompanyAccount()
        {}

        public CompanyAccount(CompanyAccount baseAccount)
        {
            this.AccountType = baseAccount.AccountType;
            this.Adress = baseAccount.Adress;
            this.Contact = baseAccount.Contact;
            this.Fund = baseAccount.Fund;
            this.Modules = baseAccount.Modules;
            this.Name = baseAccount.Name;
            this.Partners = baseAccount.Partners;
            this.Responsible = baseAccount.Responsible;
            this.Sector = baseAccount.Sector;
            this.UserId = baseAccount.UserId;
            this.Siret = baseAccount.Siret;
        }

        /// <summary> Methode de decryption des comptes
        /// <returns>Account : liste des comptes decryptes</returns>
        public override Account DecryptAccount(IConfiguration configuration)
        {
            StringEncryptCriteria criteria = new StringEncryptCriteria{
                KeySize = int.Parse(configuration["Encrypt:KeySize"]),
                DerivationIterations = int.Parse(configuration["Encrypt:DerivationIterations"]),
                PassPhrase = configuration["Encrypt:PassPhrase"]
            };
            CompanyAccount decryptedAccount = new CompanyAccount(this);
            decryptedAccount.Adress = DecryptAdress(Adress, criteria);
            decryptedAccount.Contact = DecryptContact(Contact, criteria);
            decryptedAccount.Name = DecryptPart(Name, criteria);            
            decryptedAccount.Siret = DecryptPart(Siret, criteria);
            return decryptedAccount;

        }

        /// <summary> Methode de decryption des comptes
        /// <returns> Account : liste des comptes decryptes</returns>
        public override Account EncryptAccount(IConfiguration configuration)
        {
            StringEncryptCriteria criteria = new StringEncryptCriteria{
                KeySize = int.Parse(configuration["Encrypt:KeySize"]),
                DerivationIterations = int.Parse(configuration["Encrypt:DerivationIterations"]),
                PassPhrase = configuration["Encrypt:PassPhrase"]
            };
            CompanyAccount encryptedAccount = new CompanyAccount(this);
            encryptedAccount.Adress = EncryptAdress(Adress, criteria);
            encryptedAccount.Contact = EncryptContact(Contact, criteria);
            encryptedAccount.Name = EncryptPart(Name, criteria);
            encryptedAccount.Siret = EncryptPart(Siret, criteria);
            return encryptedAccount;
        }

    }
}
