using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.Extensions.Configuration;
using lug.String.Encrypt;

namespace compte.Models.Accounts
{ 
    /// <summary>
    /// Ressource retournee pour un compte agence
    /// </summary>
    [BsonIgnoreExtraElements]
    public partial class AgencyAccount : Account
    { 
        /// <summary>
        /// Raison sociale
        /// </summary>
        /// <value>Raison sociale</value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Numéro SIRET
        /// </summary>
        /// <value>numéro Siret d'une entreprise</value>
        [Required]
        public string Siret { get; set; }

        /// <summary>
        /// identifiant du compte
        /// </summary>
        /// <value>identifiant du compte</value>
        [Required]
        public String UserId { get; set; }

        /// <summary>
        /// Secteur d&#39;activite de l&#39;agence
        /// </summary>
        /// <value>Secteur d&#39;activite de l&#39;agence</value>
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
        /// Gets or Sets Successes
        /// </summary>
        public List<String> Successes { get; set; }

        /// <summary>
        /// Gets or Sets Responsible
        /// </summary>
        public Responsible Responsible { get; set; }

        public AgencyAccount()
        {}

        public AgencyAccount(AgencyAccount baseAccount)
        {
            this.AccountType = baseAccount.AccountType;
            this.Adress = baseAccount.Adress;
            this.Contact = baseAccount.Contact;
            this.Name = baseAccount.Name;
            this.Partners = baseAccount.Partners;
            this.Responsible = baseAccount.Responsible;
            this.Sector = baseAccount.Sector;
            this.Successes = baseAccount.Successes;
            this.UserId = baseAccount.UserId;
            this.Siret = baseAccount.Siret;
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AccountAgencyResponse {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  Sector: ").Append(Sector).Append("\n");
            sb.Append("  Adress: ").Append(Adress).Append("\n");
            sb.Append("  Contact: ").Append(Contact).Append("\n");
            sb.Append("  Partners: ").Append(Partners).Append("\n");
            sb.Append("  Successes: ").Append(Successes).Append("\n");
            sb.Append("  Responsible: ").Append(Responsible).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary> Methode de decryption de compte</summary>
        /// <paramref name="account"> compte a decrypter</paramref>
        /// <returns>Account : compte decrypte</returns>
       public override Account DecryptAccount(IConfiguration configuration)
        {
            StringEncryptCriteria criteria = new StringEncryptCriteria{
                    KeySize = int.Parse(configuration["Encrypt:KeySize"]),
                    DerivationIterations = int.Parse(configuration["Encrypt:DerivationIterations"]),
                    PassPhrase = configuration["Encrypt:PassPhrase"]
                };
            AgencyAccount decryptedAccount = new AgencyAccount(this);
            decryptedAccount.Adress = DecryptAdress(Adress, criteria);
            decryptedAccount.Contact = DecryptContact(Contact, criteria);
            decryptedAccount.Name = DecryptPart(Name, criteria);
            decryptedAccount.Siret = DecryptPart(Siret, criteria);
            return decryptedAccount;

        }

        /// <summary> Methode de chiffrage de compte</summary>
        /// <paramref name="account"> compte a chiffrer</paramref>
        /// <returns>Account : compte chiffre</returns>
       public override Account EncryptAccount(IConfiguration configuration)
        {
            StringEncryptCriteria criteria = new StringEncryptCriteria{
                    KeySize = int.Parse(configuration["Encrypt:KeySize"]),
                    DerivationIterations = int.Parse(configuration["Encrypt:DerivationIterations"]),
                    PassPhrase = configuration["Encrypt:PassPhrase"]
                };
            AgencyAccount encryptedAccount = new AgencyAccount(this);
            encryptedAccount.Adress = EncryptAdress(Adress, criteria);
            encryptedAccount.Contact = EncryptContact(Contact, criteria);
            encryptedAccount.Name = EncryptPart(Name, criteria);
            encryptedAccount.Siret = EncryptPart(Siret, criteria);
            return encryptedAccount;

        }

    }
}
