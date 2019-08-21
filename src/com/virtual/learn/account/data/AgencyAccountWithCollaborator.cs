using System.Collections.Generic;
using compte.Accounts.Collaborator;
using lug.String.Encrypt;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace compte.Models.Accounts
{ 
    /// <summary>
    /// Resource used to get informations for an agency account
    /// </summary>
    [BsonIgnoreExtraElements]
    public partial class AgencyAccountWithCollaborator : AgencyAccount
    {
        /// <summary>List the collaborators of the agency</summary>
        [JsonProperty("collaborators")]
        public List<CollaboratorAccount> Collaborators {get; set;}

        /// <summary>Default constructor</summary>
        public AgencyAccountWithCollaborator() : base()
        {}

        /// <summary>Constructor used for cloning</summary>
        public AgencyAccountWithCollaborator(AgencyAccountWithCollaborator baseAccount) : base(baseAccount)
        {
            this.Collaborators = baseAccount.Collaborators;
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
            AgencyAccountWithCollaborator decryptedAccount = new AgencyAccountWithCollaborator(this);
            decryptedAccount.Adress = DecryptAdress(Adress, criteria);
            decryptedAccount.Contact = DecryptContact(Contact, criteria);
            decryptedAccount.Name = DecryptPart(Name, criteria);
            decryptedAccount.Siret = DecryptPart(Siret, criteria);

            List<CollaboratorAccount> decryptedCollaborators =new List<CollaboratorAccount>();
            foreach (CollaboratorAccount collab in this.Collaborators) {
                decryptedCollaborators.Add((CollaboratorAccount) collab.DecryptAccount(configuration));
            }
            decryptedAccount.Collaborators = decryptedCollaborators;

            return decryptedAccount;

        }

    }
}