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

namespace blockchain_admintool
{
    public class Backend
    {

        Web3 web3;
        Bvs_backendService votingService;
        GetElectionInformationOutputDTO allElectionInfo;
        TmpElectionObject currentElection;
        GetElectoralListOutputDTO backendCandidates;

        public async void CreateElection(string admAdr, int votingSys, List<Candidate> candList, DateTime start, DateTime stop, string description, int voterCount)
        {

            
            await votingService.CreateElectionRequestAsync(admAdr, (byte)votingSys, description, DateToUnix(start), DateToUnix(stop));

            System.Numerics.BigInteger id = votingService.GetLastElectionIdQueryAsync().Result;

            MessageBox.Show(id.ToString());

            await votingService.ReplaceElectoralListRequestAsync(id, candList);

            List<String> voters = new List<string>(){"0x3D350e8FAdDFE27C33CD16c03178326E82012b4c", "0x861E2334a5A0Ab09Db7d2E49cb4f1c138dA55253", "0xf722b4A940C7f7A20CA3bF4ff5d21C49875FC34c", "0x4d93FF07B5b7D3B24Fc6374F6470785C67FB6ba4",
                "0x2d3E9280E7bFACa1720f302fB49172719A60dBa4", "0x072D8d0979c2Ff8b2367886FCDB04600392863eC", "0x1f96059FEBFc08C3fA2ba0b57921569227810A62", "0x3F60c6eBF19A76801Fc5e96db993b9d6082E34F6", "0xb35d1689421D7a51b778F70CF7990Fe01F4A3894",
                "0xA30668a30C500388D687fB2E50c35eaaB3dF1eA8" };

            await votingService.ReplaceListOfEligibleVotersRequestAsync(id, voters);



        }

        public System.Numerics.BigInteger DateToUnix(DateTime d)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Int32 unixdt = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

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
