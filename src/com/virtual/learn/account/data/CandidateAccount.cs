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
using compte.Json;

namespace compte.Models.Accounts
{ 
    /// <summary>
    /// ressource retournee pour un compte candidat
    /// </summary>
    [BsonIgnoreExtraElements]
    public partial class CandidateAccount : Account
    { 
        /// <summary>
        /// civilite (m., mme, etc.)
        /// </summary>
        /// <value>civilite (m., mme, etc.)</value>
        [Required]
        public string Civility { get; set; }

        /// <summary>
        /// prenom
        /// </summary>
        /// <value>prenom</value>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// nom de famille
        /// </summary>
        /// <value>nom de famille</value>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets Adress
        /// </summary>
        [Required]
        public Adress Adress { get; set; }

        /// <summary>
        /// identifiant
        /// </summary>
        /// <value>identifiant</value>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// date de disponibilite du candidat
        /// </summary>
        /// <value>date de disponibilite du candidat</value>
        [Required]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? DispoDate { get; set; }

        /// <summary>
        /// Gets or Sets Disability
        /// </summary>
        public Disability Disability { get; set; }

        /// <summary>
        /// Gets or Sets Contact
        /// </summary>
        public Contact Contact { get; set; }

        /// <summary>
        /// Gets or Sets Skills
        /// </summary>
        public Modules Skills { get; set; }

        /// <summary>
        /// Gets or Sets Modules
        /// </summary>
        public Modules Modules { get; set; }

        /// <summary>
        /// Gets or Sets Mobility
        /// </summary>
        public Mobility Mobility { get; set; }

        /// <summary>
        /// Gets or Sets Course
        /// </summary>
        public Course Course { get; set; }

        /// <summary>
        /// Gets or Sets Preferences
        /// </summary>
        public Preferences Preferences { get; set; }

        /// <summary>
        /// Gets or Sets Partners
        /// </summary>
        public Partners Partners { get; set; }

        public CandidateAccount()
        {}

        public CandidateAccount(CandidateAccount baseAccount)
        {
            this.AccountType = baseAccount.AccountType;
            this.Adress = baseAccount.Adress;
            this.Civility = baseAccount.Civility;
            this.Contact = baseAccount.Contact;
            this.Course = baseAccount.Course;
            this.Disability = baseAccount.Disability;
            this.DispoDate = baseAccount.DispoDate;
            this.FirstName = baseAccount.FirstName;
            this.LastName = baseAccount.LastName;
            this.Mobility = baseAccount.Mobility;
            this.Modules = baseAccount.Modules;
            this.Partners = baseAccount.Partners;
            this.Preferences = baseAccount.Preferences;
            this.Skills = baseAccount.Skills;
            this.UserId = baseAccount.UserId;
        }

        /// <summary> Methode de decryption des comptes
        /// <paramref name="accounts"> Liste des comptes a decrypter</paramref>
        /// <returns>List<Account> liste des comptes decryptes</returns>
        public override Account DecryptAccount(IConfiguration configuration)
        {
            StringEncryptCriteria criteria = new StringEncryptCriteria{
                KeySize = int.Parse(configuration["Encrypt:KeySize"]),
                DerivationIterations = int.Parse(configuration["Encrypt:DerivationIterations"]),
                PassPhrase = configuration["Encrypt:PassPhrase"]
            };
            CandidateAccount decryptedAccount = new CandidateAccount(this);
            decryptedAccount.Adress = DecryptAdress(Adress, criteria);
            decryptedAccount.Contact = DecryptContact(Contact, criteria);

            decryptedAccount.Civility = DecryptPart(Civility, criteria);
            decryptedAccount.FirstName = DecryptPart(FirstName, criteria);
            decryptedAccount.LastName = DecryptPart(LastName, criteria);

            return decryptedAccount;

        }

        /// <summary> Methode de decryption des comptes
        /// <paramref name="accounts"> Liste des comptes a decrypter</paramref>
        /// <returns>List<Account> liste des comptes decryptes</returns>
        public override Account EncryptAccount(IConfiguration configuration)
        {
            StringEncryptCriteria criteria = new StringEncryptCriteria{
                KeySize = int.Parse(configuration["Encrypt:KeySize"]),
                DerivationIterations = int.Parse(configuration["Encrypt:DerivationIterations"]),
                PassPhrase = configuration["Encrypt:PassPhrase"]
            };
            CandidateAccount encryptedAccount = new CandidateAccount(this);
            encryptedAccount.Adress = EncryptAdress(Adress, criteria);
            encryptedAccount.Contact = EncryptContact(Contact, criteria);
            encryptedAccount.Civility = EncryptPart(Civility, criteria);
            encryptedAccount.FirstName = EncryptPart(FirstName, criteria);
            encryptedAccount.LastName = EncryptPart(LastName, criteria);
            return encryptedAccount;
        }

    }
}
