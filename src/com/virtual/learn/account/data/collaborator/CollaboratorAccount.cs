using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using compte.Models.Accounts;
using lug.String.Encrypt;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace compte.Accounts.Collaborator
{
    /// <summary>Classe de compte collaborateurs</summary>
    [BsonIgnoreExtraElements]
    public class CollaboratorAccount : Account
    {
        /// <summary>Identifiant du compte</summary>
        [JsonProperty("id")]
        public string UserId {get; set;}
        /// <summary>Prenom</summary>
        [JsonProperty("firstName")]
        [Required]
        public string FirstName {get; set;}
        /// <summary>Nom de famille</summary>
        [Required]
        [JsonProperty("lastName")]
        public string LastName {get; set;}
        /// <summary>Contact du collaborateur</summary>
        [Required]
        [JsonProperty("contact")]
        public Contact Contact {get; set;}
        /// <summary>Liste des comptes referents pour un compte collaborateur</summary>
        [Required]
        [JsonProperty("referents")]
        public List<ReferentAccount> Referents {get; set;}

        public CollaboratorAccount() : base()
        {
        }

        public CollaboratorAccount(CollaboratorAccount baseAccount)
        {
            this.UserId = baseAccount.UserId;
            this.LastName = baseAccount.LastName;
            this.FirstName = baseAccount.FirstName;
            this.AccountType = baseAccount.AccountType;
            this.Contact = baseAccount.Contact;
            this.Referents = baseAccount.Referents;
        }

        public override Account EncryptAccount(IConfiguration configuration)
        {
            StringEncryptCriteria criteria = new StringEncryptCriteria{
                    KeySize = int.Parse(configuration["Encrypt:KeySize"]),
                    DerivationIterations = int.Parse(configuration["Encrypt:DerivationIterations"]),
                    PassPhrase = configuration["Encrypt:PassPhrase"]
                };
            CollaboratorAccount encryptedAccount = new CollaboratorAccount(this);
            encryptedAccount.FirstName = EncryptPart(FirstName, criteria);
            encryptedAccount.Contact = EncryptContactCollaborator(Contact, criteria);
            encryptedAccount.LastName = EncryptPart(LastName, criteria);
            return encryptedAccount;
        }

        public override Account DecryptAccount(IConfiguration configuration)
        {
            StringEncryptCriteria criteria = new StringEncryptCriteria{
                    KeySize = int.Parse(configuration["Encrypt:KeySize"]),
                    DerivationIterations = int.Parse(configuration["Encrypt:DerivationIterations"]),
                    PassPhrase = configuration["Encrypt:PassPhrase"]
                };
            CollaboratorAccount decryptedAccount = new CollaboratorAccount(this);
            decryptedAccount.FirstName = DecryptPart(FirstName, criteria);
            decryptedAccount.Contact = DecryptContactCollaborator(Contact, criteria);
            decryptedAccount.LastName = DecryptPart(LastName, criteria);
            return decryptedAccount;
        }

        /// <summary> Chiffrage du contact collaborateur</summary>
        /// <param name="contact">Contact a chiffrer</param>
        /// <param name="criteria">Criterias de chiffrage</param>
        /// <return>ContactCollaborator : contact chiffre</return>
        private Contact EncryptContactCollaborator(Contact contact, StringEncryptCriteria criteria)
        {
            Contact encriptedContact = new Contact();
            encriptedContact.MailAdress = EncryptPart(contact.MailAdress, criteria);
            encriptedContact.PhoneNumber = EncryptPart(contact.PhoneNumber, criteria);
            return encriptedContact;
        }

        /// <summary> Dechiffrage du contact collaborateur</summary>
        /// <param name="contact">Contact a dechiffrer</param>
        /// <param name="criteria">Criterias de dechiffrage</param>
        /// <return>ContactCollaborator : contact dechiffre</return>
        private Contact DecryptContactCollaborator(Contact contact, StringEncryptCriteria criteria)
        {
            Contact decriptedContact = new Contact();
            decriptedContact.MailAdress = DecryptPart(contact.MailAdress, criteria);
            decriptedContact.PhoneNumber = DecryptPart(contact.PhoneNumber, criteria);
            return decriptedContact;
        }

    }
}