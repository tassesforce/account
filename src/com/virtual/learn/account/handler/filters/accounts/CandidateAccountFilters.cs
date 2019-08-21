using System.Collections.Generic;
using System.Text;
using compte.Accounts.List;
using compte.Models.Accounts;
using lug.String.Encrypt;
using Microsoft.Extensions.Configuration;

namespace compte.FIlters.Accounts
{
    public class CandidateAccountFilters<T> : AccountFilters<T> where T : Account
    {
        public IList<T> FilterAccounts(IEnumerable<T> comptes, ListAccountsRequest criterias, IConfiguration configuration)
        {
            IList<T> filteredAccounts = new List<T>();
            StringEncryptCriteria decryptCriteria = new StringEncryptCriteria{
                KeySize = int.Parse(configuration["Encrypt:KeySize"]),
                DerivationIterations = int.Parse(configuration["Encrypt:DerivationIterations"]),
                PassPhrase = configuration["Encrypt:PassPhrase"]
            };
            foreach (T account in comptes)
            {
                if (matchName(((CandidateAccount)(Account)account), criterias, decryptCriteria) && matchTown(((CandidateAccount)(Account)account), criterias, decryptCriteria)
                    && matchPostalCode(((CandidateAccount)(Account)account), criterias, decryptCriteria) && matchCounty(((CandidateAccount)(Account)account), criterias, decryptCriteria))
                {
                    filteredAccounts.Add(account);
                }
            }
            return filteredAccounts;
        }

        /// <summary>Verifie si le nom du compte correspond au nom en critere</summary>
        /// <param name="account">compte a verifier</param>
        /// <param name="criterias">Criteres a appliquer</param>
        /// <param name="decryptCriteria">Criteres de déchiffrage</param>
        /// <returns>bool : true si critere vide ou correspondant, false sinon</returns>
        private bool matchName(CandidateAccount account, ListAccountsRequest criterias, StringEncryptCriteria decryptCriteria)
        {

            decryptCriteria.Text = account.FirstName;
            string firstName = StringEncryptHelper.Decrypt(decryptCriteria);
            decryptCriteria.Text = account.LastName;
            string lastName = StringEncryptHelper.Decrypt(decryptCriteria);
            // si le nom du compte ne contient pas le nom en critere
            if (!string.IsNullOrEmpty(criterias.LastName) && !lastName.ToUpper().Contains(criterias.LastName.ToUpper()))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(criterias.FirstName) && !firstName.ToUpper().Contains(criterias.FirstName.ToUpper()))
            {
                return false;
            }
            return true;
        }

        /// <summary>Verifie si la ville du compte correspond a celle passee en critere</summary>
        /// <param name="account">compte a verifier</param>
        /// <param name="criterias">Criteres a appliquer</param>
        /// <param name="decryptCriteria">Criteres de déchiffrage</param>
        /// <returns>bool : true si critere vide ou correspondant, false sinon</returns>
        private bool matchTown(CandidateAccount account, ListAccountsRequest criterias, StringEncryptCriteria decryptCriteria)
        {
            decryptCriteria.Text = account.Adress.Town;
            // si le nom de la ville ne contient pas le nom en critere
            if (!string.IsNullOrEmpty(criterias.Town) && !StringEncryptHelper.Decrypt(decryptCriteria).ToUpper().Contains(criterias.Town.ToUpper()))
            {
                return false;
            }
            return true;
        }

        /// <summary>Verifie que le code postal du compte correspond a celui passe en critere</summary>
        /// <param name="account">compte a verifier</param>
        /// <param name="criterias">Criteres a appliquer</param>
        /// <param name="decryptCriteria">Criteres de déchiffrage</param>
        /// <returns>bool : true si critere vide ou correspondant, false sinon</returns>
        private bool matchPostalCode(CandidateAccount account, ListAccountsRequest criterias, StringEncryptCriteria decryptCriteria)
        {
            decryptCriteria.Text = account.Adress.PostalCode;
            // si le nom de la ville ne contient pas le nom en critere
            if (!string.IsNullOrEmpty(criterias.PostalCode) && !StringEncryptHelper.Decrypt(decryptCriteria).ToUpper().Equals(criterias.PostalCode))
            {
                return false;
            }
            return true;
        }

        /// <summary>Verifie que le departement du compte correspond a celui passe en critere</summary>
        /// <param name="account">compte a verifier</param>
        /// <param name="criterias">Criteres a appliquer</param>
        /// <param name="decryptCriteria">Criteres de déchiffrage</param>
        /// <returns>bool : true si critere vide ou correspondant, false sinon</returns>
        private bool matchCounty(CandidateAccount account, ListAccountsRequest criterias, StringEncryptCriteria decryptCriteria)
        {
            decryptCriteria.Text = account.Adress.PostalCode;
            // si le nom de la ville ne contient pas le nom en critere
            if (!string.IsNullOrEmpty(criterias.County) && !StringEncryptHelper.Decrypt(decryptCriteria).Substring(0, 2).Equals(criterias.County))
            {
                return false;
            }
            return true;
        }
    }
}