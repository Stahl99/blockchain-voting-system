using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Nethereum.Web3.Accounts;
using Nethereum.HdWallet;
using BlockchainVotingSystem.Contracts.DHBWVoting;
using BlockchainVotingSystem.Contracts.bvs_backend;
using BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition;
using Nethereum.Web3;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Web3.Accounts.Managed;
using System.IO;

namespace blockchain_admintool
{
    public class Backend
    {

        Web3 web3;
        Bvs_backendService votingService;
        GetElectionInformationOutputDTO allElectionInfo;
        TmpElectionObject currentElection;
        GetElectoralListOutputDTO backendCandidates;

        public async void CreateElection(string admAdr, int votingSys, List<Candidate> candList, DateTime start, DateTime stop, string description, int voterCount, string prKeyLoc)
        {

            
            await votingService.CreateElectionRequestAsync(admAdr, (byte)votingSys, description, DateToUnix(start), DateToUnix(stop));

            System.Numerics.BigInteger id = votingService.GetLastElectionIdQueryAsync().Result;

            MessageBox.Show(id.ToString());

            await votingService.ReplaceElectoralListRequestAsync(id, candList);

            List<String> voters = new List<string>();

            List<String> privateKeys = new List<string>();

            for (int i = 0; i < voterCount; i++)
            {
                var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
                var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();
                var account = new Account(privateKey);

                voters.Add(account.Address.ToString());
                privateKeys.Add(privateKey.ToString());

                await web3.Eth.GetEtherTransferService().TransferEtherAndWaitForReceiptAsync(account.Address, 3.3m);
            }

            await votingService.ReplaceListOfEligibleVotersRequestAsync(id, voters);

            PrintPrKeys(privateKeys, prKeyLoc);

            MessageBox.Show("Wahl erfolgreich erstellt");

        }

        public System.Numerics.BigInteger DateToUnix(DateTime d)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Int32 unixdt = (Int32)(d.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            MessageBox.Show(unixdt.ToString());

            System.Numerics.BigInteger tmp = (System.Numerics.BigInteger)unixdt;

            MessageBox.Show(unixdt.ToString());

            return unixdt;
        }


        public bool SetContractAdress(String contractAdress)
        {
            // check format 
            if (!OnlyHexInString(contractAdress))
            {
                MessageBox.Show("Wrong format for election adresss (must be hex-string)");
                return false;
            }

            // create voting service with new contract adress
            this.votingService = new Bvs_backendService(web3, contractAdress);
            return true;
        }

        public void PrintPrKeys(List<string> prKeys, string path)
        {

            using var sw = new StreamWriter(path + "\\PrivKeys.txt");
            
            foreach(String s in prKeys)
            {
                sw.WriteLine(s);
            }

            sw.Close();

        }

        public void SetBlockchainUrl(String url, String walletAddr)
        {
            if (url.Length != 0)
            {

                /*
                var privateKey = "0x5569b93622765c3100095da4d24e9494231dc01873ad7c07d69acc06cc1ca3b3";
                var account = new Account(privateKey);*/

                var account = new ManagedAccount(walletAddr, "test");


               
                this.web3 = new Web3(account, url);

                
            
            }
        }

        // Init the contract service with a blockchain url and contractAdress
        public bool InitService(String url, String contractAdress, string walletAddr)
        {
            // The order of these functions is important! Web3 object is needed for service
            SetBlockchainUrl(url, walletAddr);
            if (!SetContractAdress(contractAdress))
            {
                return false;
            }

            return true;
        }

        private bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");
        }

    }
}
