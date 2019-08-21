using System.Collections.Generic;
using compte.Accounts.List;
using compte.Models.Accounts;
using lug.String.Encrypt;
using Microsoft.Extensions.Configuration;

namespace compte.FIlters.Accounts
{
    public class AgencyAccountFilters<T> : AccountFilters<T> where T : Account
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
                if (matchName(((AgencyAccount)(Account)account), criterias, decryptCriteria) && matchTown(((AgencyAccount)(Account)account), criterias, decryptCriteria)
                    && matchPostalCode(((AgencyAccount)(Account)account), criterias, decryptCriteria) && matchCounty(((AgencyAccount)(Account)account), criterias, decryptCriteria))
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
        private bool matchName(AgencyAccount account, ListAccountsRequest criterias, StringEncryptCriteria decryptCriteria)
        {
            decryptCriteria.Text = account.Name;
            // si le nom du compte ne contient pas le nom en critere
            if (!string.IsNullOrEmpty(criterias.Name) && !StringEncryptHelper.Decrypt(decryptCriteria).ToUpper().Contains(criterias.Name.ToUpper()))
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
        private bool matchTown(AgencyAccount account, ListAccountsRequest criterias, StringEncryptCriteria decryptCriteria)
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
        private bool matchPostalCode(AgencyAccount account, ListAccountsRequest criterias, StringEncryptCriteria decryptCriteria)
        {
            decryptCriteria.Text = account.Adress.PostalCode;
            // si le nom de la ville ne contient pas le nom en critere
            if (!string.IsNullOrEmpty(criterias.PostalCode) && !StringEncryptHelper.Decrypt(decryptCriteria).Equals(criterias.PostalCode))
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
        private bool matchCounty(AgencyAccount account, ListAccountsRequest criterias, StringEncryptCriteria decryptCriteria)
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